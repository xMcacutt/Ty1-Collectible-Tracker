using System;
using System.Linq;
using System.Windows;
using PropertyChanged;

namespace Ty1CollectibleTracker;

[AddINotifyPropertyChangedInterface]
public class FrameViewModel
{
    public bool Display { get; set; }
    public Settings Settings { get; set; }
    public string Text { get; set; }
    public string ImageSource { get; set; }
    public bool Complete { get; set; }

    public void UpdateDisplay()
    {
        Display = Settings.DisplayFrames;
    }
    
    public FrameViewModel()
    {
        Complete = false;
        Text = "??";
        ImageSource = "../Images/Frame.png";
        Settings = SettingsHandler.Settings;
    }

    public void ReadCount(int saveAddr)
    {
        var addr = saveAddr + 0xAD2;
        ProcessHandler.TryReadBytes(addr, out var bytes, 46, false);
        var bitString = bytes.SelectMany(b => Convert.ToString(b, 2).PadLeft(8, '0').Reverse());
        var count = bitString.Count(b => b == '1');
        Text = count.ToString();
        var data = App.HLevel.CurrentLevelData;
        var frameCount = data.FrameCount;
        if (frameCount == 0) return;
        var rollingCount = data.RollingFrameCount;

        var lBitString = bitString.Skip(rollingCount).Take(frameCount);
        var lCount = lBitString.Count(b => b == '1');
        Complete = count == 373 || lCount == App.HLevel.CurrentLevelData.FrameCount;
    }
}