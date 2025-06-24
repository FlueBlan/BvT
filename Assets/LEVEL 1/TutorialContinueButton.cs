using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialContinueButton : MonoBehaviour
{
    public void OnContinueClicked()
    {
        PlayerPrefs.SetInt("ShowLevelSelect", 1); // Tell MainMenuManager to show level select next time
        PlayerPrefs.Save();
        SceneManager.LoadScene("MainMenu");
    }
}
