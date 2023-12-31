using System.IO;
using System.Net;
using System.Windows.Media;
using Newtonsoft.Json;

namespace Ty1CollectibleTracker;


public class SettingsHandler
{
    public static Settings Settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText("./Settings.json"));

    public static void Save()
    {
        File.WriteAllText("./Settings.json", JsonConvert.SerializeObject(Settings));
    }
}

public class Settings
{
    public bool DisplayOpals { get; set; }
    public bool DisplayCogs { get; set; }
    public bool DisplayTEs { get; set; }
    public bool DisplayBilbies { get; set; }
    public bool DisplayFrames { get; set; }
    public bool DisplayRangs { get; set; }
    public bool DisplayTalismans { get; set; }
    public int TickLength { get; set; }
    public FontFamily FontFamily { get; set; }
    public Color FontColor { get; set; }
    public float ItemScale { get; set; }
    public bool TransparentGrabBar { get; set; }
}
