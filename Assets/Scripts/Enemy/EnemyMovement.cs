using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 2f;
    public bool isAttacking { get; set; }
    private Vector3 targetPosition;
    private bool hasTarget = false;
    private float fixedY;
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

}
