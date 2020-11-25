using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PickCardSelector : MonoBehaviour,IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
{
    public Vector3 oriSize;

    CardView cardView = null;
    PickCardDeal pickCardDeal = null;
    PickUpDeal pickUpDeal = null;


    public void SetCardView(CardView view)
    {
        cardView = view;
    }
    public void SetDeal(PickUpDeal pu)
    {
        pickUpDeal = pu;
        pickCardDeal = null;
    }
    public void SetDeal(PickCardDeal pc)
    {
        pickCardDeal = pc;
        pickUpDeal = null;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (pickCardDeal != null)
            pickCardDeal.Click(cardView.card);

        if (pickUpDeal != null)
            pickUpDeal.Click(cardView.card.cardNo);
        Debug.Log("SELECTOR");
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        Vector3 newPos = new Vector3(oriSize.x*1.2f, oriSize.y* 1.2f, 1);
        cardView.SetSize(newPos, true);

        /*
        if (pickCardDeal != null)
            pickCardDeal.SelectorBeEnter(this);

        if (pickUpDeal != null)
            pickUpDeal.SelectorBeEnter(this);
        */
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //SizeToNormal();
        cardView.SetSize(oriSize, true);
    }

}
