using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Subscriber
{
    public bool isWorking = false;
    public virtual void Trigger1() { Debug.LogWarning("The Trigger1 is not defined."); }
    public virtual void Trigger2() { Debug.LogWarning("The Trigger2 is not defined."); }
    public virtual void Trigger3() { Debug.LogWarning("The Trigger3 is not defined."); }
    public abstract void Update();

    public T SetSubscription<T>(TriggerType type, int port) where T : TriggerInfo
    {
        if (TriggerManager.instance == null)
        {
            Debug.Log("Subscription Error");
            return null;
        }
            
        return (T)TriggerManager.instance.GetTriggerInfo<T>().SubscriptionButton(type, port, this);
    }

}