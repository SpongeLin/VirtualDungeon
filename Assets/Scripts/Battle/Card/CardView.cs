using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class CardView : MonoBehaviour,IPointerDownHandler
{
    public bool enable = false;

    public Image cardBack;
    public Image cardImage;
    public Text cost;
    public Text description;

    public CardData card;

    //Tween tween = null;
    public bool isSelected;


    public void SetCard(CardData setCard)
    {
        card = setCard;
        cardImage.sprite = card.cardImage;
        description.text = card.cardDescription;
        cost.text = card.cardCost.ToString();

        enable = true;
        isSelected = false;
    }
    public void ClearCard()
    {
        enable = false;
        card = null;
        //cardImage.sprite = null;

    }
    public void SetPos(Vector3 pos)
    {
        //StopTween();
        //tween = transform.DOMove(pos, CardManager.instance.cardSpeed).SetEase(Ease.OutQuad);
        transform.position = pos;
    }
    void StopTween()
    {
        /*
        if (tween != null)
            if (tween.IsPlaying())
                tween.Kill();
         */
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        if (!enable) return;
        //Debug.Log(card.cardName + " down now  ");
        CardManager.instance.cardViewControl.MouseDownInCard(this);
    }
}
