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

        card.cardImage = Resources.Load<Sprite>("Card/" + card.cardName);

        if (FieldManager.instance == null)
        {
            Debug.Log("OnlySHow");
            return card;
        }

        if(card.oriOverLoad>0)
            card.cardStatusControl.EnterStatus(new nCardStatus.Overload(card.oriOverLoad));
        if(card.oriExhasut)
            card.cardStatusControl.EnterStatus(new nCardStatus.OriExhasut());
        if(card.oriSoulLink)
            card.cardStatusControl.EnterStatus(new nCardStatus.SoulLink());


        foreach (CardEffect effect in card.cardEffects)
            effect.card = card;
        //cardImage...

        return card;
    }

    public static int GetRandomNormalCard()
    {
        int minCard = 10;
        int maxCard = 33;
        return Random.Range(minCard, maxCard + 1);
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
                card.oriExhasut = true;
                card.oriOverLoad = 2;
                card.charFilters.Add(new CharFilter("Camps", 2));
                card.isSelectTarget = true;

                card.cardEffects.Add(new nCardEffect.DamageEffectSelect(20));
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
                card.cardDescription = "賦予絕對和平狀態3回合";
                card.oriCost = 1;
                card.isSelectTarget = true;

                card.cardEffects.Add(new nCardEffect.GiveCharStatusSelect("DamageToZero", 3));
                break;

            case 10:
                card.cardName = "Attack";
                card.cardShowName = "打擊";
                card.cardDescription = "對一個敵方角色造成17點傷害";
                card.oriCost = 1;
                card.isSelectTarget = true;
                card.charFilters.Add(new CharFilter("Camps", 2));
                card.cardEffects.Add(new nCardEffect.DamageEffectSelect(17));
                break;
            case 11:
                card.cardName = "HeroicAttack";
                card.cardShowName = "英勇突擊";
                card.cardDescription = "對最前方角色造成23點傷害";
                card.oriCost = 1;
                card.isSelectTarget = true;
                card.charFilters.Add(new CharFilter("Front"));
                card.cardEffects.Add(new nCardEffect.DamageEffectSelect(23));
                break;
            case 12:
                card.cardName = "Armor";
                card.cardShowName = "防禦";
                card.cardDescription = "自身獲得15護盾";
                card.oriCost = 1;
                card.cardEffects.Add(new nCardEffect.ArmorSelf(15));
                break;
            case 13:
                card.cardName = "Bash";
                card.cardShowName = "重擊";
                card.cardDescription = "對一個敵方角色造成25點傷害，脆弱一回合";
                card.oriCost = 2;
                card.isSelectTarget = true;
                card.charFilters.Add(new CharFilter("Camps",2));
                card.cardEffects.Add(new nCardEffect.DamageEffectSelect(25));
                card.cardEffects.Add(new nCardEffect.GiveCharStatusSelect("Fragile", 1));
                break;
            case 14:
                card.cardName = "MagicScurrying";
                card.cardShowName = "法力亂竄";
                card.cardDescription = "對隨機一個敵人造成17傷害，自身獲得一點魔力";
                card.oriCost = 1;
                card.cardEffects.Add(new nCardEffect.DamageRandomEnemy(17));
                card.cardEffects.Add(new nCardEffect.MagicSelf(1));
                break;
            case 15:
                card.cardName = "MagicSiphon";
                card.cardShowName = "法力虹吸";
                card.cardDescription = "對一個敵方角色造成25點傷害。若擊殺目標，自身獲得一點魔力";
                card.oriCost = 2;
                card.isSelectTarget = true;
                card.charFilters.Add(new CharFilter("Camps", 2));
                card.cardEffects.Add(new nCardEffect.DamageEffectSelect(25));
                card.cardEffects.Add(new nCardEffect.GainMagicIfTargetDie(1));
                break;
            case 16:
                card.cardName = "CurseArmor";
                card.cardShowName = "詛咒之盾";
                card.cardDescription = "對自身造成10點傷害，自身獲得30點護盾";
                card.oriCost = 1;
                card.cardEffects.Add(new nCardEffect.DamageSelf(10));
                card.cardEffects.Add(new nCardEffect.ArmorSelf(30));
                break;
            case 17:
                card.cardName = "WaveFist";
                card.cardShowName = "波動拳";
                card.cardDescription = "對一個敵方角色造成16點傷害。強化:+4點傷害";
                card.oriCost = 1;
                card.canBurst = true;
                card.isSelectTarget = true;
                card.charFilters.Add(new CharFilter("Camps", 2));
                card.cardEffects.Add(new nCardEffect.DamageEffectSelectBurst(16, 4));
                break;
            case 18:
                card.cardName = "DeterminationArmor";
                card.cardShowName = "決心之盾";
                card.cardDescription = "自身獲得30點護盾。超載(1)";
                card.oriCost = 1;
                card.oriOverLoad = 1;
                card.cardEffects.Add(new nCardEffect.ArmorSelf(30));
                break;
            case 19:
                card.cardName = "Gluttony";
                card.cardShowName = "暴食";
                card.cardDescription = "獲得2點能量。消耗";
                card.oriCost = 0;
                card.oriExhasut = true;
                card.cardEffects.Add(new nCardEffect.GainEnergy(2));
                break;
            case 20:
                card.cardName = "MagicArrow";
                card.cardShowName = "魔導箭";
                card.cardDescription = "對一個敵方角色造成15點傷害。魔導(1):再造成一次傷害";
                card.oriCost = 1;
                card.oriMagicConsume = 1;
                card.isSelectTarget = true;
                card.charFilters.Add(new CharFilter("Camps", 2));
                card.cardEffects.Add(new nCardEffect.DamageEffectSelect(15));
                card.cardEffects.Add(new nCardEffect.DamageEffectSelectMagic(15));
                break;
            case 21:
                card.cardName = "Wish";
                card.cardShowName = "心願";
                card.cardDescription = "對所有敵方角色造成10點傷害。魔導(2):歸還能量";
                card.oriCost = 2;
                card.oriMagicConsume = 2;
                card.cardEffects.Add(new nCardEffect.DamageAllEnemy(10));
                card.cardEffects.Add(new nCardEffect.BackEnergyIfMagic());
                break;
            case 22:
                card.cardName = "ConcentratedBreathing";
                card.cardShowName = "集中呼吸";
                card.cardDescription = "自身獲得10點護盾。連結:歸還能量";
                card.oriCost = 1;
                card.oriSoulLink = true;
                card.cardEffects.Add(new nCardEffect.ArmorSelf(10));
                card.cardEffects.Add(new nCardEffect.BackEnergyIfLink());
                break;
            case 23:
                card.cardName = "Reorganize";
                card.cardShowName = "重新整理";
                card.cardDescription = "丟棄所有手牌";
                card.oriCost = 1;
                card.cardEffects.Add(new nCardEffect.DiscardAllHand());
                break;
            case 24:
                card.cardName = "Battery";
                card.cardShowName = "電池";
                card.cardDescription = "自身獲得1點能量。強化:再獲得1點能量";
                card.oriCost = 0;
                card.canBurst = true;
                card.cardEffects.Add(new nCardEffect.GainEnergy(1));
                card.cardEffects.Add(new nCardEffect.GainEnergyBurst(1));
                break;
            case 25:
                card.cardName = "EnergyPulse";
                card.cardShowName = "能量脈衝";
                card.cardDescription = "對所有敵方角色造成10點傷害，脆弱一回合。強化:此卡花費減少(1)";
                card.oriCost = 3;
                card.canBurst = true;
                card.cardEffects.Add(new nCardEffect.DamageAllEnemy(10));
                card.cardEffects.Add(new nCardEffect.GiveCharStatusAllEnemy("Fragile", 1));
                card.cardStatusControl.EnterStatus(new nCardStatus.CostDownAtBurst(1));
                break;
            case 26:
                card.cardName = "MagicFlash";
                card.cardShowName = "法力閃燃";
                card.cardDescription = "對一個隨機敵方角色造成8點傷害。魔導(1):複製一張此卡放到你牌庫頂";
                card.oriCost = 0;
                card.oriMagicConsume = 1;
                card.cardEffects.Add(new nCardEffect.DamageRandomEnemy(8));
                card.cardEffects.Add(new nCardEffect.CopySelfToDeckMagic());
                break;
            case 27:
                card.cardName = "GreedyArmor";
                card.cardShowName = "貪婪之盾";
                card.cardDescription = "對一個其他友方角色造成15點傷害，自身獲得35點護盾";
                card.oriCost = 1;
                card.isSelectTarget = true;
                card.charFilters.Add(new CharFilter("Camps", 1));
                card.charFilters.Add(new CharFilter("NotCurrent"));
                card.cardEffects.Add(new nCardEffect.DamageEffectSelect(15));
                card.cardEffects.Add(new nCardEffect.ArmorSelf(35));
                break;
            case 28:
                card.cardName = "LimitedWill";
                card.cardShowName = "極限意志";
                card.cardDescription = "所有友方角色獲得20點護盾。超載(2)";
                card.oriCost = 1;
                card.oriOverLoad = 2;
                card.cardEffects.Add(new nCardEffect.ArmorAllAlly(20));
                break;
            case 29:
                card.cardName = "RotationDistance";
                card.cardShowName = "迴轉距離";
                card.cardDescription = "對所有敵人造成17點傷害，放一張「記憶斷片」到你手中";
                card.oriCost = 1;
                card.cardEffects.Add(new nCardEffect.DamageAllEnemy(17));
                card.cardEffects.Add(new nCardEffect.NewCardToHand(700));
                break;
            case 30:
                card.cardName = "BeastDance";
                card.cardShowName = "野獸之舞";
                card.cardDescription = "此卡在你手上時，自身力量+5";
                card.oriCost = 0;
                card.cardStatusControl.EnterStatus(new nCardStatus.BeastDance(5));
                break;
            case 31:
                card.cardName = "GhostRule";
                card.cardShowName = "幽靈法則";
                card.cardDescription = "你的手牌消耗降低(1)，放2張記憶斷片到你的牌庫中";
                card.oriCost = 0;
                card.cardEffects.Add(new nCardEffect.HandCardCostAdjust(-1));
                card.cardEffects.Add(new nCardEffect.NewCardToHand(700));
                card.cardEffects.Add(new nCardEffect.NewCardToHand(700));
                break;
            case 32:
                card.cardName = "ThunderTouch";
                card.cardShowName = "雷神";
                card.cardDescription = "對最前方角色造成15點傷害，自身移動到最前方";
                card.oriCost = 1;
                card.cardEffects.Add(new nCardEffect.DamageFront(15));
                card.cardEffects.Add(new nCardEffect.ChangeToFront());
                break;
            case 33:
                card.cardName = "JuniorMagicStone";
                card.cardShowName = "初級魔法石";
                card.cardDescription = "獲得1點魔力";
                card.oriCost = 0;
                card.cardEffects.Add(new nCardEffect.MagicSelf(1));
                break;



            case 700:
                card.cardName = "MemoryFragment";
                card.cardShowName = "記憶斷片";
                card.cardName = "Memory Fragment";
                card.cardDescription = "無效果。消耗";
                card.oriCost = 1;
                card.oriExhasut = true;
                card.negativeCard = true;
                break;

        }


    }


}