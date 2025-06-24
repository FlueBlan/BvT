using UnityEngine;
using System.Collections;

public class GreenBean : MonoBehaviour
{
    public float healRange = 30f; // Range at which GreenBean heals other beans
    public float healAmount = 10f; // Amount of health healed per second
    public float healDuration = 3f; // How long the healing lasts
    public float cooldownTime = 5f; // Cooldown time between healing
    public AudioClip healSound;

    private float lastHealTime = 0f;

    void Update()
    {
        // Perform healing if cooldown is over
        if (Time.time >= lastHealTime + cooldownTime)
        {
            HealBeansInRange();
        }
    }

    void HealBeansInRange()
    {
        SoundManager.instance.PlaySound(healSound);
        // Find all beans within range
        Collider[] beansInRange = Physics.OverlapSphere(transform.position, healRange);

        foreach (Collider collider in beansInRange)
        {
            if (collider.CompareTag("Bean") && collider.gameObject != this.gameObject)
            {
                // Heal the beans
                Health beanHealth = collider.GetComponent<Health>();
                if (beanHealth != null)
                {
                    StartCoroutine(HealBean(beanHealth)); // Heal the bean over time
                }
            }
        }

        lastHealTime = Time.time; // Reset cooldown after healing
        StartCoroutine(DissappearAfterHealing()); // Start disappearing after healing
    }

    IEnumerator HealBean(Health beanHealth)
    {
        float elapsed = 0f;
        while (elapsed < healDuration)
        {
            beanHealth.Heal(healAmount * Time.deltaTime); // Heal over time
            elapsed += Time.deltaTime;
            yield return null;
        }
    }

    // Coroutine for disappearing after healing
    IEnumerator DissappearAfterHealing()
    {
        // Wait for the healing to complete (after healDuration)
        yield return new WaitForSeconds(healDuration);

        // Destroy or deactivate the GreenBean
        Destroy(gameObject);
    }
}
