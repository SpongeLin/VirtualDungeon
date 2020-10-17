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
        deck.Add(1);
        deck.Add(1);
        deck.Add(2);
        deck.Add(5);
        deck.Add(5);
        deck.Add(2);
        deck.Add(3);
        deck.Add(4);

        handCardNum = 4;
        //===



    }

    public List<int> deck;
    public int handCardNum;


}
