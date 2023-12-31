using System.IO;
using System.Threading;
using System.Windows;
using Newtonsoft.Json;
using PropertyChanged;

namespace Ty1CollectibleTracker;

[AddINotifyPropertyChangedInterface]
public class MainWindowViewModel
{
    public float Scale { get; set; }
    public bool Running = true;
    public Settings Settings { get; set; }
    
    public CogViewModel CogCollectible { get; } = new CogViewModel();
    public TEViewModel TECollectible { get; } = new TEViewModel();
    public BilbyViewModel BilbyCollectible { get; } = new BilbyViewModel();
    public OpalViewModel OpalCollectible { get; } = new OpalViewModel();
    public RangViewModel RangCollectible { get; } = new RangViewModel();
    public TalismanViewModel TalismanCollectible { get; } = new TalismanViewModel();
    public FrameViewModel FrameCollectible { get; } = new FrameViewModel();

    private int _saveAddress;
    
    public MainWindowViewModel()
    {
        Scale = 1.0f;
        Settings = SettingsHandler.Settings;
        InvokeUpdateDisplay();
        var t = new Thread(TyLoop);
        t.Start();
    }

    private void TyLoop()
    {
        while (Running)
        {
            if (!TyProcess.FindProcess() || !TyProcess.IsRunning) continue;
            ProcessHandler.TryRead(0x288730, out _saveAddress, true, "");
            App.HLevel.GetCurrentLevel();
            OpalCollectible.UpdateImageSource(App.HLevel.CurrentLevelId);
            if(SettingsHandler.Settings.DisplayCogs)
                CogCollectible.ReadCount(_saveAddress);
            if (SettingsHandler.Settings.DisplayCogs)
                CogCollectible.ReadCount(_saveAddress);
            if (SettingsHandler.Settings.DisplayTEs)
                TECollectible.ReadCount(_saveAddress);
            if (SettingsHandler.Settings.DisplayBilbies)
                BilbyCollectible.ReadCount(_saveAddress);
            if (SettingsHandler.Settings.DisplayOpals)
                OpalCollectible.ReadCount(_saveAddress);
            if (SettingsHandler.Settings.DisplayRangs)
                RangCollectible.ReadCount(_saveAddress);
            if (SettingsHandler.Settings.DisplayTalismans)
                TalismanCollectible.ReadCount(_saveAddress);
            if (SettingsHandler.Settings.DisplayFrames)
                FrameCollectible.ReadCount(_saveAddress);
            Thread.Sleep(SettingsHandler.Settings.TickLength);
        }
    }

    public void InvokeUpdateDisplay()
    {
        CogCollectible.UpdateDisplay();
        TECollectible.UpdateDisplay();
        BilbyCollectible.UpdateDisplay();
        OpalCollectible.UpdateDisplay();
        RangCollectible.UpdateDisplay();
        TalismanCollectible.UpdateDisplay();
        FrameCollectible.UpdateDisplay();
    }
}