using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TriggerType
{
    DamageCheck,
    DamageTotalCheck,
    DamageFinalCheck,
    DamageAfter,
    DamageBefore,
    UseCardCheck,
    UseCardAfter,
    UseCardBefore,
    TurnStarting,
    TurnStartBefore,
    TurnEnding,
    TurnEndBefore,

}

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
        subscriberDatas.Add(new SubscriberData(s, type, port));
        return this;
    }
    public void GOTrigger(TriggerType type)
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
    public int damageValue;

    public bool trueDamage;

    public DamageInfo() : base()
    {
        test = "Damage";
    }

    public override void Reset()
    {
        throw new System.NotImplementedException();
    }

    public void SetInfo(CharData chara, int _damageValue, bool _trueDamage = false)
    {
        damagedChar = chara;
        damageValue = _damageValue;
        trueDamage = _trueDamage;

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
    public TurnInfo() : base()
    {

    }
    public override void Reset()
    {
        throw new System.NotImplementedException();
    }
}