using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    // Track unlocked levels (for now just a simple boolean array)
    private bool[] unlockedLevels;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);  // Prevent duplicate
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // Keep LevelManager across scenes
        }

        // Initialize unlocked levels, start with level 1 unlocked
        unlockedLevels = new bool[3]; // 3 levels
        unlockedLevels[0] = true; // Level 1 is unlocked initially
        for (int i = 1; i < unlockedLevels.Length; i++)
        {
            unlockedLevels[i] = false; // Other levels are locked initially
        }
    }

    // Unlock a specific level
    public void UnlockLevel(int levelIndex)
    {
        if (levelIndex >= 0 && levelIndex < unlockedLevels.Length)
        {
            unlockedLevels[levelIndex] = true;
        }
    }

    // Check if a level is unlocked
    public bool IsLevelUnlocked(int levelIndex)
    {
        if (levelIndex >= 0 && levelIndex < unlockedLevels.Length)
        {
            return unlockedLevels[levelIndex];
        }
        return false;
    }

    // This can be accessed statically across your game
    public static class DialogueTriggerFlag
    {
        public static bool showAfterLevel1Dialogue = false;
    }
}
