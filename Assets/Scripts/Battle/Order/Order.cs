using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Order
{
    public abstract void Execution();
}

namespace sysOrder
{
    public class GameStart : Order
    {
        public override void Execution()
        {
            FieldManager.instance.GameStart();
        }
    }
    public class StartPrepare : Order
    {
        public override void Execution()
        {
            FieldManager.instance.StartPrepare();
        }
    }
    public class TurnEndOrder : Order
    {

        public override void Execution()
        {
            FieldManager.instance.RealTurnEnd();
        }
    }


    public class WaitOrder : Order
    {
        float time;
        public WaitOrder(float _time)
        {
            time = _time;
        }
        public override void Execution()
        {
            OrderManager.instance.SetWait(time);
        }
    }
    public class LogOrder : Order
    {
        string text;
        public LogOrder(string _text)
        {
            text = _text;
        }
        public override void Execution()
        {
            Debug.Log(text);
        }
    }
    public class CharMoveOrder : Order
    {

        public CharMoveOrder()
        {
        }
        public override void Execution()
        {
            FieldManager.instance.CurrentCharMove();
        }
    }



    //=======================================================
    public class DamagePrepare : Order
    {
        CharData character;
        CharData damagerChar;
        int damageValue;
        DamageType damageType;
        public DamagePrepare(CharData _character, int _damageValue, DamageType _type, CharData _damager = null)
        {
            character = _character;
            damageValue = _damageValue;
            damageType = _type;
            damagerChar = _damager;
        }
        public override void Execution()
        {
            if (character == null) return;
            if (damagerChar != null)
                damageValue += damagerChar.power;

            DamageInfo dif = TriggerManager.instance.GetTriggerInfo<DamageInfo>();
            dif.SetInfo(character, damageValue, damageType, damagerChar);

            dif.GoTrigger(TriggerType.DamageCheck);
            dif.GoTrigger(TriggerType.DamageTotalCheck);
            dif.GoTrigger(TriggerType.DamageFinalCheck);
            dif.Computer();

            dif.GoTrigger(TriggerType.DamageAfter);
            OrderManager.instance.AddOrder(new sysOrder.DamageOrder(dif.damagedChar, dif.damageValue, dif.damageType, dif.damagerChar));
            dif.GoTrigger(TriggerType.DamageBefore);
        }
    }
    public class DamagePapareRandom : Order
    {
        CharFilter[] filters;
        CharData damagerChar;
        int damageValue;
        DamageType damageType;

        int damageTimes;
        public DamagePapareRandom(CharFilter[] _filters, int _damageValue, DamageType _type, CharData _damager = null, int times = 1)
        {
            filters = _filters;
            damageValue = _damageValue;
            damageType = _type;
            damagerChar = _damager;

            damageTimes = times;
        }

        public override void Execution()
        {
            CharData character = FieldManager.instance.GetRandomChar(filters);

            if (character == null) return;
            if (damagerChar != null)
                damageValue += damagerChar.power;

            DamageInfo dif = TriggerManager.instance.GetTriggerInfo<DamageInfo>();
            for(int i = 0; i < damageTimes; i++)
            {
                dif.SetInfo(character, damageValue, damageType, damagerChar);

                dif.GoTrigger(TriggerType.DamageCheck);
                dif.GoTrigger(TriggerType.DamageTotalCheck);
                dif.GoTrigger(TriggerType.DamageFinalCheck);
                dif.Computer();

                dif.GoTrigger(TriggerType.DamageAfter);
                OrderManager.instance.AddOrder(new sysOrder.DamageOrder(dif.damagedChar, dif.damageValue, dif.damageType, dif.damagerChar));
                dif.GoTrigger(TriggerType.DamageBefore);
            }
        }
    }
    public class DamagePapareMulti : Order // can't use this now
    {
        CharFilter[] filters;
        CharData damagerChar;
        int damageValue;
        DamageType damageType;
        public DamagePapareMulti(CharFilter[] _filters, int _damageValue, DamageType _type, CharData _damager = null)
        {
            filters = _filters;
            damageValue = _damageValue;
            damageType = _type;
            damagerChar = _damager;
        }
        public override void Execution()
        {
            CharData[] chars = FieldManager.instance.GetConditionChar(filters);

            if (damagerChar != null)
                damageValue += damagerChar.power;

            DamageInfo dif = TriggerManager.instance.GetTriggerInfo<DamageInfo>();
            foreach(CharData character in chars)
            {
                dif.SetInfo(character, damageValue, damageType, damagerChar);

                dif.GoTrigger(TriggerType.DamageCheck);
                dif.GoTrigger(TriggerType.DamageTotalCheck);
                dif.GoTrigger(TriggerType.DamageFinalCheck);
                dif.Computer();

                dif.GoTrigger(TriggerType.DamageAfter);
                OrderManager.instance.AddOrder(new sysOrder.DamageOrder(dif.damagedChar, dif.damageValue, dif.damageType, dif.damagerChar));
                dif.GoTrigger(TriggerType.DamageBefore);
            }
        }
    }
    public class DamagePapareFront : Order
    {
        CharData damagerChar;
        int damageValue;
        DamageType damageType;
        public DamagePapareFront( int _damageValue, DamageType _type, CharData _damager = null)
        {

            damageValue = _damageValue;
            damageType = _type;
            damagerChar = _damager;
        }

