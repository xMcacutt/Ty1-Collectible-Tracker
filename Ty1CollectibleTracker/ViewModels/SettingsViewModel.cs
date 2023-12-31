using System.Diagnostics;
using System.Net.Mime;
using System.Windows;
using PropertyChanged;

namespace Ty1CollectibleTracker;

[AddINotifyPropertyChangedInterface]
public class SettingsViewModel
{
    public Settings? Settings { get; set; }

    public SettingsViewModel()
    {
        Settings = SettingsHandler.Settings;
    }
}