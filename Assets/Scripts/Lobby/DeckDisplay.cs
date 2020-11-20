using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckDisplay : MonoBehaviour
{
    public GameObject cardViewObject;
    public Transform content;

    public GameObject displayer;

    List<CardView> cards;

    public void Awake()
    {
        cards = new List<CardView>();
        for (int i = 0; i < 60; i++)
        {
            GameObject go = Instantiate(cardViewObject, content);
            go.SetActive(false);
            cards.Add(go.GetComponent<CardView>());
        }
    }
    public void Open()
    {
        displayer.SetActive(true);
        foreach (CardView card in cards)
            card.gameObject.SetActive(false);
        int i = 0;
        foreach(int cardNo in GameData.instance.deck)
        {
            cards[i].gameObject.SetActive(true);
            cards[i].SetCard(CardCreator.CreateCard(cardNo));
            i++;
        }
    }
    public void Close()
    {
        displayer.SetActive(false);
    }


}
