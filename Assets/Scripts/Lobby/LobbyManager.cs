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

    public bool isWorling;

    public List<CardData> decksData;

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
            deal.StartPickUp(true);
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


    public void EventCardCallBack()
    {
        if (GameData.instance.preEventCardNum == -1) return;
        eventCardControl.EventUpdate(GameData.instance.preEventCardNum);

        GameData.instance.preEventCardNum = -1;
    }
    public void SetPreEventCard(int ecNum)
    {
        GameData.instance.preEventCardNum = ecNum;
    }
    public void FogCounterRun()
    {
        hub.FogCounterRun();
    }
}
