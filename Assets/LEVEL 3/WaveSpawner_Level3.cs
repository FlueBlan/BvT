using UnityEngine;
using System.Collections;

public class WaveSpawner_Level3 : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject burntToastPrefab;
    public GameObject levelCompletedPanel;

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

    public static WaveSpawner_Level3 instance;

    private int currentWave = 0;
    private bool waveInProgress = false;

    public int CurrentWaveIndex => currentWave;
    public bool IsWaveInProgress => waveInProgress;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (PauseManager2.IsPaused || Time.timeScale == 0f) return;

        if (finalWaveSpawned && !levelEnded && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            EndLevel();
            return;
        }

        //Debugging, add in later
        //if (waveNumber > maxWaves) return;

        if (countdown <= 0f && !waveInProgress)
        {
            StartCoroutine(SpawnWaveRoutine());
            countdown = timeBetweenWaves;
        }

        countdown -= Time.deltaTime;
    }

    private int GetEnemyCountForWave(int wave)
    {
        if (wave == 1 || wave == 2)
            return wave + 1;        // 2, 3
        else if (wave == 3 || wave == 4)
            return wave + 2;        // 5, 6
        else
            return wave + 3;        // 8
    }

    private IEnumerator SpawnWaveRoutine()
    {
        waveInProgress = true;

        int totalEnemiesThisWave = GetEnemyCountForWave(waveNumber);

        for (int i = 0; i < totalEnemiesThisWave; i++)
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
        waveNumber++;

        //Debugging, add in later
        //if (waveNumber > maxWaves)
        //    finalWaveSpawned = true;

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

    #region UI Debugging
    //Debugging properties
    public void SpawnToastCheat()
    {
    // Use the same logic as the first enemy in a wave
    int row = 0;
    int column = 0;

    float spawnX = spawnPoint.position.x - (row * spacingX);
    float spawnZ = 0 + (column * spacingZ);
    float spawnY = 0f;

    Vector3 spawnPos = new Vector3(spawnX, spawnY, spawnZ);
    Vector3 targetPos = new Vector3(endPoint.position.x - (row * spacingX), spawnY, spawnZ);

    GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

    EnemyMovement moveScript = enemy.GetComponent<EnemyMovement>();
    if (moveScript != null)
        moveScript.SetTarget(targetPos);
    }

    public void SpawnBurntToastCheat()
{
    // Use the same logic as the first enemy in a wave
    int row = 0;
    int column = 0;

    float spawnX = spawnPoint.position.x - (row * spacingX);
    float spawnZ = 0 + (column * spacingZ);
    float spawnY = 0f;

    Vector3 spawnPos = new Vector3(spawnX, spawnY, spawnZ);
    Vector3 targetPos = new Vector3(endPoint.position.x - (row * spacingX), spawnY, spawnZ);

    GameObject enemy = Instantiate(burntToastPrefab, spawnPos, Quaternion.identity);

    EnemyMovement moveScript = enemy.GetComponent<EnemyMovement>();
    if (moveScript != null)
        moveScript.SetTarget(targetPos);
    }
    #endregion

    public int CurrentWave => waveNumber;
    public bool IsSpawning => countdown <= 0f && waveNumber <= maxWaves;
}
