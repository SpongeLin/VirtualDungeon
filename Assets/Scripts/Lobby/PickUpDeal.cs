using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpDeal : MonoBehaviour
{
    public CardView cardView1;
    public CardView cardView2;
    public CardView cardView3;

    int c1 = 10;
    int c2 = 11;
    int c3 = 12;

    public void StartPickUp()
    {
        c1 = CardCreator.GetRandomNormalCard();
        c2 = CardCreator.GetRandomNormalCard();
        c3 = CardCreator.GetRandomNormalCard();

        cardView1.SetCard(CardCreator.CreateCard(c1));
        cardView2.SetCard(CardCreator.CreateCard(c2));
        cardView3.SetCard(CardCreator.CreateCard(c3));

    }
    public void Click(int result)
    {
        if (result == 1)
        {
            GameData.instance.deck.Add(c1);
        }else if (result == 2)
        {
            GameData.instance.deck.Add(c2);
        }
        else if (result == 3)
        {
            GameData.instance.deck.Add(c3);
        }

        LobbyManager.instance.deal.Close();
    }
}
