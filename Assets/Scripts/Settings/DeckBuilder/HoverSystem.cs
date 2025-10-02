using UnityEngine;

public class HoverSystem : Singleton<HoverSystem>
{
    [SerializeField] private CardView cardViewHover;
    [SerializeField] private Vector3 hoverOffset = new Vector3(0, 0, 0);
    public void Show(Card card, Vector3 position)
    {
        cardViewHover.gameObject.SetActive(true);
        cardViewHover.Setup(card);
        cardViewHover.transform.position = position + hoverOffset;
    }

    public void Hide()
    {
        cardViewHover.gameObject.SetActive(false);
    }
}
