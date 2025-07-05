using UnityEngine;
public class Card
{
    public float Cost { get; private set; }
    public Sprite Image => data.Image;
    private readonly CardData data;
    public Card(CardData cardData)
    {
        data = cardData;
        Cost = cardData.Cost;
    }
}
