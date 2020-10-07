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

    public abstract void Reset();
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

    public DamageType damageType;

    public DamageInfo() : base()
    {
        test = "Damage";
    }

    public override void Reset()
    {
        damagedChar = null;
        damagerChar = null;
        damageValue = 0;
        damageType = DamageType.Null;
    }

    public void SetInfo(CharData chara, int _damageValue, DamageType _damageType ,CharData  _damager)
    {
        damagedChar = chara;
        damagerChar = _damager;

        damageValue = _damageValue;
        damageType = _damageType;

    }
}
public class UseCardInfo : TriggerInfo
{
    public UseCardInfo() : base()
    {
        test = "UseCard";
    }

    public override void Reset()
    {
        throw new System.NotImplementedException();
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
    public override void Reset()
    {
        character = null;
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

    public override void Reset()
    {
        skill = null;
        target = null;
    }

    public void SetInfo(Skill _skill,CharData _target)
    {
        skill = _skill;
        target = _target;

    }
}
public class DealthInfo : TriggerInfo
{
    public CharData character;
    public DealthInfo() : base()
    {

    }
    public override void Reset()
    {
        character = null;
    }
    public void SetInfo(CharData _chara)
    {
        character = _chara;
    }
}
