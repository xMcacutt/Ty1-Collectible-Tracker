using System;
using System.Linq;
using System.Windows;
using PropertyChanged;

namespace Ty1CollectibleTracker;

[AddINotifyPropertyChangedInterface]
public class RangViewModel
{    
    public bool Display { get; set; }
    public string Text { get; set; }
    public string ImageSource { get; set; }
    public Settings Settings { get; set; }
    public bool Complete { get; set; }

    public void UpdateDisplay()
    {
        Display = Settings.DisplayRangs;
    }
    
    public RangViewModel()
    {
        Complete = false;
        Text = "??";
        ImageSource = "../Images/Rang.png";
        Settings = SettingsHandler.Settings;
    }

    public void ReadCount(int saveAddr)
    {
        var addr = saveAddr + 0xAB8;
        ProcessHandler.TryReadBytes(addr, out var bytes, 12, false);
        Text = bytes.Count(x => x == 1).ToString();
        Complete = Text == "12";
    }
}