using UnityEngine;

public class BeanFighter : MonoBehaviour
{
    public float attackRange = 8f;  // Range at which Fighter Bean attacks
    public float damage = 25f;  // Fighter Bean's damage per attack
    public string enemyTag = "Enemy";  // Tag for the enemy
    public float attackCooldown = 5f; // Time between attacks
    private float lastAttackTime = 0f; // Time of the last attack
    public AudioClip punchSound;

    private Transform target;
    private float fixedY;

    void Start()
    {
        fixedY = transform.position.y; // Keep the Y position fixed
        InvokeRepeating(nameof(FindClosestEnemy), 0f, 0.5f);  // Find the nearest enemy every 0.5 seconds
    }

    void Update()
    {
        if (target == null) return;

        // Look at target (rotate to face enemy)
        Vector3 direction = target.position - transform.position;
        direction.y = 0f; // Don't rotate along the Y-axis
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            lookRotation *= Quaternion.Euler(0f, 90f, 0f); // Adjust for orientation
            transform.rotation = lookRotation;
        }

        // Check if the target is within attack range
        float distance = Vector3.Distance(transform.position, target.position);
        if (distance <= attackRange && Time.time >= lastAttackTime + attackCooldown)
        {
            SoundManager.instance.PlaySound(punchSound);
            // Attack the enemy
            BaseEnemyHealth targetHealth = target.GetComponent<BaseEnemyHealth>()
                                     ?? target.GetComponentInParent<BaseEnemyHealth>()
                                     ?? target.GetComponentInChildren<BaseEnemyHealth>();

            if (targetHealth != null)
            {
                targetHealth.TakeDamage(damage);
                lastAttackTime = Time.time; // Reset cooldown timer
            }
        }
    }

    void FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        if (enemies.Length == 0)
        {
            return; // No enemies found, skip
        }

        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null)
        {
            target = nearestEnemy.transform;
        }
    }
}
