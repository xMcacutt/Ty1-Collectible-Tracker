using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Ty1CollectibleTracker;

internal class ProcessHandler
{
    public static bool MemoryWriteDebugLogging = false;
    public static bool MemoryReadDebugLogging = false;
    public static string MostRecentIOIndicator = "None";
    public static bool LogMostRecentMemoryIOInfoOnProcessExit = true;

    [DllImport("kernel32.dll")]
    internal static extern unsafe bool ReadProcessMemory(
        nint hProcess,
        void* lpBaseAddress,
        void* lpBuffer,
        nuint nSize,
        nuint* lpNumberOfBytesRead);
    
    public static bool TryRead<T>(nint address, out T result, bool addBase, string readIndicator) where T : unmanaged
    {
        return TryRead(address, out result, addBase);
    }

    private static unsafe bool TryRead<T>(nint address, out T result, bool addBase) where T : unmanaged
    {
        try
        {
            fixed (T* pResult = &result)
            {
                //string s = GetCallStackAsString();
                if (addBase) address = TyProcess.BaseAddress + address;
                nuint nSize = (nuint)sizeof(T), nRead;
                //BasicIoC.Logger.Write(address.ToString() + " " + s);
                return ReadProcessMemory(TyProcess.Handle, (void*)address, pResult, nSize, &nRead)
                       && nRead == nSize;
            }
        }
        catch (Exception ex)
        {
            result = default;
            return false;
        }
    }

    public static unsafe bool TryReadBytes(nint address, out byte[] buffer, int length, bool addBase)
    {
        try
        {
            buffer = new byte[length];
            if (addBase) address = TyProcess.BaseAddress + address;

            fixed (byte* pBuffer = buffer)
            {
                nuint nRead;
                return ReadProcessMemory(TyProcess.Handle, (void*)address, pBuffer, (nuint)length, &nRead)
                       && nRead == (nuint)length;
            }
        }
        catch (Exception ex)
        {
            buffer = Array.Empty<byte>();
            return false; 
        }
    }
}