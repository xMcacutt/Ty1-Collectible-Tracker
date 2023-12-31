using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using ColorPickerWPF;
using ColorPickerWPF.Code;

namespace Ty1CollectibleTracker.Views;

public partial class SettingsView : Window
{
    public SettingsView()
    {
        InitializeComponent();
    }

    private void SettingsView_OnClosing(object? sender, CancelEventArgs e)
    {
        if (Application.Current.MainWindow == null) return;
        SettingsHandler.Save();
        (Application.Current?.MainWindow?.DataContext as MainWindowViewModel).InvokeUpdateDisplay();
    }

    private void InstallFont_OnClick(object sender, RoutedEventArgs e)
    {
        var info = new ProcessStartInfo()
        {
            WorkingDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Fonts"),
            FileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Fonts/FontReg.exe"),
            Arguments = "/copy",
            UseShellExecute = false,
            WindowStyle = ProcessWindowStyle.Hidden
        };
        Process.Start(info);
        SettingsHandler.Settings.FontFamily = new FontFamily("Komika Hand");
        SettingsHandler.Save();
        var executablePath = Process.GetCurrentProcess().MainModule.FileName;
        Process.Start(executablePath);
        SettingsHandler.Save();
        (Application.Current?.MainWindow?.DataContext as MainWindowViewModel).InvokeUpdateDisplay();
        Application.Current.Shutdown();
    }

    private void FontColor_Click(object sender, MouseButtonEventArgs e)
    {
        Color color;
        Application.Current.MainWindow.Topmost = false;
        Topmost = false;
        var ok = ColorPickerWindow.ShowDialog(out color);
        Application.Current.MainWindow.Topmost = true;
        Topmost = true;
        if (!ok) return;
        (FontColorSwatch.Fill as SolidColorBrush).Color = color;
    }
}