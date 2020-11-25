using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DealControl : MonoBehaviour
{
    public GameObject black;
    public PickUpDeal pickUpDeal;
    public PickCardDeal pickCardDeal;

    bool eventCardCallBack;

    public void StartPickUp(bool callBack = false)
    {
        black.SetActive(true);
        pickUpDeal.gameObject.SetActive(true);
        pickUpDeal.StartPickUp();

        eventCardCallBack = callBack;
    }
    public void StartPickCard(string pickContent)
    {
        if(pickContent == "Delete")
        {
            black.SetActive(true);
            pickCardDeal.gameObject.SetActive(true);
            pickCardDeal.StartPickCard(new DeleteCardExciteComponent());
        }
        if (pickContent == "Copy")
        {
            black.SetActive(true);
            pickCardDeal.gameObject.SetActive(true);
            pickCardDeal.StartPickCard(new CopyCardExciteComponent());
        }
    }
    public void StartPickChar(string pickContent)
    {

    }

    public void Close()
    {
        black.SetActive(false);


        if (eventCardCallBack)
            LobbyManager.instance.EventCardCallBack();
        eventCardCallBack = false;
    }

}
