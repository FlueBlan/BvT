using UnityEngine;
using UnityEngine.SceneManagement;

public class Level2CompletePanel : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip victorySFX;

    void OnEnable()
    {
        if (audioSource != null && victorySFX != null)
        {
            audioSource.PlayOneShot(victorySFX);
        }
    }

    public void OnContinueToLevelSelect()
    {
        LevelManager.instance.UnlockLevel(2);  // Unlock Level 3
        GameStateManager.instance.showAfterLevel2Dialogue = true;  // Set this flag
        SceneManager.LoadScene("LevelSelect");
    }

}
