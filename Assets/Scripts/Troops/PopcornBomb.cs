using UnityEngine;

public class PopcornBomb : MonoBehaviour
{
    public float explosionRadius = 1f;
    public LayerMask enemyLayer;
    public GameObject explosionEffect;
    public AudioClip explosionSFX;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Explode(other.transform.position.x);
        }
    }

    void Explode(float rowX)
    {
        // Play explosion SFX
        if (explosionSFX != null)
        {
            SoundManager.instance.PlaySound(explosionSFX);
        }

        // Visual explosion effect
        if (explosionEffect != null)
        {
            GameObject explosion = Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Destroy(explosion, 4f);
        }

        // Destroy enemies in the same row
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            if (Mathf.Abs(enemy.transform.position.x - rowX) < 1f)
            {
                Destroy(enemy);
            }
        }

        Destroy(gameObject); // Destroy bomb itself
    }
}
