using UnityEngine;
using TMPro;

public class MoneyUi : MonoBehaviour
{
    public TMP_Text moneyText;
    public AudioSource audioSource;
    public AudioClip coinCollectSound;

    private float lastMoneyAmount;

    void Start()
    {
        lastMoneyAmount = PlayerStats.Money;
        UpdateMoneyText();
    }

    void Update()
    {
        if (PlayerStats.Money != lastMoneyAmount)
        {
            // If money increased, play the coin sound
            if (PlayerStats.Money > lastMoneyAmount && audioSource != null && coinCollectSound != null)
            {
                audioSource.PlayOneShot(coinCollectSound);
            }

            lastMoneyAmount = PlayerStats.Money;
            UpdateMoneyText();
        }
    }

    void UpdateMoneyText()
    {
        moneyText.text = PlayerStats.Money.ToString();
    }
}
