using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;


public class PlayerStats : MonoBehaviour
{
    public static int Money;
    public int StartMoney = 200;

    public static int Lives = 5;
    public int startLives = 5;
    public GameObject gameOverBackground; 
    public TextMeshProUGUI countdownText; // Assign in inspector


    public GameObject gameOverUI;

    [Header("Game Over SFX")]
    public AudioSource audioSource;
    public AudioClip gameOverSFX;

    void Start()
    {
        Money = StartMoney;
        Lives = startLives;
        Time.timeScale = 1f; // Unpause in case it's reloaded
    }

    void Update()
    {
        if (Lives <= 0)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        if (gameOverUI.activeSelf) return; // Prevent triggering twice

        Time.timeScale = 0f;
        gameOverBackground.SetActive(true); // Show background
        gameOverUI.SetActive(true);         // Show game over text/buttons
        Debug.Log("Game Over");

        // Play Game Over sound
        if (audioSource != null && gameOverSFX != null)
        {
            audioSource.PlayOneShot(gameOverSFX);
        }
    }


    //  Button Function to Retry the Level
    void RetryLevel()
    {
        StartCoroutine(RestartAfterDelay());
       
        PlayerStats.Lives = 5;
        PlayerStats.Money = 200;

        // Reactivate or reset your spawner
        WaveSpawner spawner = FindAnyObjectByType<WaveSpawner>();
        if (spawner != null)
        {
            spawner.ResetSpawner();
        }

        // Optional: Clear enemies from scene
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Destroy(enemy);
        }

        // Reactivate turrets or reset placement if needed

        // Hide Game Over UI
        gameOverUI.SetActive(false);
        gameOverBackground.SetActive(false); // if separate

    }
    private IEnumerator RestartAfterDelay()
    {
        // Show the countdown UI
        if (countdownText != null)
        {
            countdownText.gameObject.SetActive(true);
        }

        for (int i = 3; i > 0; i--)
        {
            if (countdownText != null)
            {
                countdownText.text = i.ToString();
            }
            yield return new WaitForSecondsRealtime(1f);
        }

        // Hide countdown UI
        if (countdownText != null)
        {
            countdownText.gameObject.SetActive(false);
        }

        Time.timeScale = 1f;
        PlayerStats.Lives = 5;
        PlayerStats.Money = 400;

        WaveSpawner spawner = Object.FindAnyObjectByType<WaveSpawner>();

        if (spawner != null)
        {
            spawner.ResetSpawner();
        }

        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Destroy(enemy);
        }

        gameOverUI.SetActive(false);
        gameOverBackground.SetActive(false);

        LevelCountdownBar countdownBar = Object.FindAnyObjectByType<LevelCountdownBar>();
        if (countdownBar != null)
        {
            countdownBar.ResetSlider();
        }
    }



    //  Button Function to Go to Main Menu
    void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu"); // Replace "MainMenu" with your actual menu scene name
    }

    // These public methods can be hooked up to your UI buttons
    public void OnRetryButtonPressed()
    {
        StartCoroutine(RestartAfterDelay());
    }

    public void OnMenuButtonPressed()
    {
        GoToMainMenu();
    }

}
