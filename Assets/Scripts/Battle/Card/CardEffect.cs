using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardEffect
{
    public CardData card;
    public bool charTarget;

    public abstract void UseCardEffect();
    public bool CanUseCardEffect()
    {
        if (charTarget == true && card.targetChar == null)
            return false;
        return true;
    }
}

namespace nCardEffect
{
    public class DamageEffectSelect : CardEffect
    {
        int damageValue;
        public DamageEffectSelect(int _damageValue)
        {
            damageValue = _damageValue;

            charTarget = true;
        }
        public override void UseCardEffect()
        {
            //Animation???
            //FieldManager.instance.DamageChar(card.targetChar, damageValue, DamageType.Normal, FieldManager.instance.currentActionCharacter);
            OrderManager.instance.AddOrder(new sysOrder.DamagePrepare(card.targetChar, damageValue, DamageType.Normal, FieldManager.instance.currentActionCharacter));
        }
    }
    public class DamageRandomEnemy : CardEffect
    {
        int damageValue;
        public DamageRandomEnemy(int _damageValue)
        {
            damageValue = _damageValue;
        }
        public override void UseCardEffect()
        {
            //Animation???
            //FieldManager.instance.DamageChar(card.targetChar, damageValue, DamageType.Normal, FieldManager.instance.currentActionCharacter);
            OrderManager.instance.AddOrder(new sysOrder.DamagePapareRandom(new CharFilter[] {new CharFilter("Camps",2) },damageValue,DamageType.Normal,FieldManager.instance.currentActionCharacter));
        }
    }
    public class DamageEffectSelectBurst : CardEffect
    {
        int damageValue;
        int burstDamage;
        public DamageEffectSelectBurst(int _damageValue,int _burstDamage)
        {
            damageValue = _damageValue;
            burstDamage = _burstDamage;

            charTarget = true;
        }
        public override void UseCardEffect()
        {
            //Animation???
            //FieldManager.instance.DamageChar(card.targetChar, damageValue, DamageType.Normal, FieldManager.instance.currentActionCharacter);
            int extraBurstDamage = burstDamage * card.burst;
            OrderManager.instance.AddOrder(new sysOrder.DamagePrepare(card.targetChar, damageValue+ extraBurstDamage, DamageType.Normal, FieldManager.instance.currentActionCharacter));
        }
    }
    public class DamageEffectSelectMagic : CardEffect
    {
        int damageValue;
        public DamageEffectSelectMagic(int _damageValue)
        {
            damageValue = _damageValue;

            charTarget = true;
        }
        public override void UseCardEffect()
        {
            //Animation???
            //FieldManager.instance.DamageChar(card.targetChar, damageValue, DamageType.Normal, FieldManager.instance.currentActionCharacter);
            if (!card.magicCheck) return;
            OrderManager.instance.AddOrder(new sysOrder.DamagePrepare(card.targetChar, damageValue, DamageType.Normal, FieldManager.instance.currentActionCharacter));
        }
    }
    public class DamageAllEnemy : CardEffect
    {
        int damageValue;
        public DamageAllEnemy(int _damageValue)
        {
            damageValue = _damageValue;
        }
        public override void UseCardEffect()
        {
            //Animation???
            OrderManager.instance.AddOrder(new sysOrder.DamagePapareMulti(new CharFilter[] {new CharFilter("Camps",2) }, damageValue, DamageType.Normal, FieldManager.instance.currentActionCharacter));
        }
    }
    public class DamageFront : CardEffect
    {
        int damageValue;
        public DamageFront(int _damageValue)
        {
            damageValue = _damageValue;
        }
        public override void UseCardEffect()
        {
            //Animation???
            OrderManager.instance.AddOrder(new sysOrder.DamagePapareFront( damageValue, DamageType.Normal, FieldManager.instance.currentActionCharacter));
        }
    }
    public class ArmorSelf : CardEffect
    {
        public int armorNum;
        public ArmorSelf(int armor)
        {
            armorNum = armor;
        }

        public override void UseCardEffect()
        {
            OrderManager.instance.AddOrder(new sysOrder.ArmorChar(FieldManager.instance.currentActionCharacter, armorNum, FieldManager.instance.currentActionCharacter));
        }
    }
    public class ArmorSelect : CardEffect
    {
        public int armorNum;
        public ArmorSelect(int armor)
        {
            armorNum = armor;
            charTarget = true;
        }

        public override void UseCardEffect()
        {
            OrderManager.instance.AddOrder(new sysOrder.ArmorChar(card.targetChar, armorNum, FieldManager.instance.currentActionCharacter));
        }
    }
    public class ArmorAllAlly : CardEffect
    {
        public int armorNum;
        public ArmorAllAlly(int armor)
        {
            armorNum = armor;
        }

        public override void UseCardEffect()
        {
            OrderManager.instance.AddOrder(new sysOrder.ArmorCharMulti(new CharFilter[] { new CharFilter("Camps", 1) }, armorNum, FieldManager.instance.currentActionCharacter));
        }
    }
    public class GainEnergy : CardEffect
    {
        int energy;
        public GainEnergy(int _energy)
        {
            energy = _energy;
        }
        public override void UseCardEffect()
        {
            OrderManager.instance.AddOrder(new sysOrder.GainEnergy(FieldManager.instance.currentActionCharacter, energy));
        }
    }
    public class MagicSelf : CardEffect
    {
        int magic;
        public MagicSelf(int magicNum)
        {
            magic = magicNum;
        }

