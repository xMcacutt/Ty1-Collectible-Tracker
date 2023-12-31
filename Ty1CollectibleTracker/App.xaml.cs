using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Ty1CollectibleTracker
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static LevelHandler HLevel;
        
        public App()
        {
            var HSettings = new SettingsHandler();
            HLevel = new LevelHandler();
        }
    }
}