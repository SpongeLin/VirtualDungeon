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
    public int cardCost;

    public bool canUse;
    public int banCount;

    public CardTarget cardTarget;
    public CharData targetChar;

    public List<CardEffect> cardEffects;

    public CardStatusControl cardStatusControl;

    public CardData()
    {
        cardEffects = new List<CardEffect>();
        cardStatusControl = new CardStatusControl(this);
    }

}
