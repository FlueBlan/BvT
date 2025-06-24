using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class EndCreditsManager : MonoBehaviour
{
    public Image fadePanel;
    public float fadeDuration = 2f;

    public TextMeshProUGUI thankYouText;
    public Button returnButton;

    void Start()
    {
        // Ensure initial visibility is correct
        thankYouText.gameObject.SetActive(false);
        returnButton.gameObject.SetActive(false);
        StartCoroutine(FadeInSequence());
    }

    IEnumerator FadeInSequence()
    {
        // Fade from black
        float timer = 0f;
        Color color = fadePanel.color;
        fadePanel.gameObject.SetActive(true);

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = 1f - (timer / fadeDuration);
            fadePanel.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }

        fadePanel.color = new Color(color.r, color.g, color.b, 0f);
        fadePanel.gameObject.SetActive(false);

        // Show Thank You message and return button
        thankYouText.gameObject.SetActive(true);
        returnButton.gameObject.SetActive(true);
    }

    public void OnReturnToMainMenu()
    {
        Time.timeScale = 1f;  // Just in case it's paused
        SceneManager.LoadScene("MainMenu");
    }
}
