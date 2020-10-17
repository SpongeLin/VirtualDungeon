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
            //進行防禦計算!!

            character.ReduceHealth(damageValue);

            FieldManager.instance.battleTextControl.ShowDamageText(character, damageValue, TextType.Damage);
            Debug.Log(character.charShowName + " 受到 " + damageValue + " 點傷害!");
        }
    }
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