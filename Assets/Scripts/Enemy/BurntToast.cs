using UnityEngine;

public class BurntToast : BaseEnemyHealth
{
    public float shieldHealth = 150f;
    private bool shieldBroken = false;
    public override void TakeDamage(float amount)
    {
        if (!shieldBroken)
        {
            shieldHealth -= amount;
            Debug.Log("Hit");
            if (shieldHealth <= 0)
            {
                shieldBroken = true;
                Debug.Log("Shield Broken!");
            }
            return; // Don't deal damage to health yet
        }
        
        base.TakeDamage(amount);
    }
}