        public override void Execution()
        {
            CharData character = null;
            foreach(CharData chara in FieldManager.instance.allChar)
            {
                if (chara.isEnemy)
                    if (FieldManager.instance.CheckCharIsFrontest(chara))
                    {
                        character = chara;
                        break;
                    }
            }

            if (character == null) return;
            if (damagerChar != null)
                damageValue += damagerChar.power;

            DamageInfo dif = TriggerManager.instance.GetTriggerInfo<DamageInfo>();
            dif.SetInfo(character, damageValue, damageType, damagerChar);

            dif.GoTrigger(TriggerType.DamageCheck);
            dif.GoTrigger(TriggerType.DamageTotalCheck);
            dif.GoTrigger(TriggerType.DamageFinalCheck);
            dif.Computer();

            dif.GoTrigger(TriggerType.DamageAfter);
            OrderManager.instance.AddOrder(new sysOrder.DamageOrder(dif.damagedChar, dif.damageValue, dif.damageType, dif.damagerChar));
            dif.GoTrigger(TriggerType.DamageBefore);
        }
    }
    public class DamageOrder : Order
    {
        CharData character;
        CharData damagerChar;
        int damageValue;
        DamageType damageType;
        public DamageOrder(CharData _character,int _damageValue,DamageType _damageType,CharData _damagerChar)
        {
            character = _character;
            damageValue = _damageValue;
            damageType = _damageType;

            damagerChar = _damagerChar;
        }
        public override void Execution()
        {
            FieldManager.instance.RealDamage(character, damageValue, damageType, damagerChar);

        }
    }

    public class DamagePapareRandomGiveStatus : Order
    {
        CharFilter[] filters;
        CharData damagerChar;
        int damageValue;
        DamageType damageType;

        string status;
        int statusNum;

        public DamagePapareRandomGiveStatus(string _status, int _statusNum,CharFilter[] _filters, int _damageValue, DamageType _type, CharData _damager = null)
        {
            status = _status;
            statusNum = _statusNum;

            filters = _filters;
            damageValue = _damageValue;
            damageType = _type;
            damagerChar = _damager;
        }

        public override void Execution()
        {
            CharData character = FieldManager.instance.GetRandomChar(filters);

            if (character == null) return;
            if (damagerChar != null)
                damageValue += damagerChar.power;

            DamageInfo dif = TriggerManager.instance.GetTriggerInfo<DamageInfo>();
            dif.SetInfo(character, damageValue, damageType, damagerChar);

            dif.GoTrigger(TriggerType.DamageCheck);
            dif.GoTrigger(TriggerType.DamageTotalCheck);
            dif.GoTrigger(TriggerType.DamageFinalCheck);
            dif.Computer();

            dif.GoTrigger(TriggerType.DamageAfter);
            OrderManager.instance.AddOrder(new sysOrder.DamageOrder(dif.damagedChar, dif.damageValue, dif.damageType, dif.damagerChar));
            dif.GoTrigger(TriggerType.DamageBefore);

            FieldManager.instance.GiveCharStatus(character, status, statusNum);
        }
    }

    //===================================

    public class UseSkillOrder : Order
    {
        Skill skill;
        CharData target;

