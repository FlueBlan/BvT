using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDatabase : MonoBehaviour
{
    public static List<Card> cards = new List<Card>();

    void Awake()
    {
        cardList.Add(new Card(0, "None", 0, 0));
        cardList.Add(new Card(1, "Wizard Bean", 50, 15));
        cardList.Add(new Card(2, "Fighter Bean", 100, 30));
        cardList.Add(new Card(3, "Healer Bean", 0, 10));
        cardList.Add(new Card(4, "Popcorn Bomb", 0, 200));
    }
}
