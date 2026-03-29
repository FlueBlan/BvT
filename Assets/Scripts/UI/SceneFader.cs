using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine;

public class SceneFader : MonoBehaviour
{
    public CanvasGroup fadeGroup;
    public float fadeDuration = 1f;

    public void FadeToScene(string sceneName, bool showLevelSelectAfter = false)
    {
        StartCoroutine(FadeAndLoad(sceneName, showLevelSelectAfter));
    }

    IEnumerator FadeAndLoad(string sceneName, bool showLevelSelectAfter)
    {
        fadeGroup.blocksRaycasts = true;

        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            fadeGroup.alpha = Mathf.Lerp(0, 1, timer / fadeDuration);
            yield return null;
        }

        if (showLevelSelectAfter)
        {
            PlayerPrefs.SetInt("ShowLevelSelect", 1);
        }

        SceneManager.LoadScene(sceneName);
    }
}
