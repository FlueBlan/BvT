using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class CardView : MonoBehaviour
{
    [SerializeField] private TMP_Text cardCost;
    [SerializeField] private SpriteRenderer cardBG;
    [SerializeField] private GameObject wrapper;
    [SerializeField] private float YOffset = 1f;
    public Card Card { get; private set; }
    private BuildManager buildManager;
    void Start()
    {
        buildManager = BuildManager.instance;
    }
    public void Setup(Card card)
    {
        Card = card;
        cardCost.text = card.Cost.ToString();
        cardBG.sprite = card.Image;
    }
    void OnMouseEnter()
    {
        wrapper.SetActive(false);
        Vector3 pos = transform.position + new Vector3(0, YOffset, 0);
        HoverSystem.Instance.Show(Card, pos);
    }
    void OnMouseExit()
    {
        HoverSystem.Instance.Hide();
        wrapper.SetActive(true);
    }
    void OnMouseDown()
    {
        buildManager.SelectBeanToBuild(Card.BeanPrefab, this);
    }
}
