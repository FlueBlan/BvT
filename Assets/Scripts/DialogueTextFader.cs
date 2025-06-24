using UnityEngine;
using TMPro;
using System.Collections;

public class DialogueTextFader : MonoBehaviour
{
    public float fadeDuration = 0.5f;

    private TextMeshProUGUI dialogueText;
    private string previousText = "";

    void Awake()
    {
        dialogueText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (dialogueText.text != previousText)
        {
            StopAllCoroutines();
            StartCoroutine(FadeInText());
            previousText = dialogueText.text;
        }
    }

    IEnumerator FadeInText()
    {
        Color c = dialogueText.color;
        c.a = 0f;
        dialogueText.color = c;

        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            c.a = Mathf.Clamp01(elapsed / fadeDuration);
            dialogueText.color = c;
            yield return null;
        }
    }
}
