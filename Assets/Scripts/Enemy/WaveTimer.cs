using UnityEngine;
using TMPro;
using System.Collections;

public class WaveTimer : MonoBehaviour
{
    public TextMeshProUGUI waveCountdownText;
    public float timeBetweenWaves = 10f;
    private int currentWave = 0;
    private int totalWaves = 5;

    void Start()
    {
        StartCoroutine(WaveTimerRoutine());
    }

    IEnumerator WaveTimerRoutine()
    {
        while (currentWave < totalWaves)
        {
            // Wait if globally paused
            yield return new WaitUntil(() => !PauseManager2.IsPaused);

            waveCountdownText.text = GetRandomWaveMessage();

            float timer = 0f;
            while (timer < timeBetweenWaves)
            {
                if (!PauseManager2.IsPaused)
                    timer += Time.deltaTime;

                yield return null;
            }

            currentWave++;

            if (currentWave == totalWaves)
            {
                waveCountdownText.text = "Final Wave!";
                yield return new WaitForSeconds(7f);
                waveCountdownText.text = "";
            }
            else
            {
                waveCountdownText.text = "";
            }
        }

        waveCountdownText.text = "All Waves Complete!";
    }

    string GetRandomWaveMessage()
    {
        string[] messages = { "New Wave Incoming!", "Prepare Yourself!", "Enemies Approaching!", "Brace Yourself!" };
        return messages[Random.Range(0, messages.Length)];
    }
}
