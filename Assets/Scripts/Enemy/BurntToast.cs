using UnityEngine;

public class BurntToast : MonoBehaviour
{
    public float shieldHealth = 30f;
    public float actualHealth = 100f;
    private bool shieldBroken = false;

    public void TakeDamage(float amount)
    {
        if (!shieldBroken)
        {
            shieldHealth -= amount;
            if (shieldHealth <= 0)
            {
                shieldBroken = true;
                Debug.Log("Shield Broken!");
            }
            return; // Don't deal damage to health yet
        }

        actualHealth -= amount;
        if (actualHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
        // Crumbs, death animation, etc.
    }
}
