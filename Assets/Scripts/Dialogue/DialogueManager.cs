using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    public TextMeshProUGUI dialogueText;
    public GameObject dialoguePanel;
    private CanvasGroup canvasGroup;
    private Queue<string> sentences = new Queue<string>();
    private bool isDialogueActive = false;
    private bool isNextPressed = false;

    public float fadeDuration = 0.5f;

    public delegate void DialogueFinishedHandler();
    public static event DialogueFinishedHandler OnDialogueFinished;

    void Awake()
    {
        instance = this;
        if (dialogueText == null) Debug.LogError("DialogueManager: dialogueText is not assigned!");
        if (dialoguePanel == null) Debug.LogError("DialogueManager: dialoguePanel is not assigned!");

        canvasGroup = dialoguePanel.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            Debug.LogError("DialogueManager: dialoguePanel needs a CanvasGroup component!");
        }
    }

    // Start dialogue with an optional callback when finished
    public void StartDialogue(Dialogue dialogue, System.Action onComplete = null)
    {
        StartCoroutine(StartDialogueCoroutine(dialogue, onComplete));
    }

    private IEnumerator StartDialogueCoroutine(Dialogue dialogue, System.Action onComplete)
    {
        isDialogueActive = true;
        dialoguePanel.SetActive(true);
        canvasGroup.alpha = 1f;  // Ensure the panel is visible

        sentences.Clear();
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        // Show first sentence
        if (sentences.Count > 0)
        {
            string firstSentence = sentences.Dequeue();
            dialogueText.text = firstSentence;

            yield return null;
            isNextPressed = false;
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space) || isNextPressed);
        }

        // Loop through remaining sentences
        while (sentences.Count > 0)
        {
            string sentence = sentences.Dequeue();
            dialogueText.text = sentence;

            isNextPressed = false;
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space) || isNextPressed);
        }

        // Fade out and complete dialogue
        yield return StartCoroutine(FadeOutAndEndDialogue());
        onComplete?.Invoke();  // Callback when finished
    }

    // Call this method when the user presses Next (e.g., spacebar)
    public void Next()
    {
        isNextPressed = true;
    }

    // End the dialogue and call the event
    private IEnumerator FadeOutAndEndDialogue()
    {
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
            yield return null;
        }

        EndDialogue();
    }

    // End the dialogue and hide the panel
    public void EndDialogue()
    {
        isDialogueActive = false;
        dialoguePanel.SetActive(false);
        OnDialogueFinished?.Invoke();  // Invoke the event after the dialogue finishes
    }

    // Check if dialogue is still running
    public bool IsDialogueRunning()
    {
        return isDialogueActive;
    }
}
