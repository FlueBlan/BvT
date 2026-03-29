using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardCooldown : MonoBehaviour
{
    public float cooldownTime = 5f;
    private float cooldownTimer = 0f;
    private bool isCoolingDown = false;

    public Button cardButton;
    public Image cooldownOverlay;
    public TextMeshProUGUI cooldownText; // Assign in Inspector or auto-find below

    void Start()
    {
        if (cooldownText == null)
            cooldownText = GetComponentInChildren<TextMeshProUGUI>(true); // Find local text if not assigned

        cardButton.onClick.AddListener(StartCooldown);
        cooldownOverlay.fillAmount = 0f;
        cooldownText.text = "";
    }

    void Update()
    {
        if (isCoolingDown)
        {
            cooldownTimer -= Time.deltaTime;
            cooldownOverlay.fillAmount = cooldownTimer / cooldownTime;
            cooldownText.text = Mathf.Ceil(cooldownTimer).ToString();

            if (cooldownTimer <= 0f)
            {
                isCoolingDown = false;
                cardButton.interactable = true;
                cooldownOverlay.fillAmount = 0f;
                cooldownText.text = "";
            }
        }
    }

    public void StartCooldown()
    {
        if (isCoolingDown) return;

        isCoolingDown = true;
        cooldownTimer = cooldownTime;
        cardButton.interactable = false;
        cooldownOverlay.fillAmount = 1f;
        cooldownText.text = Mathf.Ceil(cooldownTimer).ToString();
    }
}
