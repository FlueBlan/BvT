using UnityEngine;
using DG.Tweening;

public class DeckBuilder : Singleton<DeckBuilder>
{
    [SerializeField] private CardView cardPrefab;

    public CardView CreateCardView(Card card, Vector3 position, Quaternion rotation)
    {
        CardView carVe = Instantiate(cardPrefab, position, rotation);
        carVe.transform.localScale = Vector3.zero;
        carVe.transform.DOScale(Vector3.one, 0.15f);
        carVe.Setup(card);
        return carVe;
    }
}
