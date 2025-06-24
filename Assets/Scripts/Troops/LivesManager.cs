using UnityEngine;
using UnityEngine.UI;

public class LivesManager : MonoBehaviour
{
    public int lives = 5;  // Start with 5 lives
    public Text livesText;  // Reference to the UI Text for displaying lives

    // This method will be called when an enemy reaches the end
    public void LoseLife()
    {
        lives--;  // Decrease lives by 1
        if (lives <= 0)
        {
            GameOver();  // Trigger game over if lives reach 0
        }
        UpdateLivesUI();  // Update the UI with the new number of lives
    }

    // Updates the UI text to reflect the current number of lives
    void UpdateLivesUI()
    {
        livesText.text = "Lives: " + lives.ToString();
    }

    // Call this method when game is over
    void GameOver()
    {
        // Implement game over logic here (e.g., show a game over screen)
        Debug.Log("Game Over!");
    }
}
