using UnityEngine;
using System.Collections;

public class EnemyAttackHitbox : MonoBehaviour
{
    public float damageAmount = 25f;
    //public AudioClip attackSound;
    private Coroutine attackCoroutine;
    private EnemyMovement enemyMov; // Reference to parent
    private GameObject currentBean;

    void Awake()
    {
        enemyMov = GetComponentInParent<EnemyMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bean"))
        {
            currentBean = other.gameObject;
            StartAttacking();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Bean") && !enemyMov.isAttacking)
        {
            currentBean = other.gameObject;
            StartAttacking();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Bean") && other.gameObject == currentBean)
        {
            StopAttacking();
            currentBean = null;
        }
    }

    void StartAttacking()
    {
        if (currentBean == null)
            return;

        if (enemyMov.isAttacking)
            return;

        enemyMov.isAttacking = true;
        attackCoroutine = StartCoroutine(AttackRoutine(currentBean));
    }

    void StopAttacking()
    {
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
            attackCoroutine = null;
        }

        enemyMov.isAttacking = false;
    }

    IEnumerator AttackRoutine(GameObject bean)
    {
        Health beanHealth = bean.GetComponent<Health>();

        while (bean != null && beanHealth != null && beanHealth.currentHealth > 0)
        {
            //if (attackSound != null && SoundManager.instance != null)
                //SoundManager.instance.PlaySound(attackSound);

            beanHealth.TakeDamage(damageAmount);
            yield return new WaitForSeconds(1f);
        }

        enemyMov.isAttacking = false;
    }
}
