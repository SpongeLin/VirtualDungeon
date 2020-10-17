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
    public class DamageEffect : CardEffect
    {
        int damageValue;
        public DamageEffect(int _damageValue)
        {
            damageValue = _damageValue;

            charTarget = true;
        }
        public override void UseCardEffect()
        {
            //Animation???
            FieldManager.instance.DamageChar(card.targetChar, damageValue, DamageType.Normal, FieldManager.instance.currentActionCharacter);
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
            FieldManager.instance.GainEnergy(energy);
        }
    }
    public class GiveStatus : CardEffect
    {
        string statusName;
        int statusNum;
        public GiveStatus(string _statusName,int _statusNum)
        {
            statusName = _statusName;
            statusNum = _statusNum;

            charTarget = true;
        }
        public override void UseCardEffect()
        {
            //Animation???
            FieldManager.instance.GiveCharStatus(card.targetChar, statusName, statusNum);
        }
    }
}