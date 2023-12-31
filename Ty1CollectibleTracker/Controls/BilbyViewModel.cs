using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using PropertyChanged;

namespace Ty1CollectibleTracker;

[AddINotifyPropertyChangedInterface]
public class BilbyViewModel
{
    public bool Display { get; set; }
    public Settings Settings { get; set; }
    public string Text { get; set; }
    public string ImageSource { get; set; }
    public bool Complete { get; set; }

    public BilbyViewModel()
    {
        Text = "??";
        Complete = false;
        ImageSource = "../Images/Bilby.png";
        Settings = SettingsHandler.Settings;
    }

    public void UpdateDisplay()
    {
        Display = Settings.DisplayBilbies;
    }

    public void ReadCount(int saveAddr)
    {
        var addr = saveAddr + 0x10 + 0x70 * App.HLevel.CurrentLevelId + 0x3A;
        ProcessHandler.TryReadBytes(addr, out var bytes, 5, false);
        Text = bytes.Count(x => x > 0).ToString();
        Complete = Text == "5";
    }
}