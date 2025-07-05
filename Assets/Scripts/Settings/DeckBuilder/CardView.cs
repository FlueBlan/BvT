using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class CardView : MonoBehaviour
{
    [SerializeField] private TMP_Text cardCost;
    [SerializeField] private SpriteRenderer cardBG;
    [SerializeField] private GameObject wrapper;
    public Card Card { get; private set; }
    public void Setup(Card card)
    {
        Card = card;
        cardCost.text = card.Cost.ToString();
        cardBG.sprite = card.Image;
    }
    void OnMouseEnter()
    {
        wrapper.SetActive(false);
        Vector3 pos = new(transform.position.x, -2, 0);
        HoverSystem.Instance.Show(Card, pos);
    }
    void OnMouseExit()
    {
        HoverSystem.Instance.Hide();
        wrapper.SetActive(true);
    }
}
