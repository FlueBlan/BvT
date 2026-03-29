using UnityEngine;

public class EnemyHealth : BaseEnemyHealth
{
    private BaseEnemyHealth beh;
    
    void Awake()
    {
        beh = GetComponent<BaseEnemyHealth>();
    }
    protected override void Die()
    {
        base.Die();
        FindFirstObjectByType<TutorialManager>()?.OnToastDefeated();
    }
}
