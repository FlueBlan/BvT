using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager instance;

    public bool showAfterLevel1Dialogue = false;
    public bool showAfterLevel2Dialogue = false;
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // Keep this object across scene loads
        }
    }
    // GameStateManager.cs
    public void ResetState()
    {
        showAfterLevel1Dialogue = false;
        showAfterLevel2Dialogue = false;
        // reset other flags, scores, etc.
    }

    // LevelManager.cs
    public void ResetLevels()
    {
        PlayerPrefs.DeleteAll();  // or use your own save reset system
    }

}
