using UnityEngine;

public class BaseEnemyHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;

    public float reward = 25f;
    public GameObject deathEffect;

    //public AudioClip deathSFX;
    private DamageFlash df;

    void Awake()
    {
        currentHealth = maxHealth;
        df = GetComponent<DamageFlash>();
    }

    public virtual void TakeDamage(float damage)
    {
        currentHealth -= damage;
        df.Flash();

        Debug.Log($"{gameObject.name} took {damage} damage. Remaining: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void SetHealth(float newHealth)
    {
        maxHealth = newHealth;
        currentHealth = maxHealth;
    }

    protected virtual void Die()
    {
        PlayerStats.Money += reward;

        // Play death sound
        //if (deathSFX != null)
            //SoundManager.instance.PlaySound(deathSFX);

        // Visual effect
        if (deathEffect != null)
        {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, 5f);
        }

        Destroy(gameObject);
    }
}
