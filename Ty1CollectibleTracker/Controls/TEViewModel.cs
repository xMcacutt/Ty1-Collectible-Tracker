using System;
using System.Linq;
using System.Windows;
using PropertyChanged;

namespace Ty1CollectibleTracker;

[AddINotifyPropertyChangedInterface]
public class TEViewModel
{
    public bool Display { get; set; }
    public string Text { get; set; }
    public string ImageSource { get; set; }
    public Settings Settings { get; set; }
    public bool Complete { get; set; }
    
    public void UpdateDisplay()
    {
        Display = Settings.DisplayTEs;
    }

    public TEViewModel()
    {
        Complete = false;
        Text = "??";
        Settings = SettingsHandler.Settings;
        ImageSource = "../Images/TE.png";
    }

    public void ReadCount(int saveAddr)
    {
        var count = 0;
        var lCount = 0;
        foreach (var level in Levels.MainStages)
        {
            var addr = saveAddr + 0x10 + 0x70 * level.Id + 0x28;
            ProcessHandler.TryReadBytes(addr, out var bytes, 8, false);
            var add = bytes.Count(x => x == 1);
            count += add;
            if (level.Id == App.HLevel.CurrentLevelId) lCount = add;
        }
        Text = count.ToString();
        Complete = count == 72 || lCount == 8;
    }
}