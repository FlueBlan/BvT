using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class TestSystem : MonoBehaviour
{
    [SerializeField] private HandView handView;
    [SerializeField] private List<CardData> cardDataList;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && cardDataList != null && cardDataList.Count > 0)
        {
            int randomIndex = Random.Range(0, cardDataList.Count);
            CardData randomCardData = cardDataList[randomIndex];
            Card card = new(randomCardData);
            CardView cardFace = DeckBuilder.Instance.CreateCardView(card, transform.position, Quaternion.identity);
            StartCoroutine(handView.AddCard(cardFace));
        }
    }
}
