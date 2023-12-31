using System;
using System.Linq;
using System.Windows;
using PropertyChanged;

namespace Ty1CollectibleTracker;

[AddINotifyPropertyChangedInterface]
public class TalismanViewModel
{
    public bool Display { get; set; }
    public Settings Settings { get; set; }
    public string Text { get; set; }
    public string ImageSource { get; set; }
    public bool Complete { get; set; }
    
    public void UpdateDisplay()
    {
        Display = Settings.DisplayTalismans;
    }

    public TalismanViewModel()
    {
        Complete = false;
        Text = "??";
        ImageSource = "../Images/Talisman.png";
        Settings = SettingsHandler.Settings;
    }

    public void ReadCount(int saveAddr)
    {
        var addr = saveAddr + 0xAC4;
        ProcessHandler.TryReadBytes(addr, out var bytes, 5, false);
        Text = bytes.Count(x => x == 1).ToString();
        Complete = Text == "5";
    }
}