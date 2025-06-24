using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    public AudioClip clickSound;

    public void PlayClickSound()
    {
        SoundManager.instance.PlaySound(clickSound);
    }
}
