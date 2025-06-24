using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public Button continueButton;
    public Button FighterBeanitem;
    public WaveSpawner waveSpawner;
    public Node beanPlacement;
    public LevelCountdownBar timerScript;
    public WaveTimer timerText;
    public Button wizardBeanButton;



    private int stepIndex = 0;
    private bool waitingForPlayerAction = false;

    [TextArea(3, 10)]
    public string[] tutorialSteps;

    void Start()
    {
        PauseGameplay();
        ShowNextStep();


    }

    public void OnContinueClicked()
    {
        if (waitingForPlayerAction) return;

        stepIndex++;
        ShowNextStep();
    }

    void ShowNextStep()
    {
        if (stepIndex >= tutorialSteps.Length)
        {
            EndTutorial();
            return;
        }

        dialoguePanel.SetActive(true);
        dialogueText.text = tutorialSteps[stepIndex];

        // Hide continue button by default each step
        continueButton.gameObject.SetActive(true);
        waitingForPlayerAction = false;



        if (tutorialSteps[stepIndex].Contains("-"))
        {
            UnlockWizardBean();
        }

        // Special condition: wait for enemy defeat
        if (tutorialSteps[stepIndex].ToLower().Contains("*wait for toast to die*"))
        {
            waitingForPlayerAction = true;
            continueButton.gameObject.SetActive(false); // hide button until toast dies
        }

        if (tutorialSteps[stepIndex].Contains("*unlock fighter bean*"))
        {
            UnlockFighterBean();
        }
        if (tutorialSteps[stepIndex].Contains("*wait for toast to die*"))
        {
            waitingForPlayerAction = true;
        }

        if (tutorialSteps[stepIndex].Contains("*unlock fighter bean*"))
        {
            UnlockFighterBean();
        }

        if (tutorialSteps[stepIndex].Contains("*start toast wave*"))
        {
            ResumeGameplay();
        }
    }
    void UnlockFighterBean()
    {
        FighterBeanitem.interactable = true;
        Debug.Log("Fighter Bean unlocked!");

    }
    void UnlockWizardBean()
    {
        wizardBeanButton.interactable = true;
        Debug.Log("Wizard Bean unlocked!");
    }

    // Call this from your enemy script when a toast dies
    public void OnToastDefeated()
    {
        if (waitingForPlayerAction)
        {
            waitingForPlayerAction = false;
            continueButton.gameObject.SetActive(true);
        }
    }
    void PauseGameplay()
    {
        // Disable enemy spawning, bean placement, etc.
        WaveSpawner.instance.enabled = false;
        Node[] allNodes = Object.FindObjectsByType<Node>(FindObjectsSortMode.None);


        if (timerScript != null) timerScript.enabled = false;
        if (timerText != null) timerText.enabled = false;
    }

    void ResumeGameplay()
    {
        WaveSpawner.instance.enabled = true;
        Node[] allNodes = Object.FindObjectsByType<Node>(FindObjectsSortMode.None);
        if (timerScript != null) timerScript.enabled = true;
        if (timerText != null) timerText.enabled = true;
    }

    void EndTutorial()
    {
        dialoguePanel.SetActive(false);
        Debug.Log("Tutorial Finished!");
    }

}