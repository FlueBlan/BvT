using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 100f;  // Maximum health
    public float currentHealth;     // Current health
    //private AudioSource audioSource;


    void Start()
    {
        currentHealth = maxHealth;
    }


    // Method to take damage
    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    // Method to heal (add health)
    public void Heal(float amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth; // Cap health at max
        }
    }

    // Method to handle death
    //public AudioClip deathSound; // assign in Inspector

    private void Die()
    {
        //if (deathSound != null)
            //SoundManager.instance.PlaySound(deathSound); 

        Debug.Log($"{gameObject.name} has died.");
        Destroy(gameObject);
    }
}
