using UnityEngine;

[CreateAssetMenu(menuName = "Data/Card")]
public class CardData : ScriptableObject
{
    [field: SerializeField] public float Cost { get; private set; }
    [field: SerializeField] public Sprite Image { get; private set; }
}
