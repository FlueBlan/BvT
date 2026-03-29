using UnityEngine;
using DG.Tweening;

public class DeckBuilder : Singleton<DeckBuilder>
{
    [SerializeField] private CardView cardPrefab;
    [SerializeField] private Vector3 cardScale = new Vector3(.1f, .1f, .1f);

    public CardView CreateCardView(Card card, Vector3 position, Quaternion rotation)
    {
        CardView carVe = Instantiate(cardPrefab, position, rotation);
        carVe.transform.localScale = cardScale; // Adjust scale
        carVe.transform.DOScale(cardScale, 0.15f);
        carVe.Setup(card);
        return carVe;
    }
}
