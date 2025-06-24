using UnityEngine;
using UnityEngine.UI;

public class LevelCountdownBar : MonoBehaviour
{
    public Slider levelSlider;
    public float levelDuration = 60f;
    private float currentTime;

    public Image handleIcon;
    public Sprite startIcon;
    public Sprite endIcon;

    private void Start()
    {
        currentTime = 0;
        levelSlider.minValue = 0;
        levelSlider.maxValue = levelDuration;
        levelSlider.value = 0;

        if (handleIcon != null && startIcon != null)
        {
            handleIcon.sprite = startIcon;
        }
    }

    void Update()
    {

        if (PauseManager2.IsPaused) return;
        if (currentTime < levelDuration)
        {
            currentTime += Time.deltaTime;
            levelSlider.value = currentTime;
        }

        if (currentTime >= levelDuration && handleIcon.sprite != endIcon)
        {
            handleIcon.sprite = endIcon;
        }
    }

    public void ResetSlider()
    {
        currentTime = 0;
        levelSlider.value = 0;

        if (handleIcon != null && startIcon != null)
        {
            handleIcon.sprite = startIcon;
        }
    }
}
