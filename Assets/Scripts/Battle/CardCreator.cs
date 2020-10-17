using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class CardCreator
{

    public static CardData CreateCard(int cardNo)
    {
        CardData card = new CardData();

        Create(card, cardNo);
        foreach (CardEffect effect in card.cardEffects)
            effect.card = card;
        //cardImage...

        return card;
    }

    static void Create(CardData card,int cardNo)
    {
        switch (cardNo)
        {
            case 1:
                card.cardShowName = "TEST1";
                card.cardDescription = "無目標";
                card.cardCost = 0;
                card.cardTarget = CardTarget.CardSelf;
                card.cardEffects.Add(new nCardEffect.GainEnergy(1));
                break;
            case 2:
                card.cardShowName = "TEST2";
                card.cardDescription = "敵方";
                card.cardCost = 2;
                card.cardTarget = CardTarget.Enemies;

                card.cardEffects.Add(new nCardEffect.DamageEffect(20));
                break;
            case 3:
                card.cardShowName = "TEST3";
                card.cardDescription = "友方";
                card.cardCost = 3;
                card.cardTarget = CardTarget.Allies;

                card.cardEffects.Add(new nCardEffect.DamageEffect(30));
                break;
            case 4:
                card.cardShowName = "TEST4";
                card.cardDescription = "全體角色";
                card.cardCost = 3;
                card.cardTarget = CardTarget.All;

                card.cardEffects.Add(new nCardEffect.DamageEffect(40));
                break;
            case 5:
                card.cardShowName = "TEST4";
                card.cardDescription = "狀態賦予";
                card.cardCost = 1;
                card.cardTarget = CardTarget.All;

                card.cardEffects.Add(new nCardEffect.GiveStatus("DamageToZero", 3));
                break;



        }


    }


}