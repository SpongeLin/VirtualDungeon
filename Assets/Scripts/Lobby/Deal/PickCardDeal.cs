using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickCardDeal : MonoBehaviour
{
    public Text pickDescription;
    public GameObject cardViewObject;
    public Transform content;

    PickCardExciteComponent component = null;


    List<CardView> cards;

    public void Awake()
    {
        cards = new List<CardView>();
        for (int i = 0; i < 60; i++)
        {
            GameObject go = Instantiate(cardViewObject, content);
            CardView view = go.GetComponentInChildren<CardView>();
            cards.Add(view);
            PickCardSelector pcs = go.GetComponentInChildren<PickCardSelector>();
            pcs.SetCardView(view);
            pcs.SetDeal(this);

            
        }
    }

    public void StartPickCard(PickCardExciteComponent c)
    {
        component = c;
        pickDescription.text = component.componentDescription;

        foreach (CardView card in cards)
            card.transform.parent.gameObject.SetActive(false);
        int i = 0;
        foreach (CardData card in LobbyManager.instance.decksData)
        {
            cards[i].transform.parent.gameObject.SetActive(true);
            cards[i].SetCard(card);
            i++;
        }
    }
    public void Click(CardData card)
    {
        component.Excite(card);

        LobbyManager.instance.deal.Close();
        gameObject.SetActive(false);
    }
}


public abstract class PickCardExciteComponent
{
    public string componentDescription;
    public abstract void Excite(CardData card);
}
public class CopyCardExciteComponent : PickCardExciteComponent
{
    public CopyCardExciteComponent()
    {
        componentDescription = "【記憶重組】選擇一張卡，複製他";
    }
    public override void Excite(CardData card)
    {
        LobbyManager.instance.AddCardToDeck(card.cardNo);
    }
}
public class DeleteCardExciteComponent : PickCardExciteComponent
{
    public DeleteCardExciteComponent()
    {
        componentDescription = "【墜入深處】選擇一張卡，刪除他";
    }
    public override void Excite(CardData card)
    {
        LobbyManager.instance.DeleteCardInDeck(card);
    }
}