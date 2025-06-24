using UnityEngine;

public class Shop : MonoBehaviour
{
    public BeanShooterBlueprint wizardbean;
    public BeanShooterBlueprint fighterbean;
    public BeanShooterBlueprint greenbean;
    public BeanShooterBlueprint popcornBomb;

    [Header("SFX")]
    public AudioSource audioSource;
    public AudioClip selectCardSFX;

    private BuildManager buildManager;

    void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void SelectWizardBean()
    {
        Debug.Log("Wizard Bean Selected");
        buildManager.SelectBeanToBuild(wizardbean);
        PlaySelectSound();
    }

    public void SelectBuffBean()
    {
        Debug.Log("Buff Bean Selected");
        buildManager.SelectBeanToBuild(fighterbean);
        PlaySelectSound();
    }

    public void SelectGreenBean()
    {
        Debug.Log("Green Bean Selected");
        buildManager.SelectBeanToBuild(greenbean);
        PlaySelectSound();
    }

    public void SelectPopcornBomb()
    {
        Debug.Log("Popcorn Bomb Selected");
        buildManager.SelectBeanToBuild(popcornBomb);
        PlaySelectSound();
    }

    private void PlaySelectSound()
    {
        if (audioSource != null && selectCardSFX != null)
        {
            audioSource.PlayOneShot(selectCardSFX);
        }
    }
}
