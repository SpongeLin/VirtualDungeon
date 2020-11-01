using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardData 
{
    public int cardNo;
    public string cardName;
    public string cardShowName;

    public Sprite cardImage;

    public string cardDescription;
    public int cardCost
    {
        get
        {
            if (setCost == -1)
                return (oriCost + extraCost)<0?0:(oriCost + extraCost);
            else return (setCost + extraCost)<0?0:(setCost + extraCost);
        }
    }
    public int extraCost;
    public int setCost = -1;
    public int oriCost;

    public bool canUse;
    public int banCount;
    public int exhasutCount;

    public int magicConsume;
    public bool magicCheck;
    public bool canBurst;
    public int burst;

    public CharData linkChar;
    public bool linkCheck;

    public bool isSelectTarget;
    public CharData targetChar;

    public List<CardEffect> cardEffects;
    public List<CharFilter> charFilters;

    public CardStatusControl cardStatusControl;

    public CardData()
    {
        cardEffects = new List<CardEffect>();
        cardStatusControl = new CardStatusControl(this);

        charFilters = new List<CharFilter>();
    }

    public void Clear()
    {
        targetChar = null;
        cardStatusControl.ExitAll();
        magicCheck = false;
        burst = 0;
        linkChar = null;
        linkCheck = false;
        //cardStatus clear!!
    }

    public void Burst()
    {
        burst++;
    }
    public void CostAdjust(int costNum)
    {
        extraCost += costNum;
    }
    public void CostSet(int costNum)
    {
        setCost = costNum;
    }

}
