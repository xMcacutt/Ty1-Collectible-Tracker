using System;

namespace Ty1CollectibleTracker;

public class LevelHandler
{
    public LevelData CurrentLevelData;
    private int currentLevelId;
    public int CurrentLevelId
    {
        get => currentLevelId;
        set
        {
            CurrentLevelData = Levels.GetLevelData(value);
            currentLevelId = value;
        }
    }

    public void GetCurrentLevel()
    {
        ProcessHandler.TryRead(0x280594, out int levelId, true, "LevelHandler::GetCurrentLevel()");
        CurrentLevelId = levelId;
    }
}