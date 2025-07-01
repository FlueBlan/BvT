using UnityEngine;

public class EnemyHealth : BaseEnemyHealth
{
    private float curReward = 25f;
    private BaseEnemyHealth beh;
    
    void Awake()
    {
        beh = GetComponent<BaseEnemyHealth>();
    }
    protected override void Die()
    {
        beh.reward = curReward;
        base.Die();

        FindFirstObjectByType<TutorialManager>()?.OnToastDefeated();
    }
}
