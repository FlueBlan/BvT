using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;
using System.Collections;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager instance;

    public GameObject mainMenuPanel;
    public DialogueManager dialogueManager;
    public Dialogue introDialogue;
    public AudioSource menuMusic;

    public CanvasGroup fadePanelCanvasGroup;
    public Canvas cinematicCanvas;
    public RawImage videoRawImage;
    public VideoPlayer videoPlayer;
    public Button skipButton;

    private bool videoFinished = false;
    private bool cinematicSkipped = false;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        FindReferences();
        SetupStartButton();

        if (cinematicCanvas != null)
            cinematicCanvas.gameObject.SetActive(false);
    }

    private void FindReferences()
    {
        if (mainMenuPanel == null)
            mainMenuPanel = GameObject.Find("MainMenuCanvas");

        if (fadePanelCanvasGroup == null)
            fadePanelCanvasGroup = GameObject.Find("FadePanel")?.GetComponent<CanvasGroup>();

        if (dialogueManager == null)
            dialogueManager = FindFirstObjectByType<DialogueManager>();

        if (videoPlayer != null)
            videoPlayer.Stop();
    }

    private void SetupStartButton()
    {
        Button startButton = mainMenuPanel?.GetComponentInChildren<Button>();
        if (startButton != null)
        {
            startButton.onClick.RemoveAllListeners();
            startButton.onClick.AddListener(StartGame);
        }
    }

    public void StartGame()
    {
        StartCoroutine(StartGameRoutine());
    }

    private IEnumerator StartGameRoutine()
    {
        // Step 1: Fade to black
        yield return StartCoroutine(FadeInBlack());

        // Step 2: Stop menu music
        if (menuMusic != null)
            menuMusic.Stop();

        // Step 3: Hide menu and show cinematic canvas
        if (mainMenuPanel != null)
            mainMenuPanel.SetActive(false);

        if (cinematicCanvas != null)
            cinematicCanvas.gameObject.SetActive(true);

        // Step 4: Prepare and play video
        cinematicSkipped = false;
        videoFinished = false;

        if (skipButton != null)
        {
            skipButton.gameObject.SetActive(true);
            skipButton.onClick.RemoveAllListeners();
            skipButton.onClick.AddListener(SkipCinematic);
        }

        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached += OnVideoFinished;
            videoPlayer.Play();
        }

        // Step 5: Fade out to show cinematic
        yield return StartCoroutine(FadeOutBlack());

        // Step 6: Wait for cinematic to finish or be skipped
        yield return new WaitUntil(() => videoFinished || cinematicSkipped);

        if (videoPlayer != null)
        {
            videoPlayer.Stop();
            videoPlayer.loopPointReached -= OnVideoFinished;
        }

        if (skipButton != null)
        {
            skipButton.onClick.RemoveAllListeners();
            skipButton.gameObject.SetActive(false);
        }

        if (cinematicCanvas != null)
            cinematicCanvas.gameObject.SetActive(false);

        if (cinematicCanvas != null)
            cinematicCanvas.gameObject.SetActive(false);

        // Resume music *after* cinematic but *before* dialogue
        if (menuMusic != null)
            menuMusic.Play();

        // Step 7:  Dialogue
        if (dialogueManager != null && introDialogue != null)
        {
            bool finished = false;
            dialogueManager.StartDialogue(introDialogue, () => finished = true);
            yield return new WaitUntil(() => finished);
        }

        // Step 8: Fade to black before loading next scene
        yield return StartCoroutine(FadeInBlack(1f));
        SceneManager.LoadScene("CoreScene");
    }

    private void OnVideoFinished(VideoPlayer vp)
    {
        videoFinished = true;
    }

    public void SkipCinematic()
    {
        cinematicSkipped = true;
    }

    private IEnumerator FadeInBlack(float duration = 1f)
    {
        if (fadePanelCanvasGroup == null) yield break;
        fadePanelCanvasGroup.gameObject.SetActive(true);
        float elapsed = 0f;

        while (elapsed < duration)
        {
            fadePanelCanvasGroup.alpha = Mathf.Lerp(0, 1, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        fadePanelCanvasGroup.alpha = 1f;
    }

    private IEnumerator FadeOutBlack(float duration = 1f)
    {
        if (fadePanelCanvasGroup == null) yield break;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            fadePanelCanvasGroup.alpha = Mathf.Lerp(1, 0, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        fadePanelCanvasGroup.alpha = 0f;
        fadePanelCanvasGroup.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainMenu")
        {
            FindReferences();
            if (mainMenuPanel != null)
                mainMenuPanel.SetActive(true);
            SetupStartButton();
        }
    }
}
