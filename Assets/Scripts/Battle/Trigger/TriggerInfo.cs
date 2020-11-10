using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class TriggerInfo
{
    public class SubscriberData
    {
        public SubscriberData(Subscriber _subscriber, TriggerType _type, int _port)
        {
            subscriber = _subscriber;
            type = _type;
            port = _port;
        }
        public Subscriber subscriber;
        public TriggerType type;
        public int port;
    }
    public TriggerInfo()
    {
        subscriberDatas = new List<SubscriberData>();
    }
    public List<SubscriberData> subscriberDatas;
    public string test;

    //public abstract void Reset();
    public TriggerInfo SubscriptionButton(TriggerType type, int port, Subscriber s)
    {
        //Debug.Log("SUB!");
        subscriberDatas.Add(new SubscriberData(s, type, port));
        return this;
    }
    public void GoTrigger(TriggerType type)
    {
        subscriberDatas.RemoveAll(DontWorkSubscriber);
        foreach (SubscriberData data in subscriberDatas)
        {
            if (!data.subscriber.isWorking) Debug.LogError("subscriberData that isWorking is false is exist.");

            if (type == data.type)
            {
                if (data.port == 1)
                {
                    data.subscriber.Trigger1();
                }
                else if (data.port == 2)
                {
                    data.subscriber.Trigger2();
                }
                else if (data.port == 3)
                {
                    data.subscriber.Trigger3();
                }
            }
        }
    }

    private static bool DontWorkSubscriber(SubscriberData sd)
    {
        return !sd.subscriber.isWorking;
    }
}

//=======================================================================


public class DamageInfo : TriggerInfo
{
    public CharData damagedChar;
    public CharData damagerChar;
    public int damageValue;
    public int extraDamageValue;
    public float multiDamageValue;

    public DamageType damageType;

    public DamageInfo() : base()
    {
        test = "Damage";
    }

    public void SetInfo(CharData chara, int _damageValue, DamageType _damageType ,CharData  _damager)
    {
        damagedChar = chara;
        damagerChar = _damager;

        damageValue = _damageValue;
        damageType = _damageType;
        extraDamageValue = 0;
        multiDamageValue = 1;
    }
    public void Computer()
    {
        damageValue += extraDamageValue;
        damageValue = (int)(damageValue*multiDamageValue);
    }
}
public class UseCardInfo : TriggerInfo
{
    public CardData card;
    public UseCardInfo() : base()
    {
        test = "UseCard";
    }
    public void SetInfo(CardData _card)
    {
        card = _card;
    }

}
public class TurnInfo : TriggerInfo
{
    public CharData character;
    public bool isEnemyTurn;
    public TurnInfo() : base()
    {

    }
    public void SetInfo(CharData _character)
    {
        character = _character;
        isEnemyTurn = character.isEnemy;
    }
}

public class SkillInfo : TriggerInfo
{
    public Skill skill;
    public CharData target;

    public SkillInfo() : base()
    {
        test = "Damage";
    }

    public void SetInfo(Skill _skill,CharData _target)
    {
        skill = _skill;
        target = _target;

    }
}

public class CharInfo : TriggerInfo
{
    public CharData character;
    public int argument1;
    public bool argument2;
    public CharData argumentChar;

    public CharInfo() : base()
    {

    }
    public void SetInfo(CharData _chara)
    {
        character = _chara;

        argument1 = 0;
        argument2 = false;
        argumentChar = null;
    }
    public void SetInfo(CharData _chara, int _argument1, bool _argument2 = false)
    {
        character = _chara;
        argument1 = _argument1;
        argument2 = _argument2;

        argumentChar = null;
    }
    public void SetInfo(CharData _chara, int _argument1, CharData _char)
    {
        character = _chara;
        argument1 = _argument1;
        argumentChar = _char;

        argument2 = false;
    }
}
public class CardInfo : TriggerInfo
{
    public CardData card;
    public int burstNum;
    public CardPos from;
    public CardPos to;

    public CardInfo() : base()
    {

    }
    public void SetInfo(CardData _card, int _burstNum)
    {
        card = _card;
        burstNum = _burstNum;
        from = CardPos.Null;
        to = CardPos.Null;
    }
    public void SetInfo(CardData _card)
    {
        card = _card;
        burstNum = 0;
        from = CardPos.Null;
        to = CardPos.Null;
    }
    public void SetInfo(CardData _card, CardPos _form,CardPos _to)
    {
        card = _card;
        burstNum = 0;
        from = _form;
        to = _to;
    }
}