        public override void UseCardEffect()
        {
            OrderManager.instance.AddOrder(new sysOrder.GainMagic(FieldManager.instance.currentActionCharacter, magic));
        }
    }
    public class DamageSelf : CardEffect
    {
        int damageValue;
        public DamageSelf(int _damageValue)
        {
            damageValue = _damageValue;
        }

        public override void UseCardEffect()
        {
            OrderManager.instance.AddOrder(new sysOrder.DamagePrepare(FieldManager.instance.currentActionCharacter, damageValue, DamageType.Normal, FieldManager.instance.currentActionCharacter));
        }
    }
    public class GiveCharStatusSelect : CardEffect
    {
        string statusName;
        int statusNum;
        public GiveCharStatusSelect(string _statusName,int _statusNum)
        {
            statusName = _statusName;
            statusNum = _statusNum;

            charTarget = true;
        }
        public override void UseCardEffect()
        {
            //Animation???
            OrderManager.instance.AddOrder(new sysOrder.GiveCharStatus(card.targetChar, statusName, statusNum));
        }
    }
    public class GiveCharStatusAllEnemy : CardEffect
    {
        string statusName;
        int statusNum;
        public GiveCharStatusAllEnemy(string _statusName, int _statusNum)
        {
            statusName = _statusName;
            statusNum = _statusNum;
        }
        public override void UseCardEffect()
        {
            //Animation???
            OrderManager.instance.AddOrder(new sysOrder.GiveCharStatusMulti(new CharFilter[] {new CharFilter("Camps",2) }, statusName, statusNum));
        }
    }
    public class GainMagicIfTargetDie : CardEffect
    {
        int magicNum;
        public GainMagicIfTargetDie(int magic)
        {
            magicNum = magic;

            charTarget = true;
        }
        public override void UseCardEffect()
        {
            OrderManager.instance.AddOrder(new nOrder.GainMagicIfTargetDie(magicNum,card.targetChar,FieldManager.instance.currentActionCharacter));
        }
    }
    public class BackEnergyIfMagic : CardEffect
    {
        public BackEnergyIfMagic()
        {

        }
        public override void UseCardEffect()
        {
            if (!card.magicCheck) return;
            OrderManager.instance.AddOrder(new sysOrder.GainEnergy(FieldManager.instance.currentActionCharacter, card.cardCost));
        }
    }
    public class BackEnergyIfLink : CardEffect
    {
        public BackEnergyIfLink()
        {

        }
        public override void UseCardEffect()
        {
            if (!card.linkCheck) return;
            OrderManager.instance.AddOrder(new sysOrder.GainEnergy(FieldManager.instance.currentActionCharacter, card.cardCost));
        }
    }
    public class DiscardAllHand : CardEffect
    {
        public DiscardAllHand()
        {

        }
        public override void UseCardEffect()
        {
            CardData[] cards = CardManager.instance.handCards.ToArray();
            foreach(CardData card in cards)
            {
                OrderManager.instance.AddOrder(new sysOrder.DiscardOrder(card));
            }
        }
    }
    public class GainEnergyBurst : CardEffect
    {
        public int burstEnergy;
        public GainEnergyBurst(int _burstEnergy)
        {
            burstEnergy = _burstEnergy;
        }
        public override void UseCardEffect()
        {
            if (card.burst > 0)
            {
                for(int i=0;i<card.burst;i++)
                    OrderManager.instance.AddOrder(new sysOrder.GainEnergy(FieldManager.instance.currentActionCharacter, burstEnergy));
            }
        }
    }
    public class CopySelfToDeckMagic : CardEffect
    {
        public CopySelfToDeckMagic()
        {
        }
        public override void UseCardEffect()
        {
            if (card.magicCheck)
            {
                OrderManager.instance.AddOrder(new sysOrder.NewCardToDeck(card.cardNo,true));
            }
        }
    }
    public class NewCardToHand : CardEffect
    {
        public int cardNo;
        public NewCardToHand(int _cardNo)
        {
            cardNo = _cardNo;
        }
        public override void UseCardEffect()
        {
            OrderManager.instance.AddOrder(new sysOrder.NewCardToHand(cardNo));
        }
    }
    public class ChangeToFront : CardEffect
    {
        public ChangeToFront()
        {
        }
        public override void UseCardEffect()
        {
            OrderManager.instance.AddOrder(new sysOrder.ChangeToFront(FieldManager.instance.currentActionCharacter));
        }
    }
    public class HandCardCostAdjust : CardEffect
    {
        int adjustNum;
        public HandCardCostAdjust(int _adjustNum)
        {
            adjustNum = _adjustNum;
        }
        public override void UseCardEffect()
        {
            OrderManager.instance.AddOrder(new nOrder.HandCardCostAdjust(adjustNum));
        }
    }
}