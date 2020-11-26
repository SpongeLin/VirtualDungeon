using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData instance { get; private set; }
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            TestChar();
        }
        else
        {
            gameObject.SetActive(false);
            return;
        }
        deck = new List<int>();
        //Test
        /*
        deck.Add(1);
        deck.Add(1);
        deck.Add(2);
        deck.Add(5);
        deck.Add(5);
        deck.Add(2);
        deck.Add(3);
        deck.Add(4);
        */
        deck.Add(15);
        deck.Add(22);
        deck.Add(25);
        //deck.Add(10);
        //deck.Add(11);
        deck.Add(24);
        deck.Add(13);
        deck.Add(13);
        deck.Add(14);
        deck.Add(23);
        deck.Add(32);
        deck.Add(30);

        handCardNum = 4;
        //===
        //front = new CharacterDataPack("", 0, 0,0);
        //middle = new CharacterDataPack("", 0, 0,0);
        //back = new CharacterDataPack("", 0, 0,0);
        eventCards = new List<EventCardDataPack>();
        eventCards.Add(new EventCardDataPack(0, 1, ""));
        eventCards.Add(new EventCardDataPack(1, 1, ""));
        eventCards.Add(new EventCardDataPack(2, 1, ""));
        eventCards.Add(new EventCardDataPack(3, 1, ""));
        eventCards.Add(new EventCardDataPack(4, 2, ""));
        eventCards.Add(new EventCardDataPack(5, 3, ""));
        eventCards.Add(new EventCardDataPack(6, 2, ""));
        eventCards.Add(new EventCardDataPack(7, 1, ""));
        eventCards.Add(new EventCardDataPack(8, 1, ""));
        eventCards.Add(new EventCardDataPack(9, 1, ""));
        eventCards.Add(new EventCardDataPack(10, 5, ""));
        eventCards.Add(new EventCardDataPack(11, 4, ""));
        eventCards.Add(new EventCardDataPack(12, 3, ""));
        eventCards.Add(new EventCardDataPack(13, 1, ""));
        eventCards.Add(new EventCardDataPack(14, 4, ""));
        eventCards.Add(new EventCardDataPack(15, 7, "", true));
        fogBeforeNum = 2;
        eventCurrentNum = 1;

        preEventCardNum = -1;
    }

    public List<int> deck;
    public int handCardNum;
    public int money;

    public bool battleResult;
    public bool moneyReward;

    public CharacterDataPack front;
    public CharacterDataPack middle;
    public CharacterDataPack back;

    public CharacterDataPack frontEnemy;
    public CharacterDataPack middleEnemy;
    public CharacterDataPack backEnemy;

    public List<EventCardDataPack> eventCards;
    public int eventCurrentNum;
    public int eventFogNum;
    public int fogBeforeNum;

    public int preEventCardNum;

    public void TestChar()
    {
        front = new CharacterDataPack("Iro", 0, 0, 64, 0);
        middle = new CharacterDataPack("Nao", 0, 0, 100, 0);
        back = new CharacterDataPack("ShouMe", 0, 0, 75, 0);

        frontEnemy = new CharacterDataPack("NovitiateKnight", 0, 0, 75, 0);
        middleEnemy = new CharacterDataPack("NovitiateMage", 0, 0, 60, 0);
        backEnemy = new CharacterDataPack("NovitiateLancer", 0, 0, 70, 0);
    }
}

public class CharacterDataPack
{
    public CharacterDataPack(string name, int cd1, int cd2,int maxHp,int hp)
    {
        heroName = name;
        skillCD1 = cd1;
        skillCD2 = cd2;
        maxHealth = maxHp;
        currentHealth = hp == 0 ? maxHp : hp;
    }
    public string heroName;
    public int skillCD1;
    public int skillCD2;

    public int currentHealth;
    public int maxHealth;
    public int power;
    public int magic;
    public int armor;
    public int agility;
    public int maxEnergy;

    public void FullHealth()
    {
        currentHealth = maxHealth;
    }
}

public class EventCardDataPack
{
    public EventCardDataPack(int num, int type, string content,bool _bool = false)
    {
        eventCardNum = num;
        eventType = type;
        eventContent = content;
        eventBool = _bool;
    }
    public int eventCardNum;
    public int eventType;
    public string eventContent;
    public bool eventBool;

    public bool used;
}