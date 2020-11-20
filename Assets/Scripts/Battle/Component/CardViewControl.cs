using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardViewControl : MonoBehaviour
{
    public float handCardDistance;

    public GameObject cardViewObject;
    public Transform handCardPlace;
    public Transform handCardEndPos;
    public Transform cardViewpool;

    public Transform useLine;
    public Transform deckPlace;
    public Transform cemeteryPlace;

    public List<CardView> disableCard;
    public List<CardView> handCard;


    private void Awake()
    {
        disableCard = new List<CardView>();
        handCard = new List<CardView>();
        for (int i = 0; i < 35; i++)
        {
            GameObject cardView = Instantiate(cardViewObject, cardViewpool);
            cardView.transform.localPosition = Vector3.zero;
            disableCard.Add(cardView.GetComponent<CardView>());
        }
    }



    public void GameUpdate()
    {
        foreach (CardView cv in handCard)
            cv.GameUpdate();
    }
    public void OpenCard(CardData card)
    {
        CardView cardView = null;
        foreach (CardView cv in disableCard)
        {
            if (!cv.enable)
            {
                cardView = cv;
                break;
            }
        }
        if (!cardView) Debug.LogError("OpenCard : can't find cardView gameobject");

        cardView.SetCard(card);
        handCard.Add(cardView);
        disableCard.Remove(cardView);

        cardView.transform.position = deckPlace.position;
        HandCardPosUpdate();
        return;
        /*
        int i = 0;
        foreach(CardView cv in handCard)
        {
            if (cv == cardView)
            {
                cardView.SetPos(GetHandCardPos(i), true);
                break;
            }
            i++;
        }
        */

    }
    public void CloseCard(CardData card)
    {
        CardView closeCardView = null;
        foreach (CardView cv in handCard)
        {
            if (cv.card == card)
            {
                closeCardView = cv;
                break;
            }
        }
        if (!closeCardView) Debug.LogError("CloseHandCard : can't find cardView gameobject");

        closeCardView.ClearCard();
        disableCard.Add(closeCardView);
        handCard.Remove(closeCardView);

        //closeCardView.transform.position = cardViewpool.transform.position;
        closeCardView.SetPos(cardViewpool.transform.position);
        //closeCardView.SetPos(cemeteryTransform.transform.position);
    }

    public void HandCardPosUpdate()
    {
        int i = 0;
        foreach (CardView cv in handCard)
        {

            Vector3 pos = GetHandCardPos(i);
            cv.SetPos(pos,true,true);
            i++;
        }
        //Debug.Log(handCardPlace.transform.position.z);
    }
    Vector3 GetHandCardPos(int i)
    {
        float dis = handCardEndPos.transform.position.x - handCardPlace.transform.position.x;
        //Debug.Log(dis);
        dis = dis / handCard.Count;

        return new Vector3(handCardPlace.transform.position.x + dis * i, handCardPlace.transform.position.y, handCardPlace.transform.position.z);
    }

}
