using UnityEngine;

public class Orb : MonoBehaviour
{
    private Transform target;
    public float speed = 70f;
    public GameObject impactEffect;
    private float damage;
    public AudioClip shootSound;
    public void SetDamage(float _damage)
    {
        damage = _damage;
    }



    public void Seek(Transform _target)
    {
        target = _target;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    void HitTarget()
    {
        
        GameObject effectIns = Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectIns, 2f);

        if (target != null)
        {
            Debug.Log("Hit target: " + target.name);
            
            // Try getting EnemyHealth from the target or its parent
            BaseEnemyHealth enemyHealth = target.GetComponent<BaseEnemyHealth>() ?? target.GetComponentInParent<BaseEnemyHealth>();

            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
            else
            {
                Debug.LogWarning("Target has no EnemyHealth component!");
            }
        }

        Destroy(gameObject);
    }





}
