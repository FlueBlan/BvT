using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 100f;
    public GameObject deathEffect;
    public AudioClip deathSFX;

    public void TakeDamage(float damage)
    {
        Debug.Log($"{gameObject.name} took {damage} damage. Remaining: {health - damage}");
        health -= damage;

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