        public UseSkillOrder(Skill _skill,CharData _target)
        {
            skill = _skill;
            target = _target;
        }
        public override void Execution()
        {
            skill.Excite(target);
        }
    }
    public class UseCardOrder : Order
    {
        CardData card;
        public UseCardOrder(CardData _card)
        {
            card = _card;
        }

        public override void Execution()
        {
            CardManager.instance.RealUseCard(card);
        }
    }

    public class DealthOrder : Order
    {
        CharData character;

        public DealthOrder(CharData _chara)
        {
            character = _chara;
        }
        public override void Execution()
        {
            FieldManager.instance.RealDealth(character);
        }
    }
    public class DrawOrder : Order
    {
        public DrawOrder()
        {
        }
        public override void Execution()
        {
            CardManager.instance.Draw();
        }
    }
    public class DiscardOrder : Order
    {
        CardData card;
        public DiscardOrder(CardData _card)
        {
            card = _card;
        }
        public override void Execution()
        {
            CardManager.instance.DiscardHandCard(card);
        }
    }
    public class ReduceEnergy : Order
    {
        int energyNum;
        public ReduceEnergy(int _energyNum)
        {
            energyNum = _energyNum;
        }

        public override void Execution()
        {
            FieldManager.instance.currentActionCharacter.ReduceEnergy(energyNum);
        }
    }
    public class GainMagic : Order
    {
        int magicNum;
        CharData character;
        public GainMagic(CharData chara,int _magic)
        {
            magicNum = _magic;
            character = chara;
        }

        public override void Execution()
        {
            FieldManager.instance.CharGainMagic(character, magicNum);
        }
    }
    public class CardBurst : Order
    {
        CardData card;
        public CardBurst(CardData _card)
        {
            card = _card;
        }

        public override void Execution()
        {
            CardManager.instance.CardBurst(card);
        }
    }
    public class ArmorChar : Order
    {
        int armorNum;
        CharData character;
        CharData user;
        public ArmorChar(CharData chara, int armor,CharData _user=null)
        {
            character = chara;
            armorNum = armor;
            user = _user;
        }

        public override void Execution()
        {
            FieldManager.instance.ArmorChar(character, armorNum, user);
        }
    }
    public class ArmorCharMulti : Order
    {
        int armorNum;
        CharFilter[] filters;
        CharData user;
        public ArmorCharMulti(CharFilter[] _filters, int armor, CharData _user = null)
        {
            filters = _filters;
            armorNum = armor;
            user = _user;
        }

        public override void Execution()
        {
            CharData[] chars = FieldManager.instance.GetConditionChar(filters);
            foreach (CharData chara in chars)
                FieldManager.instance.ArmorChar(chara, armorNum, user);
        }
    }

    public class HealOrder : Order
    {
        int healNum;
        CharData character;
        CharData user;
        public HealOrder(CharData chara, int _healNum, CharData _user = null)
        {
            character = chara;
            healNum = _healNum;
            user = _user;
        }

        public override void Execution()
        {
            FieldManager.instance.HealChar(character, healNum, user);
        }
    }
    public class HealOrderMulti : Order
    {
        int healNum;
        CharFilter[] filters;
        CharData user;
        public HealOrderMulti(CharFilter[] _filters, int _healNum, CharData _user = null)
        {
            filters = _filters;
            healNum = _healNum;
            user = _user;
        }

        public override void Execution()
        {
            CharData[] chars = FieldManager.instance.GetConditionChar(filters);
            foreach (CharData chara in chars)
                FieldManager.instance.HealChar(chara, healNum, user);
        }
    }
    public class HealRandom : Order
    {
        int healNum;
        CharFilter[] filters;
        CharData user;
        public HealRandom(CharFilter[] _filters, int _healNum, CharData _user = null)
        {
            filters = _filters;
            healNum = _healNum;
            user = _user;
        }

        public override void Execution()
        {
            CharData chara = FieldManager.instance.GetRandomChar(filters);
            if(chara!=null)
                FieldManager.instance.HealChar(chara, healNum, user);
        }
    }
    public class GainEnergy : Order
    {
        public int energy;
        public CharData character;
        public GainEnergy(CharData chara,int energyNum)
        {
            energy = energyNum;
            character = chara;
        }

