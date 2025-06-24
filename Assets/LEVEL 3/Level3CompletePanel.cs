using UnityEngine;
using UnityEngine.SceneManagement;

public class Level3CompletePanel : MonoBehaviour
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

    public void OnContinueToEndCredits()
    {
        SceneManager.LoadScene("EndCreditsScene");
    }
}
