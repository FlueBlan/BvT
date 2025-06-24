using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [Range(0f, 1f)]
    public float sfxVolume = 1f;

    private AudioSource audioSource; // Don't make it static

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject); // Ensure this persists across scenes

        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogError("AudioSource component missing from SoundManager GameObject.");
        }
    }

    public void PlaySound(AudioClip clip)
    {
        if (audioSource == null || clip == null)
        {
            Debug.LogWarning("AudioSource or clip is missing when trying to play sound.");
            return;
        }

        audioSource.PlayOneShot(clip, sfxVolume);
    }
}
