using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class Card : MonoBehaviour
{
    public int id;
    public string cardName;
    public int cost;
    public int atk;

    //void Card()
    //{

    //}
    public CardValues(int ID, string CardName, int Cost, int Atk)
    {
        id = ID;
        cardName = CardName;
        cost = Cost;
        atk = Atk;

        return;
    }

}
