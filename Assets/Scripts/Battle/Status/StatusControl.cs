using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusControl : Subscriber
{
    public List<Status> statusList;
    public bool isEnemy;
    public StatusControl()
    {
        isWorking = true;
        statusList = new List<Status>();
        TriggerManager.instance.AddUpdateList(this);

        SetSubscription<TurnInfo>(TriggerType.TurnEnding, 1);
    }
    public bool CheckStatus(Status status)
    {

        bool repeat = false;
        foreach (Status s in statusList)
        {
            if (s.statusName == status.statusName)
            {
                if (s.canRepeat)
                {
                    s.Repeat(status);
                }
                repeat = true;
                break;
            }
        }
        if (!repeat)
        {
            statusList.Add(status);
            status.isWorking = true;
            status.Enter();
            return true;
        }
        return false;

    }
    public void CardUpdate()
    {
        foreach (Status s in statusList)
        {
            if (s.isWorking)
            {
                if ((s.num <= 0 || s.time <= 0) && !s.eternal)
                    s.Exit();
                s.Update();
            }
        }

        List<Status> removeList = null;
        foreach (Status s in statusList)
        {
            if (!s.isWorking)
            {

                if (removeList == null)
                    removeList = new List<Status>();
                removeList.Add(s);
            }
        }
        if (removeList != null)
        {
            foreach (Status s in removeList)
                statusList.Remove(s);
        }
    }
    public void ExitAll()
    {
        foreach (CharStatus cs in statusList)
        {
            if (cs.isWorking) cs.Exit();
        }
    }

    public override void Trigger1()
    {
        if (FieldManager.instance.playerTurn == isEnemy)
            return;
        foreach (Status s in statusList)
        {
            if (!s.eternal)
            {
                s.time--;
            }
        }
    }

    public override void Update()
    {
        CardUpdate();
    }
}

public class CharStatusControl : StatusControl
{
    public CharData chara;
    public CharStatusControl(CharData _chara) : base()
    {
        chara = _chara;
    }
    public void EnterStatus(CharStatus status)
    {
        if (CheckStatus(status))
            status.character = chara;
    }
}