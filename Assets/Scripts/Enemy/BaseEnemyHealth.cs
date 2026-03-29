using UnityEngine;

public class BaseEnemyHealth : MonoBehaviour
{
    [SerializeField] private float health = 100f;
    public float reward = 25f;
    public GameObject deathEffect;
    //public AudioClip deathSFX;
    private DamageFlash df;
    void Awake()
    {
        df = GetComponent<DamageFlash>();
    }
    public virtual void TakeDamage(float damage)
    {
        Debug.Log($"{gameObject.name} took {damage} damage. Remaining: {health - damage}");
        health -= damage;
        df.Flash();

        if (health <= 0)
        {
            Die();
        }
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
