using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DealControl : MonoBehaviour
{
    public GameObject black;
    public PickUpDeal pickUpDeal;
    public PickCardDeal pickCardDeal;

    string eventCardCallBack;

    public void StartPickUp(string callBack = "")
    {
        black.SetActive(true);
        pickUpDeal.gameObject.SetActive(true);
        pickUpDeal.StartPickUp();

        eventCardCallBack = callBack;
    }
    public void StartPickCard(string pickContent, string callBack = "")
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
        eventCardCallBack = callBack;
    }
    public void StartPickChar(string pickContent)
    {

    }

    public void Close()
    {
        black.SetActive(false);


        if (eventCardCallBack == "EventCard")
        {
            LobbyManager.instance.EventCardCallBack();
            eventCardCallBack = "";
        }
        if (eventCardCallBack == "GameEvent")
        {
            LobbyManager.instance.gameEvent.Next();
            eventCardCallBack = "";
        }
    }

}
