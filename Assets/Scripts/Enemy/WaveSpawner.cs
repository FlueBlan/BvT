using UnityEngine;
using System.Collections;

public class WaveSpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject burntToastPrefab; // Assign in Inspector
    public GameObject levelCompletedPanel; // Assign in Inspector

    public Transform spawnPoint;
    public Transform endPoint;

    public float timeBetweenWaves = 5f;
    private float countdown = 2f;

    private int waveNumber = 1;
    private int maxWaves = 5;

    private float spacingZ = 10f;
    private float spacingX = 4f;
    private int enemiesPerRow = 3;

    private bool burntToastSpawned = false;
    private bool levelEnded = false;
    private bool finalWaveSpawned = false;

    public static WaveSpawner instance;

    private int currentWave = 0;         // Exposed as read-only
    private bool waveInProgress = false; // Exposed as read-only

    public int CurrentWaveIndex => currentWave;
    public bool IsWaveInProgress => waveInProgress;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (PauseManager2.IsPaused) return;
        if (Time.timeScale == 0f) return;

        // ✅ Fix: Only end after final wave was spawned AND all enemies are dead
        if (finalWaveSpawned && !levelEnded && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            EndLevel();
            return;
        }

        // Stop spawning if all waves have been scheduled
        if (waveNumber > maxWaves) return;

        if (countdown <= 0f && !waveInProgress)
        {
            StartCoroutine(SpawnWaveRoutine());
            countdown = timeBetweenWaves;
        }

        countdown -= Time.deltaTime;
    }

    private IEnumerator SpawnWaveRoutine()
    {
        waveInProgress = true;

        for (int i = 0; i < waveNumber; i++)
        {
            int row = i / enemiesPerRow;
            int column = i % enemiesPerRow;

            float spawnX = spawnPoint.position.x - (row * spacingX);
            float spawnZ = 0 + (column * spacingZ);
            float spawnY = 0f;

            Vector3 spawnPos = new Vector3(spawnX, spawnY, spawnZ);
            Vector3 targetPos = new Vector3(endPoint.position.x - (row * spacingX), spawnY, spawnZ);

            GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

            EnemyMovement moveScript = enemy.GetComponent<EnemyMovement>();
            if (moveScript != null)
            {
                moveScript.SetTarget(targetPos);
            }
        }

        // Spawn Burnt Toast in the last wave only
        if (waveNumber == maxWaves && !burntToastSpawned)
        {
            float spawnX = spawnPoint.position.x + 5f;
            float spawnZ = spacingZ;
            float spawnY = 5f;

            Vector3 spawnPos = new Vector3(spawnX, spawnY, spawnZ);
            Vector3 targetPos = new Vector3(endPoint.position.x, spawnY, spawnZ);

            GameObject burnt = Instantiate(burntToastPrefab, spawnPos, Quaternion.Euler(0, 180, 0));

            EnemyMovement moveScript = burnt.GetComponent<EnemyMovement>();
            if (moveScript != null)
            {
                moveScript.SetTarget(targetPos);
            }

            burntToastSpawned = true;
            Debug.Log("Burnt Toast spawned in final wave!");
        }

        currentWave = waveNumber;

        // ✅ Move final wave flag here BEFORE waiting for enemies to die
        if (waveNumber == maxWaves)
        {
            finalWaveSpawned = true;
        }

        waveNumber++;

        // ✅ Now wait for all enemies to be gone before moving to next wave
        yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("Enemy").Length == 0);

        waveInProgress = false;
    }

    void EndLevel()
    {
        levelEnded = true;
        if (levelCompletedPanel != null)
        {
            levelCompletedPanel.SetActive(true);
            Debug.Log("Level Completed!");
        }
        else
        {
            Debug.LogWarning("LevelCompletedPanel not assigned in inspector!");
        }
    }

    public void ResetSpawner()
    {
        waveNumber = 1;
        currentWave = 0;
        countdown = 2f;
        burntToastSpawned = false;
        levelEnded = false;
        waveInProgress = false;
        finalWaveSpawned = false;
        enabled = true;
    }

    // Optional: legacy compatibility
    public int CurrentWave => waveNumber;
    public bool IsSpawning => countdown <= 0f && waveNumber <= maxWaves;
}
