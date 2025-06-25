using UnityEngine;
using System.Collections;

public class EnemyAttackHitbox : MonoBehaviour
{
    public float damageAmount = 25f;
    public AudioClip attackSound;
    private Coroutine attackCoroutine;
    private EnemyMovement enemyMov; // Reference to parent

    void Awake()
    {
        enemyMov = GetComponentInParent<EnemyMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bean"))
        {
            Health beanHealth = other.GetComponent<Health>();
            if (beanHealth != null)
            {
                StartAttacking(other.gameObject);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Bean") && !enemyMov.isAttacking)
        {
            StartAttacking(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Bean"))
        {
            StopAttacking();
        }
    }

    void StartAttacking(GameObject bean)
    {
        enemyMov.isAttacking = true;
        attackCoroutine = StartCoroutine(AttackRoutine(bean));
    }

    void StopAttacking()
    {
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
        }
        enemyMov.isAttacking = false;
    }

    IEnumerator AttackRoutine(GameObject bean)
    {
        Health beanHealth = bean.GetComponent<Health>();

        while (bean != null && beanHealth != null && beanHealth.currentHealth > 0)
        {
            if (attackSound != null)
                SoundManager.instance.PlaySound(attackSound);

            beanHealth.TakeDamage(damageAmount);
            yield return new WaitForSeconds(1f);
        }

        enemyMov.isAttacking = false;
    }
}