        public override void Execution()
        {
            FieldManager.instance.GainEnergy(character,energy);
        }
    }
    public class GainPower : Order
    {
        public int power;
        public CharData character;
        public GainPower(CharData chara, int powerNum)
        {
            power = powerNum;
            character = chara;
        }

        public override void Execution()
        {
            FieldManager.instance.GainPower(character, power);
        }
    }
    public class GiveCharStatus : Order
    {
        public CharData character;
        public string statusName;
        public int statusNum;
        public GiveCharStatus(CharData chara,string _statusName,int _statusNum)
        {
            character = chara;
            statusName = _statusName;
            statusNum = _statusNum;
        }

        public override void Execution()
        {
            FieldManager.instance.GiveCharStatus(character, statusName, statusNum);
        }
    }
    public class GiveCharStatusMulti : Order
    {
        public CharFilter[] filters;
        public string statusName;
        public int statusNum;
        public GiveCharStatusMulti(CharFilter[] _filters, string _statusName, int _statusNum)
        {
            filters = _filters;
            statusName = _statusName;
            statusNum = _statusNum;
        }

        public override void Execution()
        {
            CharData[] chars = FieldManager.instance.GetConditionChar(filters);
            foreach(CharData chara in chars)
                FieldManager.instance.GiveCharStatus(chara, statusName, statusNum);
        }
    }
    public class ChangeToFront : Order
    {
        public CharData character;
        public ChangeToFront(CharData chara)
        {
            character = chara;
        }

        public override void Execution()
        {
            FieldManager.instance.ChangeTeamPos(character,TeamPos.Front);
        }
    }
    public class NewCardToDeck : Order
    {
        public int cardNo;
        bool top;
        public NewCardToDeck(int _cardNo,bool _top = false)
        {
            cardNo = _cardNo;
            top = _top;
        }

        public override void Execution()
        {
            CardData card = CardCreator.CreateCard(cardNo);
            if (card != null)
            {
                if (top)
                {
                    CardManager.instance.CardMove(card, CardPos.Deck, 1);
                }
                else
                {
                    CardManager.instance.CardMove(card, CardPos.Deck);
                }
            }
        }
    }
    public class NewCardToHand : Order
    {
        public int cardNo;
        public NewCardToHand(int _cardNo)
        {
            cardNo = _cardNo;
        }

        public override void Execution()
        {
            CardData card = CardCreator.CreateCard(cardNo);
            if (card != null)
            {
                CardManager.instance.CardMove(card, CardPos.Hand);
            }
        }
    }
    public class MoveCard : Order
    {
        CardData card;
        CardPos to;
        public MoveCard(CardData _card,CardPos _to)
        {
            card = _card;
            to = _to;
        }

        public override void Execution()
        {
            CardManager.instance.CardMove(card, to);
        }
    }
    public class CardCostAdjust : Order
    {
        public int adjustNum;
        public CardData card;
        public CardCostAdjust(CardData _card,int _adjustNum)
        {
            adjustNum = _adjustNum;
            card = _card;
        }

        public override void Execution()
        {
            card.CostAdjust(adjustNum);
        }
    }

}

namespace nOrder
{
    public class HandCardCostAdjust : Order
    {
        int adjustNum;
        public HandCardCostAdjust(int _adjustNum)
        {
            adjustNum = _adjustNum;
        }
        public override void Execution()
        {
            foreach(CardData card in CardManager.instance.handCards)
            {
                OrderManager.instance.AddOrder(new sysOrder.CardCostAdjust(card, adjustNum));
            }
        }
    }
    public class GainMagicIfTargetDie : Order
    {
        int magic;
        CharData target;
        CharData self;
        public GainMagicIfTargetDie(int magicNum,CharData _target,CharData _self)
        {
            magic = magicNum;
            target = _target;
            self = _self;
        }
        public override void Execution()
        {
            if (target.isDie)
            {
                FieldManager.instance.CharGainMagic(self, magic);
            }
        }
    }

}

namespace pOrder
{
    public class EnemyActionOrder : Order
    {
        EnemyAction action;
        public EnemyActionOrder(EnemyAction _action)
        {
            action = _action;
        }
        public override void Execution()
        {
            action.Action();
        }
    }
    public class TurnEndButton : Order
    {
        public override void Execution()
        {
            //if this order is not first orer in stack, this order is not available
            FieldManager.instance.CharTurnEnd();
        }
    }
}