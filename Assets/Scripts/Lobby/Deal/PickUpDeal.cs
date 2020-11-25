using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpDeal : MonoBehaviour
{
    public CardView cardView1;
    public CardView cardView2;
    public CardView cardView3;
    public PickCardSelector selector1;
    public PickCardSelector selector2;
    public PickCardSelector selector3;

    int c1 = 10;
    int c2 = 11;
    int c3 = 12;


    private void Awake()
    {
        selector1.SetCardView(cardView1);
        selector1.SetDeal(this);
        selector2.SetCardView(cardView2);
        selector2.SetDeal(this);
        selector3.SetCardView(cardView3);
        selector3.SetDeal(this);
    }

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
        LobbyManager.instance.AddCardToDeck(result);


        LobbyManager.instance.deal.Close();
        gameObject.SetActive(false);
    }

}
