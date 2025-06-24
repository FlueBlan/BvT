using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class Level2Manager : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public Dialogue[] dialogueSteps;
    public GameObject gachaPanel;
    public GameObject levelCompletePanel;
    public Dialogue level2IntroDialogue;
    public WaveSpawner waveSpawner;
    public Button healerBeanShopButton;
    public Button popcornBombShopButton;





    // UI
    public Image canImage;
    public Image cardImage;
    public TextMeshProUGUI gachaText;
    public Button pullCardButton;
    public Button useCardButton;
    public Sprite healerBeanCardSprite;

    private CanvasGroup gachaCanvasGroup;
    private CanvasGroup canCanvasGroup;
    private CanvasGroup cardCanvasGroup;

    void Start()
    {
        Debug.Log("Level2Manager Start() called!");

        gachaCanvasGroup = gachaPanel.GetComponent<CanvasGroup>();
        canCanvasGroup = canImage.GetComponent<CanvasGroup>();
        cardCanvasGroup = cardImage.GetComponent<CanvasGroup>();

        if (!gachaCanvasGroup || !canCanvasGroup || !cardCanvasGroup)
        {
            Debug.LogError("Missing CanvasGroup(s)! Ensure all necessary UI elements have CanvasGroup components.");
            return;
        }

        cardImage.gameObject.SetActive(false);
        useCardButton.gameObject.SetActive(false);

        gachaCanvasGroup.alpha = 0f;
        gachaPanel.SetActive(false);

        StartCoroutine(RunLevelSequence());
    }

    private IEnumerator RunLevelSequence()
    {
        yield return StartCoroutine(dialogueManager.StartDialogueRoutine(dialogueSteps[0]));
        yield return StartCoroutine(WaitForWave3ThenShowDialogue());
        yield return StartCoroutine(FadeInGachaPanel());
        yield return StartCoroutine(dialogueManager.StartDialogueRoutine(dialogueSteps[2]));

        // Popcorn part moved to UseCard()
    }





    private IEnumerator WaitForWave3ThenShowDialogue()
    {
        while (waveSpawner.CurrentWave < 3 || waveSpawner.IsWaveInProgress)
        {
            yield return null;
        }

        PauseManager2.PauseGame();
        yield return StartCoroutine(dialogueManager.StartDialogueRoutine(dialogueSteps[1]));
    }

    private IEnumerator FadeInGachaPanel()
    {
        gachaPanel.SetActive(true);
        float duration = 1f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            gachaCanvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsed / duration);
            yield return null;
        }

        gachaCanvasGroup.alpha = 1f;
    }


    public void PullGachaCard()
    {
        pullCardButton.interactable = false;
        StartCoroutine(FadeCanToCard());
    }

    private IEnumerator FadeCanToCard()
    {
        // Ensure both are active
        canImage.gameObject.SetActive(true);
        cardImage.gameObject.SetActive(true);

        // Start states
        canCanvasGroup.alpha = 1f;
        cardCanvasGroup.alpha = 0f;

        float duration = 1f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            canCanvasGroup.alpha = Mathf.Lerp(1f, 0f, t);
            cardCanvasGroup.alpha = Mathf.Lerp(0f, 1f, t);

            yield return null;
        }

        canCanvasGroup.alpha = 0f;
        cardCanvasGroup.alpha = 1f;

        canImage.gameObject.SetActive(false);

        gachaText.text = "Healer Bean";
        useCardButton.gameObject.SetActive(true);
        pullCardButton.gameObject.SetActive(false);
    }

    public void UseCard()
    {
        Debug.Log("Healer Bean Card Used!");
        StartCoroutine(FadeOutGachaPanel());

        // Unlock the Healer Bean in the shop
        if (healerBeanShopButton != null)
        {
            healerBeanShopButton.interactable = true;
        }
        else
        {
            Debug.LogWarning("Healer Bean shop button is not assigned in the Inspector.");
        }

        // Give 150 crumbs to the player
        PlayerStats.Money += 150;


        // Start the next step after fade
        StartCoroutine(WaitForGreenBeanThenUnlockPopcorn());
    }

    private IEnumerator WaitForGreenBeanThenUnlockPopcorn()
    {
        while (GameObject.FindWithTag("GreenBean") == null)
        {
            yield return null;
        }

        yield return new WaitForSeconds(1f);

        // Show the first 3 lines of popcorn dialogue
        yield return StartCoroutine(dialogueManager.StartDialogueRoutine(dialogueSteps[3]));

        // Unlock Popcorn Bomb after line 3
        if (popcornBombShopButton != null)
        {
            popcornBombShopButton.interactable = true;
            Debug.Log("Popcorn Bomb Unlocked!");
        }

        // Show the rest of the popcorn dialogue
        yield return StartCoroutine(dialogueManager.StartDialogueRoutine(dialogueSteps[4]));

        yield return new WaitForSeconds(15f);
        levelCompletePanel.SetActive(true);
    }




    private IEnumerator FadeOutGachaPanel()
    {
        float duration = 1f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsed / duration);
            gachaCanvasGroup.alpha = alpha;
            yield return null;
        }

        gachaCanvasGroup.alpha = 0f;
        gachaPanel.SetActive(false);

        // Unpause the game after the gacha panel disappears
        PauseManager2.ResumeGame();
    }

    public void UnlockPopcornBomb()
    {
        // Unlock the Popcorn Bomb card
        if (popcornBombShopButton != null)
        {
            popcornBombShopButton.interactable = true;

            // Give the player 200 crumbs
            PlayerStats.Money += 200;
        }
        else
        {
            Debug.LogWarning("Popcorn Bomb shop button is not assigned!");
        }

    }



    private IEnumerator WaitForDialogueToFinish()
    {
        while (DialogueManager.instance.IsDialogueRunning())
        {
            yield return null;
        }
    }
}
