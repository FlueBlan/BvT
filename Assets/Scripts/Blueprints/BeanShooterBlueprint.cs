using UnityEngine;

[CreateAssetMenu(fileName = "New Bean Blueprint", menuName = "Bean/BeanShooterBlueprint")]
public class BeanShooterBlueprint : ScriptableObject
{
    public GameObject prefab;
    public int cost;
    public Vector3 spawnOffset = Vector3.zero;
    public Vector3 rotationEuler = Vector3.zero;
}
