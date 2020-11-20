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
    public Text cardName;

    public CardData card;
    public bool CanUseHighLight;
    public GameObject highLight;

    //Tween tween = null;
    Tween scaleTween = null;
    Tween posTween = null;

    public int costStatus = 0;
    ViewMode viewMode = ViewMode.Battle;


    public void SetCard(CardData setCard)
    {
        card = setCard;
        cardImage.sprite = card.cardImage;
        cardName.text = card.cardShowName;
        description.text = card.cardDescription;
        cost.text = card.cardCost.ToString();

        enable = true;
    }
    public void ClearCard()
    {
        enable = false;
        card = null;
        //cardImage.sprite = null;

        CanUseHighLight = false;
        highLight.SetActive(false);

        cost.color = Color.white;
        costStatus = 0;

    }
    public void GameUpdate()
    {
        description.text = card.cardDescription;
        if (card.burst >= 1)
            description.text += "\n<color=#FF0000>強化：" + card.burst.ToString()+ "</color>";
        if (card.linkChar != null)
            description.text += "\n<color=#0000E3>連結：" + card.linkChar.charShowName.ToString() + "</color>";

        cost.text = card.cardCost.ToString();

        if (!CanUseHighLight)
        {
            if (card.canUse)
            {
                highLight.SetActive(true);
                CanUseHighLight = true;
            }
        }
        else
        {
            if (!card.canUse)
            {
                highLight.SetActive(false);
                CanUseHighLight = false;
            }
        }

        if ((card.oriCost == card.cardCost && costStatus != 0) || (card.oriCost > card.cardCost && costStatus != -1) || (card.oriCost < card.cardCost && costStatus != 1))
        {
            if (card.oriCost == card.cardCost)
            {
                costStatus = 0;
                cost.color = Color.white;
            }
            else if (card.oriCost > card.cardCost)
            {
                costStatus = -1;
                cost.color = Color.green;
            }
            else if (card.oriCost < card.cardCost)
            {
                costStatus = 1;
                cost.color = Color.red;
            }
        }

    }


    public void SetPos(Vector3 pos,bool anim=false,bool fast=false)
    {
        //StopTween();
        //tween = transform.DOMove(pos, CardManager.instance.cardSpeed).SetEase(Ease.OutQuad);
        if (posTween != null)
        {
            posTween.Kill();
            posTween = null;
        }
        if (anim)
        {
            float cardSpeed = fast ? 0.5f : 0.6f;
            posTween = transform.DOMove(pos, cardSpeed).SetEase(Ease.OutQuad);
        }
        else
        {
            transform.position = pos;
        }
    }
    public void SetSize(Vector3 size,bool anim=false)
    {
        if (scaleTween != null)
        {
            scaleTween.Kill();
            scaleTween = null;
        }
        if (anim)
        {
            scaleTween = transform.DOScale(size, 0.15f);
        }
        else
        {
            transform.localScale = size;
        }

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
        if (viewMode == ViewMode.Battle)
        {
            if (!enable) return;
            if (!card.canUse) return;
            //Debug.Log(card.cardName + " down now  ");

            CardManager.instance.MouseDownCard(this);
        }
        else
        {

        }
    }
    public void LobbyMode()
    {
        viewMode = ViewMode.Lobby;
    }

    enum ViewMode
    {
        Battle,
        Lobby
    }
}
