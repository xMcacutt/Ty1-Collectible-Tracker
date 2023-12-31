using System;
using System.Collections;
using System.Linq;
using System.Windows;
using PropertyChanged;

namespace Ty1CollectibleTracker;

[AddINotifyPropertyChangedInterface]
public class OpalViewModel
{
    public bool Display { get; set; }
    public Settings Settings { get; set; }
    public string Text { get; set; }
    public string ImageSource { get; set; }
    public bool Complete { get; set; }

    public void UpdateDisplay()
    {
        Display = Settings.DisplayOpals;
    }

    public OpalViewModel()
    {
        Complete = false;
        Text = "??";
        ImageSource = "../Images/Opal.png";
        Settings = SettingsHandler.Settings;
    }

    public void UpdateImageSource(int level)
    {
        ImageSource = level switch
        {
            < 4 => "../Images/RainbowScale.png",
            < 8 => "../Images/OpalA.png",
            < 12 => "../Images/OpalB.png",
            < 16 => "../Images/OpalC.png",
            _ => "../Images/OpalC.png"
        };
    }

    public void ReadCount(int saveAddr)
    {
        ProcessHandler.TryRead(0x2888B0, out int count, true, "");
        Text = count.ToString();
        var lCount = App.HLevel.CurrentLevelId == 0 ? 25 : 300;
        Complete = count == lCount;
    }
}