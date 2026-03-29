using UnityEngine;
using UnityEngine.SceneManagement;
using static LevelManager;

public class LevelCompletePanel : MonoBehaviour
{
    [Header("Victory SFX")]
    public AudioSource audioSource;
    public AudioClip victorySFX;

    void OnEnable()
    {
        
        // Play victory sound when the panel appears
        if (audioSource != null && victorySFX != null)
        {
            audioSource.PlayOneShot(victorySFX);
        }
    }

    public void OnContinueToLevelSelect()
    {
        LevelManager.instance.UnlockLevel(1);  // Unlock Level 2
        GameStateManager.instance.showAfterLevel1Dialogue = true;
        SceneManager.LoadScene("LevelSelect");
    }

    public static class GameFlags
    {
        public static bool showAfterLevel1Dialogue = false;
    }

}