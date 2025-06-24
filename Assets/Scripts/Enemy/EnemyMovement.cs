using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 2f;
    public float damageAmount = 25f;  // Set your damage here
    private Vector3 targetPosition;
    private bool hasTarget = false;
    private float fixedY;

    private bool isAttacking = false;
    private Coroutine attackCoroutine;
    private AudioSource audioSource;

    void Start()
    {
        fixedY = transform.position.y;
        
    }

  

    void Update()
    {

        if (PauseManager2.IsPaused) return;
        if (GameManager.instance != null && GameManager.instance.isPaused)
            return;

        if (!hasTarget || isAttacking) return;

        Vector3 moveTarget = new Vector3(targetPosition.x, fixedY, targetPosition.z);
        transform.position = Vector3.MoveTowards(transform.position, moveTarget, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, moveTarget) < 0.1f)
        {
            PlayerStats.Lives--;
            Destroy(gameObject);
        }
    }

    public void SetTarget(Vector3 position)
    {
        targetPosition = position;
        hasTarget = true;
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
        if (other.CompareTag("Bean") && !isAttacking)
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
        isAttacking = true;
        attackCoroutine = StartCoroutine(AttackRoutine(bean));
    }

    void StopAttacking()
    {
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
        }

        isAttacking = false;
    }

    public AudioClip attackSound; // assign in Inspector

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

        isAttacking = false;
    }


}
