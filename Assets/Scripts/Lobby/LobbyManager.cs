using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    public static LobbyManager instance { get; private set; }

    public LobbyHubControl hub;
    public EventCardControl eventCardControl;
    public DealControl deal;
    public DeckDisplay deckDisplay;

    public GameEventContorl gameEvent;
    public ShopContorl shop;

    public GameObject blackForEventCard;

    public bool isWorling;

    public List<CardData> decksData;


    public bool currentEventCardBeNotUse = false;

    public void Awake()
    {
        instance = this;
        decksData = new List<CardData>();
        foreach(int cardNo in GameData.instance.deck)
        {
            decksData.Add(CardCreator.CreateCard(cardNo));
        }
    }

    public void Start()
    {
        hub.LobbyStart();

        if (GameData.instance.battleResult)
        {
            deal.StartPickUp("EventCard");
            GameData.instance.money += 35;
            hub.MoneyUpdate();
            //GameData.instance.money += GameData.instance.moneyReward;
        }
        else
        {
            EventCardCallBack();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            GoBattle("");
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            deal.StartPickUp();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            deal.StartPickCard("Delete");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            shop.StartShop();
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            TakeMoney(30);
        }
    }

    public void AddCardToDeck(int cardNo)
    {
        GameData.instance.deck.Add(cardNo);
        decksData.Add(CardCreator.CreateCard(cardNo));
    }
    public void DeleteCardInDeck(CardData card)
    {
        GameData.instance.deck.Remove(card.cardNo);
        decksData.Remove(card);
    }
    public void TakeMoney(int money)
    {
        GameData.instance.money += money;
        hub.MoneyUpdate();
    }
    public void LoseMoney(int loseMoney)
    {
        GameData.instance.money -= loseMoney;
        hub.MoneyUpdate();
    }
    public void HealAllHeroFull()
    {
        GameData.instance.front.FullHealth();
        GameData.instance.middle.FullHealth();
        GameData.instance.back.FullHealth();
        hub.LobbyUpdate();
    }



    public void DeckDisplayOpen()
    {
        deckDisplay.Open();
    }
    public void GoBattle(string enemy,int preECNum=-1)
    {
        GameData.instance.frontEnemy = new CharacterDataPack("NovitiateKnight", 0, 0, 75, 0);
        GameData.instance.middleEnemy = new CharacterDataPack("NovitiateMage", 0, 0, 60, 0);
        GameData.instance.backEnemy = new CharacterDataPack("NovitiateLancer", 0, 0, 70, 0);
        //temp
        if (preECNum != -1)
        {
            SetPreEventCard(preECNum);
        }

        SceneManager.LoadScene("Battle", LoadSceneMode.Single);
    }
    public void OpenShop(int preECNum=-1)
    {
        if (preECNum != -1)
        {
            SetPreEventCard(preECNum);
            shop.StartShop("EventCard");
            return;
        }
        shop.StartShop();
    }
    public void OpenShop(bool eventNext)
    {
        if (eventNext)
        {
            shop.StartShop("GameEvent");
        }
    }
    public void OpenGameEvent(string eventName, int preECNum = -1)
    {
        gameEvent.StartEvent(eventName);
        if (preECNum != -1)
        {
            SetPreEventCard(preECNum);
        }
    }


    public void EventCardCallBack()
    {
        if (GameData.instance.preEventCardNum == -1) return;
        eventCardControl.EventUpdate(GameData.instance.preEventCardNum);

        GameData.instance.preEventCardNum = -1;
    }
    public void SetBlackForEventCard(bool active)
    {
        blackForEventCard.SetActive(active);
    }
    public void SetPreEventCard(int ecNum)
    {
        GameData.instance.preEventCardNum = ecNum;
    }
    public void FogCounterRun()
    {
        hub.FogCounterRun();
    }



    public void TestGoToEnd()
    {
        SceneManager.LoadScene("End", LoadSceneMode.Single);
    }
}
