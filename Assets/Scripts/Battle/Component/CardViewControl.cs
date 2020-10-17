using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardViewControl : MonoBehaviour
{
    public float handCardDistance;

    public GameObject cardViewObject;
    public Transform handCardPlace;
    public Transform cardViewpool;

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

        cardView.transform.position = handCardPlace.position;

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

        closeCardView.transform.position = cardViewpool.transform.position;
        //closeCardView.SetPos(cemeteryTransform.transform.position);
    }

    public void HandCardPosUpdate()
    {
        int i = 0;
        foreach (CardView cv in handCard)
        {

            Vector3 pos = new Vector2(handCardPlace.transform.position.x + handCardDistance * i, handCardPlace.transform.position.y);
            cv.SetPos(pos);
            i++;
        }
    }

}
