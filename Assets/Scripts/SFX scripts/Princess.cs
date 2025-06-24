using UnityEngine;

public class Princess : MonoBehaviour
{
    private AudioSource princessVoice;

    void Awake()
    {
        princessVoice = GetComponent<AudioSource>();
        if (princessVoice == null)
            Debug.LogWarning("Princess: AudioSource not found on GameObject!");
    }

    public void PlayPrincessSound()
    {
        if (princessVoice != null)
            princessVoice.Play();
    }
}
