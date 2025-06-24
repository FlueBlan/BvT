using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CountdownText : MonoBehaviour
{
    public Text countdownText;  // Reference to the UI Text for the countdown
    public CanvasGroup canvasGroup; // Reference to the CanvasGroup for fading
    public float scaleDuration = 0.5f;  // Duration for the scaling effect
    public float fadeDuration = 0.5f;   // Duration for the fade effect
    public string[] countdownMessages = { "Restarting in 3...", "2...", "1..." };

    private void Start()
    {
        StartCoroutine(ShowCountdown());
    }

    // Coroutine to handle countdown and animations
    private IEnumerator ShowCountdown()
    {
        for (int i = 0; i < countdownMessages.Length; i++)
        {
            // Show the message
            countdownText.text = countdownMessages[i];

            // Reset and animate fade in and scale up
            yield return StartCoroutine(AnimateText(true));

            // Wait before starting the next countdown number
            yield return new WaitForSeconds(1f);

            // Animate fade out and scale down for transition
            yield return StartCoroutine(AnimateText(false));

            // Optionally, wait before showing the next message
            yield return new WaitForSeconds(0.5f);
        }
    }

    // Coroutine to animate the text scale and alpha fade
    private IEnumerator AnimateText(bool fadeIn)
    {
        float targetAlpha = fadeIn ? 1f : 0f;
        float targetScale = fadeIn ? 1.2f : 1f;  // Scaling up if fade-in, scaling down if fade-out
        float timeElapsed = 0f;

        // Animate alpha fade and scaling
        while (timeElapsed < fadeDuration)
        {
            float t = timeElapsed / fadeDuration;
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, targetAlpha, t);
            countdownText.transform.localScale = Vector3.Lerp(countdownText.transform.localScale, new Vector3(targetScale, targetScale, 1), t);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // Ensure it reaches the final values
        canvasGroup.alpha = targetAlpha;
        countdownText.transform.localScale = new Vector3(targetScale, targetScale, 1);
    }
}
