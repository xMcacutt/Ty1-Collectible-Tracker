using System;
using System.Diagnostics;
using System.IO;
using System.Net.Mime;
using System.Runtime.InteropServices;
using System.Windows;

namespace Ty1CollectibleTracker;

internal class TyProcess
{
    private static Process process;
    private static bool handleOpen;

    public static bool IsRunning { get; private set; }
    public static nint BaseAddress { get; private set; }
    public static IntPtr Handle { get; private set; }

    [DllImport("kernel32.dll")]
    public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

    [DllImport("kernel32.dll")]
    public static extern bool CloseHandle(IntPtr handle);

    public static event Action OnTyProcessExited = delegate { };
    public static event Action OnTyProcessFound = delegate { };

    //Returns true if process is running
    //Attempts to find the Ty process if not, returns true if successfully found
    //Returns false if Ty is closed or is in the process of being launched
    public static bool FindProcess()
    {
        if (IsRunning)
            return true;
        
        var processes = Process.GetProcessesByName("Mul-Ty-Player");
        if (processes.Length == 0)
        {
            processes = Process.GetProcessesByName("TY");
            if(processes.Length == 0)
                return false;
        }

        if (processes.Length > 1)
        {
            //Use first non-exiting process
            var setProcess = false;
            for (var i = 1; i < processes.Length; i++)
                if (processes[i].HasExited)
                {
                    processes[i].Dispose();
                    processes[i].Close();
                }
                else if (!setProcess)
                {
                    process = processes[i];
                    OnTyProcessFound?.Invoke();
                    PullProcessData();
                    setProcess = true;
                }
            return setProcess;
        }
        process = processes[0];
        OnTyProcessFound?.Invoke();
        PullProcessData();
        return true;
    }

    private static void PullProcessData()
    {
        process.EnableRaisingEvents = true;
        process.Exited += OnExit;
        Handle = OpenProcess(0x1F0FFF, false, process.Id);
        handleOpen = true;
        //Ty takes a little while to open from process.Start(), so we wait until we can find the mainmodule before continuing
        while (process.MainModule == null)
        {
        }
        BaseAddress = process.MainModule.BaseAddress;
        IsRunning = true;
    }

    private static void OnExit(object o, EventArgs e)
    {
        OnTyProcessExited?.Invoke();
        IsRunning = false;
        process.Close();
        CloseHandle();
        process.Dispose();
        process.Refresh();
    }

    private static void CloseHandle()
    {
        if (!handleOpen)
            return;
        var successfullyClosed = CloseHandle(Handle);
        handleOpen = false;
    }
}

public class TyProcessException : Exception
{
    public TyProcessException() : base("Ty process has exited, been lost, or privileges have changed.")
    {
    }

    public TyProcessException(string source) : base(
        "Ty process has exited, been lost, or privileges have changed.")
    {
        Source = source;
    }

    public TyProcessException(Exception innerException) : base(
        "Ty process has exited, been lost, or privileges have changed.", innerException)
    {
    }

    public TyProcessException(string source, Exception innerException) : base(
        "Ty process has exited, been lost, or privileges have changed.", innerException)
    {
        Source = source;
    }
}