using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerManager : MonoBehaviour
{
    public static TriggerManager instance { get; private set; }

    public List<TriggerInfo> triggerInfoList;
    public List<Subscriber> updateList;

    private void Awake()
    {
        instance = this;
        updateList = new List<Subscriber>();

        //set TriggerInfo instance
        triggerInfoList = new List<TriggerInfo>();
        triggerInfoList.Add(new DamageInfo());
        triggerInfoList.Add(new UseCardInfo());
        triggerInfoList.Add(new TurnInfo());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            UseCardInfo uc = GetTriggerInfo<UseCardInfo>();
            //Debug.Log(uc.test);
            DamageInfo dif = GetTriggerInfo<DamageInfo>();
            //Debug.Log(dif.test);
        }
    }
    public void AddUpdateList(Subscriber s)
    {
        updateList.Add(s);
    }
    public void CardUpdate()
    {
        //foreach updatelist Subscriber.Update() 
        updateList.RemoveAll(DontWorkSubscriber);

        //CardManager.instance.CardUpdate();
        //FieldManager.instance.CardUpdate();
    }

    public T GetTriggerInfo<T>() where T : TriggerInfo
    {
        System.Type type = typeof(T);

        foreach (TriggerInfo ti in triggerInfoList)
        {
            if (type.Name == ti.GetType().Name)
                return (T)ti;
        }

        Debug.LogWarning("Find not TriggerInfo");
        return null;
    }


    private static bool DontWorkSubscriber(Subscriber s)
    {
        return !s.isWorking;
    }






}
