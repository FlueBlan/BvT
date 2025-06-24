using UnityEngine;

public class MusicStopper : MonoBehaviour
{
    public void StopMusic()
    {
        GameObject musicPlayer = GameObject.Find("MusicPlayer");
        if (musicPlayer != null)
        {
            Destroy(musicPlayer);
        }
    }
}
