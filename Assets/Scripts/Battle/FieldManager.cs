using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : MonoBehaviour
{
    public static FieldManager instance { get; private set; }

    public bool playerTurn;
    public bool isWorking = false;

    public List<CharData> heros;
    public List<CharData> enemies;


    private void Awake()
    {
        instance = this;
        heros = new List<CharData>();
        enemies = new List<CharData>();

    }
    private void Start()
    {
        isWorking = true;


        GameStart();
    }



    public void GameStart()
    {
        isWorking = true;
    }


    public void DamageChar(CharData character, int damageValue,DamageType type)
    {

    }

}
