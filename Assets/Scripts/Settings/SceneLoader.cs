using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void StartTraining()
    {
        SceneManager.LoadScene("Level1Tutorial"); 
    }
}