using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameEventData
{
    public string eventName;
    public string eventDescription;
    public string eventImage;

    public  GameEventSelection selection1;
    public GameEventSelection selection2;

    public GameEventData nextEvent;
}
public static class EventCreator
{
    public static GameEventData CreateGameEvent(string eventName)
    {
        GameEventData data = new GameEventData();
        data.eventImage = eventName;
        switch (eventName)
        {
            case "Bed":
                data.eventName = "莫名其妙的床";
                data.eventDescription = "在前進的路上發現了一張床，非常突兀的放在遺忘迴廊的一處，如果睡上去會發生什麼事情呢？如果什麼都沒發生的話，大概是忘了裝補丁吧。但如果能入睡的話，可能會對記憶會有些幫助也說不定。";
                data.selection1 = new nGameEventSelection.CopyeCard();
                data.selection2 = new nGameEventSelection.DeleteCard();
                break;
            case "HotSpring":
                data.eventName = "溫泉";
                data.eventDescription = "可以幫助你回復生命的溫泉，沒什麼特別的。";
                data.selection1 = new nGameEventSelection.AllHeroHealFull();
                data.selection2 = new nGameEventSelection.Nothing();
                break;
            case "Treasure":
                data.eventName = "寶藏！";
                data.eventDescription = "路邊的寶箱，身為勇者就是要開啦。";
                data.selection1 = new nGameEventSelection.PickUpOneCard();
                data.selection2 = new nGameEventSelection.Nothing();
                break;

            default:
                return null;
        }


        return data;
    }
}

public abstract class GameEventSelection {
    public string selectionContent;

    public abstract void SelectionExcite();
}
namespace nGameEventSelection
{
    public class DeleteCard : GameEventSelection
    {
        public DeleteCard()
        {
            selectionContent = "刪除一張卡";
        }
        public override void SelectionExcite()
        {
            LobbyManager.instance.deal.StartPickCard("Delete", "GameEvent");
        }
    }
    public class CopyeCard : GameEventSelection
    {
        public CopyeCard()
        {
            selectionContent = "複製一張卡";
        }
        public override void SelectionExcite()
        {
            LobbyManager.instance.deal.StartPickCard("Copy", "GameEvent");
        }
    }
    public class Nothing : GameEventSelection
    {
        public Nothing()
        {
            selectionContent = "我才不要";
        }
        public override void SelectionExcite()
        {
            LobbyManager.instance.currentEventCardBeNotUse = true;
            LobbyManager.instance.gameEvent.Next();
        }
    }
    public class AllHeroHealFull : GameEventSelection
    {
        public AllHeroHealFull()
        {
            selectionContent = "所有英雄生命回滿";
        }
        public override void SelectionExcite()
        {
            LobbyManager.instance.HealAllHeroFull();
            LobbyManager.instance.gameEvent.Next();
        }
    }
    public class PickUpOneCard : GameEventSelection
    {
        public PickUpOneCard()
        {
            selectionContent = "獲得一張卡牌";
        }
        public override void SelectionExcite()
        {
            LobbyManager.instance.deal.StartPickUp("GameEvent");
        }
    }
}