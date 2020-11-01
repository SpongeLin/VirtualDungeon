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
        deck.Add(10);
        deck.Add(11);
        deck.Add(12);
        deck.Add(13);
        deck.Add(13);
        deck.Add(14);
        deck.Add(14);

        handCardNum = 4;
        //===
        front = new CharacterDataPack("", 0, 0,0);
        middle = new CharacterDataPack("", 0, 0,0);
        back = new CharacterDataPack("", 0, 0,0);
    }

    public List<int> deck;
    public int handCardNum;

    public CharacterDataPack front;
    public CharacterDataPack middle;
    public CharacterDataPack back;

}

public class CharacterDataPack
{
    public CharacterDataPack(string name, int cd1, int cd2,int hp)
    {
        heroName = name;
        skillCD1 = cd1;
        skillCD2 = cd2;
        currentHealth = hp;
    }
    public string heroName;
    public int skillCD1;
    public int skillCD2;
    public int currentHealth;
}
