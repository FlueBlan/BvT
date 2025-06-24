using UnityEngine;

public class PauseManager2 : MonoBehaviour
{
    public static PauseManager2 Instance { get; private set; }

    private static bool isPaused = false;
    public static bool IsPaused => isPaused; // Read-only from outside

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public static void PauseGame()
    {
        isPaused = true;
        Debug.Log("Game Paused");
    }

    public static void ResumeGame()
    {
        isPaused = false;
        Debug.Log("Game Resumed");
    }

    public static void TogglePause()
    {
        if (isPaused) ResumeGame();
        else PauseGame();
    }
}
