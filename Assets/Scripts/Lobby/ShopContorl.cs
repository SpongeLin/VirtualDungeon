using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopContorl : MonoBehaviour
{
    public GameObject shopWindow;

    public CardView card1;
    public CardView card2;
    public CardView card3;
    int price1;
    int price2;
    int price3;

    public int refreshCost;

    public Button refreshButton;
    public Text refreshText;

    public Button button1;
    public Button button2;
    public Button button3;
    public Text buttonText1;
    public Text buttonText2;
    public Text buttonText3;

    string shopCallBack;

    public void StartShop(string callBack="")
    {
        shopCallBack = callBack;

        LobbyManager.instance.SetBlackForEventCard(true);
        shopWindow.SetActive(true);
        refreshText.text = "重新整理  " + refreshCost.ToString() + "$";
        Refresh();
    }
    public void Close()
    {
        LobbyManager.instance.SetBlackForEventCard(false);
        shopWindow.SetActive(false);

        if (shopCallBack == "EventCard")
        {
            LobbyManager.instance.EventCardCallBack();
            shopCallBack = "";
        }
        if (shopCallBack == "GameEvent")
        {
            LobbyManager.instance.gameEvent.Next();
            shopCallBack = "";
        }
    }
    public void RefreshButton()
    {
        LobbyManager.instance.LoseMoney(refreshCost);
        Refresh();
    }
    public void Click(int num)
    {
        if (num == 1)
        {
            LobbyManager.instance.LoseMoney(price1);
            LobbyManager.instance.AddCardToDeck(card1.card.cardNo);
            buttonText1.text = "已販售";
        }
        if (num == 2)
        {
            LobbyManager.instance.LoseMoney(price2);
            LobbyManager.instance.AddCardToDeck(card2.card.cardNo);
            buttonText2.text = "已販售";
        }
        if (num == 3)
        {
            LobbyManager.instance.LoseMoney(price3);
            LobbyManager.instance.AddCardToDeck(card3.card.cardNo);
            buttonText3.text = "已販售";
        }
        UpdateGood();
    }

    void Refresh()
    {
        int cardNo = CardCreator.GetRandomNormalCard();
        card1.SetCard(CardCreator.CreateCard(cardNo));
        int money = Random.Range(50, 80);
        price1 = money;
        buttonText1.text = price1.ToString() + "$";

        cardNo = CardCreator.GetRandomNormalCard();
        card2.SetCard(CardCreator.CreateCard(cardNo));
        money = Random.Range(50, 80);
        price2 = money;
        buttonText2.text = price2.ToString() + "$";

        cardNo = CardCreator.GetRandomNormalCard();
        card3.SetCard(CardCreator.CreateCard(cardNo));
        money = Random.Range(50, 80);
        price3 = money;
        buttonText3.text = price3.ToString() + "$";

        UpdateGood();

    }
    void UpdateGood()
    {
        if (GameData.instance.money < price1)
            button1.interactable = false;
        else
            button1.interactable = true;

        if (GameData.instance.money < price2)
            button2.interactable = false;
        else
            button2.interactable = true;

        if (GameData.instance.money < price3)
            button3.interactable = false;
        else
            button3.interactable = true;

        if (GameData.instance.money < refreshCost)
            refreshButton.interactable = false;
        else
            refreshButton.interactable = true;
    }

}
