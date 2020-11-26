using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventCardControl : MonoBehaviour
{
    int row = 3;
    public FogEventCard fog;
    public List<Sprite> eventImages;
    List<EventCard> eventCards;

    private void Awake()
    {
        eventCards = new List<EventCard>();
        eventCards.AddRange(GetComponentsInChildren<EventCard>());
        foreach (EventCard ec in eventCards)
            ec.SetControl(this);


    }
    public void Start()
    {
        foreach(EventCard ec in eventCards)
        {
            EventCardDataPack dp = GetEventCardData(ec.eventCardNum);
            if (dp != null)
            {
                ec.SetEventCard(GetEventSprite(dp.eventType), dp.eventType, dp.eventContent);
                if (dp.used)
                {
                    ec.SetClick(false);
                    ec.ShowTip();
                }
                else
                {
                    ec.SetClick(true);
                }
            }
        }

        int currentNum = GameData.instance.eventCurrentNum * row;
        foreach(EventCard ec in eventCards)
        {
            if (ec.eventCardNum < currentNum)
                ec.ShowEvent();
        }
        int fogNum = GameData.instance.eventFogNum * row;
        foreach (EventCard ec in eventCards)
        {
            if (ec.eventCardNum < fogNum)
                ec.SetClick(false);
        }
        //Set Fog Pos
        SetFogPos();

        LobbyManager.instance.FogCounterRun();
    }

    public void EventUpdate(int currentEventCardNum)
    {
        AfterEventCardUseCheck(currentEventCardNum);

        int temp = GameData.instance.eventCurrentNum * row - currentEventCardNum;
        if (temp > 0 && temp <= 3) Go();

        if (GameData.instance.fogBeforeNum != 0)
        {
            GameData.instance.fogBeforeNum --;
            LobbyManager.instance.FogCounterRun();
        }
        else
        {
            Fog();
        }
        LobbyManager.instance.isWorling = true;

    }
    void Go()
    {
        GameData.instance.eventCurrentNum++;
        int currentNewNum = GameData.instance.eventCurrentNum * row;
        foreach (EventCard ec in eventCards)
        {
            int temp = currentNewNum - ec.eventCardNum;
            if (temp > 0 && temp <= 3) 
            {
                ec.ShowEvent(true);
            }
        }
    }
    void Fog(bool anima = false)
    {
        GameData.instance.eventFogNum++;
        int currentFogNum = GameData.instance.eventFogNum * row;
        foreach (EventCard ec in eventCards)
        {
            int temp = currentFogNum - ec.eventCardNum;
            if (temp > 0 && temp <= 3)
            {
                ec.SetClick(false);
            }
        }
        //anima!!
        SetFogPos(true);

    }
    void SetFogPos(bool anim = false)
    {
        int fogPosNum = GameData.instance.eventFogNum - 1;
        fogPosNum = fogPosNum * row;

        EventCard eCard = null;
        foreach (EventCard ec in eventCards)
        {
            if (ec.eventCardNum == fogPosNum)
            {
                eCard = ec;
                break;
            }
        }
        if (eCard != null)
        {
            fog.SetFogPos(eCard.transform.position, anim);
        }
    }
    void AfterEventCardUseCheck(int eventCardNum)
    {
        if (LobbyManager.instance.currentEventCardBeNotUse)
        {
            LobbyManager.instance.currentEventCardBeNotUse = false;
            return;
        }

        EventCard eCard = null;
        foreach(EventCard ec in eventCards)
        {
            if(ec.eventCardNum == eventCardNum)
            {
                eCard = ec;
                break;
            }
        }
        if (eCard == null)
        {
            Debug.LogWarning("Found not eventcard");
            return;
        }

        eCard.ShowTip();
        eCard.SetClick(false);
        GetEventCardData(eventCardNum).used = true;

    }

    EventCardDataPack GetEventCardData(int num)
    {
        foreach (EventCardDataPack ecdp in GameData.instance.eventCards)
            if (ecdp.eventCardNum == num)
                return ecdp;
        return null;
    }
    Sprite GetEventSprite(int type)
    {
        if (type > eventImages.Count)
            return null;
        return eventImages[type - 1];
    }
    public void ClickEvent(EventCard ec)
    {
        //Debug.Log(ec.eventCardNum);
        switch (ec.eventType)
        {
            case 1:
                LobbyManager.instance.GoBattle(ec.eventContent,ec.eventCardNum);
                break;
            case 2:
                LobbyManager.instance.OpenGameEvent("Treasure", ec.eventCardNum);
                break;
            case 3:
                LobbyManager.instance.OpenGameEvent("HotSpring", ec.eventCardNum);
                break;
            case 4:
                LobbyManager.instance.OpenGameEvent("Bed", ec.eventCardNum);
                break;
            case 5:
                LobbyManager.instance.OpenShop(ec.eventCardNum);
                break;
            case 6:
                EventUpdate(ec.eventCardNum);
                break;
            case 7:
                LobbyManager.instance.TestGoToEnd();
                break;
        }

    }
}
