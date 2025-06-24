using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelSelectManager : MonoBehaviour
{
    public GameObject level2LockImage;
    public GameObject level3LockImage;
    public Button[] levelButtons;
    public Button backButton;
    public CanvasGroup fadePanel;
    public Dialogue Level1Complete;
    public Dialogue Level2Complete; 
    void Start()
    {
        Debug.Log("showAfterLevel1Dialogue = " + GameStateManager.instance.showAfterLevel1Dialogue);
        Debug.Log("showAfterLevel2Dialogue = " + GameStateManager.instance.showAfterLevel2Dialogue);

        // Check if Level 1 was completed and show dialogue
        if (GameStateManager.instance.showAfterLevel1Dialogue)
        {
            GameStateManager.instance.showAfterLevel1Dialogue = false;

            if (DialogueManager.instance != null && DialogueManager.instance.dialoguePanel != null)
            {
                DialogueManager.instance.dialoguePanel.SetActive(true);
                DialogueManager.instance.StartDialogue(Level1Complete);
            }
            else
            {
                Debug.LogError("DialogueManager or its dialoguePanel is not assigned!");
            }
        }

        // ✅ Check if Level 2 was completed and show new dialogue
        if (GameStateManager.instance.showAfterLevel2Dialogue)
        {
            GameStateManager.instance.showAfterLevel2Dialogue = false;

            if (DialogueManager.instance != null && DialogueManager.instance.dialoguePanel != null)
            {
                DialogueManager.instance.dialoguePanel.SetActive(true);
                DialogueManager.instance.StartDialogue(Level2Complete);
            }
            else
            {
                Debug.LogError("DialogueManager or its dialoguePanel is not assigned!");
            }
        }

        // Lock/unlock level buttons
        if (level2LockImage != null)
            level2LockImage.SetActive(!LevelManager.instance.IsLevelUnlocked(1));
        if (level3LockImage != null)
            level3LockImage.SetActive(!LevelManager.instance.IsLevelUnlocked(2));

        levelButtons[1].interactable = LevelManager.instance.IsLevelUnlocked(1);
        levelButtons[2].interactable = LevelManager.instance.IsLevelUnlocked(2);

        // Level button listeners
        levelButtons[0].onClick.AddListener(() => LoadLevel("Level1"));
        levelButtons[1].onClick.AddListener(() => LoadLevel("Level2"));
        levelButtons[2].onClick.AddListener(() => LoadLevel("FinalCutscene"));


        // Back button
        if (backButton != null)
        {
            backButton.onClick.AddListener(() => LoadLevel("MainMenu"));
        }
        else
        {
            Debug.LogWarning("Back button is not assigned in the Inspector.");
        }
    }

    public void LoadLevel(string levelName)
    {
        StartCoroutine(FadeAndLoad(levelName));
    }

    private IEnumerator FadeAndLoad(string levelName)
    {
        fadePanel.gameObject.SetActive(true);
        float duration = 1f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            fadePanel.alpha = Mathf.Lerp(0f, 1f, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        fadePanel.alpha = 1f;
        SceneManager.LoadScene(levelName);
    }
}
