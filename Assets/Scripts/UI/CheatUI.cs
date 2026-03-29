using UnityEngine;

public class CheatUI : MonoBehaviour
{
    private PlayerStats ps;
    private WaveSpawner_Level3 ws;

    void Awake()
    {
        ps = FindObjectOfType<PlayerStats>();
        ws = FindObjectOfType<WaveSpawner_Level3>();
    }
    public void MoneyCheat(int amount)
    {
        amount = 500;
        ps.AddMoneyNow(amount);
    }

    public void SpawnToast()
    {
        if (ws != null)
            ws.SpawnToastCheat();
    }

    public void SpawnBurntToast()
    {
        if (ws != null)
            ws.SpawnBurntToastCheat();
    }
}
