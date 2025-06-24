using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    // Singleton instance for easy access
    public static PlayerInputManager Instance;

    private bool isInputEnabled = true;  // Track if input is enabled

    void Awake()
    {
        Instance = this;
    }

    // Call this method to enable or disable input
    public void SetInputEnabled(bool enabled)
    {
        isInputEnabled = enabled;
    }

    void Update()
    {
        if (!isInputEnabled) return;  // If input is disabled, exit early

        // Handle player input here (e.g., movement, placement, etc.)
        // Example:
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     // Action logic
        // }
    }
}
