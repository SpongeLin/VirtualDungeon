using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class CardCreator
{

    public static CardData CreateCard(int cardNo)
    {
        CardData card = new CardData();
        card.cardNo = cardNo;

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
                card.oriCost = 0;
                card.cardEffects.Add(new nCardEffect.GainEnergy(1));
                break;
            case 2:
                card.cardShowName = "TEST2";
                card.cardDescription = "敵方";
                card.oriCost = 2;
                card.charFilters.Add(new CharFilter("Camps", 2));
                card.isSelectTarget = true;

                card.cardEffects.Add(new nCardEffect.DamageEffectSelect(20));
                card.cardStatusControl.EnterStatus(new nCardStatus.Overload(2));
                card.cardStatusControl.EnterStatus(new nCardStatus.OriExhasut());
                break;
            case 3:
                card.cardShowName = "TEST3";
                card.cardDescription = "友方";
                card.oriCost = 3;
                card.charFilters.Add(new CharFilter("Camps", 1));
                card.isSelectTarget = true;

                card.cardEffects.Add(new nCardEffect.DamageEffectSelect(30));
                break;
            case 4:
                card.cardShowName = "TEST4";
                card.cardDescription = "全體角色";
                card.oriCost = 3;
                card.isSelectTarget = true;

                card.cardEffects.Add(new nCardEffect.DamageEffectSelect(40));
                break;
            case 5:
                card.cardShowName = "TEST4";
                card.cardDescription = "狀態賦予";
                card.oriCost = 1;
                card.isSelectTarget = true;

                card.cardEffects.Add(new nCardEffect.GiveCharStatusSelect("DamageToZero", 3));
                break;

            case 10:
                card.cardShowName = "打擊";
                card.cardDescription = "對一個敵方角色造成17點傷害";
                card.oriCost = 1;
                card.isSelectTarget = true;
                card.charFilters.Add(new CharFilter("Camps", 2));
                card.cardEffects.Add(new nCardEffect.DamageEffectSelect(17));
                break;
            case 11:
                card.cardShowName = "重擊";
                card.cardDescription = "對最前方角色造成45點傷害";
                card.oriCost = 2;
                card.isSelectTarget = true;
                card.charFilters.Add(new CharFilter("Front"));
                card.cardEffects.Add(new nCardEffect.DamageEffectSelect(17));
                break;
            case 12:
                card.cardShowName = "防禦";
                card.cardDescription = "自身獲得15盾";
                card.oriCost = 1;
                card.cardEffects.Add(new nCardEffect.ArmorSelf(15));
                break;
            case 13:
                card.cardShowName = "X";
                card.cardDescription = "對一個敵方角色造成25點傷害，脆弱一回合";
                card.oriCost = 2;
                card.isSelectTarget = true;
                card.charFilters.Add(new CharFilter("Camps",2));
                card.cardEffects.Add(new nCardEffect.DamageEffectSelect(25));
                card.cardEffects.Add(new nCardEffect.GiveCharStatusSelect("Fragile", 1));
                break;
            case 14:
                card.cardShowName = "X";
                card.cardDescription = "對隨機一個敵人造成20傷害，自身獲得一點魔力";
                card.oriCost = 2;
                card.cardEffects.Add(new nCardEffect.DamageRandomEnemy(20));
                card.cardEffects.Add(new nCardEffect.MagicSelf(1));
                break;
            case 15:
                card.cardShowName = "X";
                card.cardDescription = "對一個敵方角色造成25點傷害。若擊殺目標，自身獲得一點魔力";
                card.oriCost = 1;
                card.isSelectTarget = true;
                card.charFilters.Add(new CharFilter("Camps", 2));
                card.cardEffects.Add(new nCardEffect.DamageEffectSelect(25));
                card.cardEffects.Add(new nCardEffect.GainMagicIfTargetDie(1));
                break;
            case 16:
                card.cardShowName = "X";
                card.cardDescription = "對自身造成10點傷害，自身獲得35點護盾";
                card.oriCost = 1;
                card.cardEffects.Add(new nCardEffect.DamageSelf(10));
                card.cardEffects.Add(new nCardEffect.ArmorSelf(35));
                card.oriCost = 1;
                break;
            case 17:
                card.cardShowName = "X";
                card.cardDescription = "對一個敵方角色造成16點傷害。強化：+4點傷害";
                card.oriCost = 1;
                card.canBurst = true;
                card.isSelectTarget = true;
                card.charFilters.Add(new CharFilter("Camps", 2));
                card.cardEffects.Add(new nCardEffect.DamageEffectSelectBurst(16, 4));
                break;
            case 18:
                card.cardShowName = "防禦";
                card.cardDescription = "自身獲得30點護盾。超載(1)";
                card.oriCost = 1;
                card.cardEffects.Add(new nCardEffect.ArmorSelf(30));
                card.cardStatusControl.EnterStatus(new nCardStatus.Overload(1));
                break;
            case 19:
                card.cardShowName = "防禦";
                card.cardDescription = "對自身造成10點傷害，獲得2點AP。消耗";
                card.oriCost = 0;
                card.cardStatusControl.EnterStatus(new nCardStatus.OriExhasut());
                card.cardEffects.Add(new nCardEffect.DamageSelf(10));
                card.cardEffects.Add(new nCardEffect.GainEnergy(2));
                break;
            case 20:
                card.cardShowName = "防禦";
                card.cardDescription = "對一個敵方角色造成15點傷害。魔力(1):再造成一次傷害";
                card.oriCost = 1;
                card.magicConsume = 1;
                card.isSelectTarget = true;
                card.charFilters.Add(new CharFilter("Camps", 2));
                card.cardEffects.Add(new nCardEffect.DamageEffectSelect(15));
                card.cardEffects.Add(new nCardEffect.DamageEffectSelectMagic(15));
                break;
            case 21:
                card.cardShowName = "防禦";
                card.cardDescription = "對所有敵方角色造成15點傷害。魔力(2):歸還AP";
                card.oriCost = 2;
                card.magicConsume = 2;
                card.cardEffects.Add(new nCardEffect.DamageAllEnemy(15));
                card.cardEffects.Add(new nCardEffect.BackEnergyIfMagic());
                break;
            case 22:
                card.cardShowName = "符文射擊";
                card.cardDescription = "對一個敵方角色造成40點傷害。連結：歸還能量";
                card.oriCost = 3;
                card.isSelectTarget = true;
                card.charFilters.Add(new CharFilter("Camps", 2));
                card.cardEffects.Add(new nCardEffect.DamageEffectSelect(40));
                card.cardEffects.Add(new nCardEffect.BackEnergyIfLink());
                card.cardStatusControl.EnterStatus(new nCardStatus.SoulLink());
                break;
            case 23:
                card.cardShowName = "重新整理";
                card.cardDescription = "丟棄所有手牌";
                card.oriCost = 1;
                card.cardEffects.Add(new nCardEffect.DiscardAllHand());
                break;
            case 24:
                card.cardShowName = "電池";
                card.cardDescription = "自身獲得1點能量。強化:再獲得1點能量";
                card.oriCost = 0;
                card.canBurst = true;
                card.cardEffects.Add(new nCardEffect.GainEnergy(1));
                card.cardEffects.Add(new nCardEffect.GainEnergyBurst(1));
                break;
            case 25:
                card.cardShowName = "能量脈衝";
                card.cardDescription = "對所有敵方角色造成10點傷害，脆弱一回合。強化：此卡費用減少(1)";
                card.oriCost = 3;
                card.canBurst = true;
                card.cardEffects.Add(new nCardEffect.DamageAllEnemy(10));
                card.cardEffects.Add(new nCardEffect.GiveCharStatusAllEnemy("Fragile", 1));
                card.cardStatusControl.EnterStatus(new nCardStatus.CostDownAtBurst(1));
                break;
            case 26:
                card.cardShowName = "魔力閃燃";
                card.cardDescription = "對一個隨機敵方角色造成8點傷害。魔力(1):複製一張此卡放到你牌庫頂";
                card.oriCost = 1;
                card.magicConsume = 1;
                card.cardEffects.Add(new nCardEffect.DamageRandomEnemy(8));
                card.cardEffects.Add(new nCardEffect.CopySelfToDeckMagic());
                break;

        }


    }


}