using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 100f;
    public GameObject deathEffect;
    public AudioClip deathSFX;
    private DamageFlash df;

    void Awake()
    {
        df = GetComponent<DamageFlash>();
    }
    public void TakeDamage(float damage)
    {
        Debug.Log($"{gameObject.name} took {damage} damage. Remaining: {health - damage}");
        health -= damage;
        df.Flash(); // Flash the material to indicate damage

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        PlayerStats.Money += 25;

        // Play death sound
        if (deathSFX != null)
        {
            SoundManager.instance.PlaySound(deathSFX);
        }

        // Visual effect
        if (deathEffect != null)
        {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, 5f);
            FindFirstObjectByType<TutorialManager>()?.OnToastDefeated();
        }

        Destroy(gameObject);
    }
}
