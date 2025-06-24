using UnityEngine;
using System.Collections;

public class GachaManager : MonoBehaviour
{
    public static GachaManager Instance;
    public GameObject gachaPanel;
    private CanvasGroup gachaCanvasGroup;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        if (gachaPanel != null)
        {
            gachaCanvasGroup = gachaPanel.GetComponent<CanvasGroup>();
            gachaCanvasGroup.alpha = 0f;
            gachaPanel.SetActive(false);
        }
        else
        {
            Debug.LogError("Gacha Panel not assigned in GachaManager.");
        }
    }

    public void FadeInGacha()
    {
        if (gachaPanel != null)
        {
            gachaPanel.SetActive(true);
            StartCoroutine(FadeIn());
        }
    }

    private IEnumerator FadeIn()
    {
        float duration = 1f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            gachaCanvasGroup.alpha = Mathf.Lerp(0, 1, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        gachaCanvasGroup.alpha = 1f;
    }
}
