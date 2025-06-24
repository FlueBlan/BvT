using UnityEngine;
using UnityEngine.UI;

public class AudioSettingsController : MonoBehaviour
{
    public Slider volumeSlider;
    private AudioSource musicSource;

    private void Start()
    {
        // Find the persistent MusicManager in the scene
        GameObject musicManager = GameObject.Find("MusicManager");
        if (musicManager != null)
        {
            musicSource = musicManager.GetComponent<AudioSource>();
        }

        if (volumeSlider != null && musicSource != null)
        {
            volumeSlider.value = musicSource.volume;
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }
    }

    public void SetVolume(float volume)
    {
        if (musicSource != null)
        {
            musicSource.volume = volume;
        }
    }
}
