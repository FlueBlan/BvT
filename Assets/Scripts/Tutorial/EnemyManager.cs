using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;
    private bool isPaused = false;

    void Awake()
    {
        Instance = this;
    }

    public void SetPaused(bool pause)
    {
        isPaused = pause;
    }

    void Update()
    {
        if (isPaused) return;

        // enemy spawning and movement logic
    }
}
