using TMPro;
using UnityEngine;

public class LivesUI : MonoBehaviour
{
    public TMP_Text livesText;  // Reference to your TMP_Text UI element

    void Update()
    {
        // Update the UI text to reflect the current number of lives
        livesText.text = PlayerStats.Lives.ToString();
    }
}
