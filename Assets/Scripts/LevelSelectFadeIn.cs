using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelSelectFadeIn : MonoBehaviour
{
    public CanvasGroup fadePanelCanvasGroup;

    private void Start()
    {
        if (fadePanelCanvasGroup != null)
            StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        float duration = 1f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            fadePanelCanvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        fadePanelCanvasGroup.alpha = 0f;
        fadePanelCanvasGroup.gameObject.SetActive(false);
    }
}
