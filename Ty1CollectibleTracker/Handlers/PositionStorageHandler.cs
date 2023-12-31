using System.IO;
using Newtonsoft.Json;

namespace Ty1CollectibleTracker;

public class PositionStorageHandler
{
    public static Positions Positions;

    public static void Load()
    {
        var json = File.ReadAllText("./Positions.json");
        Positions = JsonConvert.DeserializeObject<Positions>(json);
    }

    public static void Save()
    {
        var json = JsonConvert.SerializeObject(Positions, Formatting.Indented);
        File.WriteAllText("./Positions.json", json);
    }
}

public class Positions
{
    public double[] TEPosition { get; set; }
    public double[] CogPosition { get; set; }
    public double[] TalismanPosition { get; set; }
    public double[] OpalPosition { get; set; }
    public double[] BilbyPosition { get; set; }
    public double[] RangPosition { get; set; }
    public double[] FramePosition { get; set; }
    public double[] WindowPosition { get; set; }
    public double[] WindowSize { get; set; }
    public bool WindowMaximized { get; set; }
}