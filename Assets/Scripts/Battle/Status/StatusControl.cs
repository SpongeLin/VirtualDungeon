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
        if (TriggerManager.instance != null)
            TriggerManager.instance.AddUpdateList(this);
        else Debug.Log("not update");

        SetSubscription<TurnInfo>(TriggerType.TurnEnding, 1);
    }
    public bool CheckStatus(Status status)
    {

        bool willAdd = true;
        foreach (Status s in statusList)
        {
            if (s.statusName == status.statusName)
            {
                if (s.canRepeat)
                {
                    s.Repeat(status);
                    willAdd = false;
                }
                if(s.onlyOne)
                    willAdd = false;
                break;
            }
        }
        if (willAdd)
        {
            statusList.Add(status);
            status.isWorking = true;
            status.Enter();
            return true;
        }
        return false;

    }
    public void StatusUpdate()
    {
        foreach (Status s in statusList)
        {
            if (s.isWorking)
            {
                if ((s.num <= 0 || s.time <= 0) && !s.eternal)
                {
                    s.isWorking = false;
                    s.Exit();
                }
                    
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
        foreach (Status s in statusList)
        {
            if (s.isWorking && !s.original)
            {
                s.isWorking = false;
                s.Exit();
            }
        }
    }

    public override void Update()
    {
        StatusUpdate();
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
        status.character = chara;
        CheckStatus(status);

    }
    public override void Trigger1()
    {
        if (FieldManager.instance.currentActionCharacter != chara)
            return;
        foreach (Status s in statusList)
        {
            if (!s.eternal)
            {
                s.time--;
            }
            if (s.thisTurn)
            {
                s.isWorking = false;
                s.Exit();
            }
        }
    }
}
public class CardStatusControl : StatusControl
{
    public CardData card;
    public CardStatusControl(CardData _card) : base()
    {
        card = _card;
    }
    public void EnterStatus(CardStatus status)
    {
        status.card = card;
        CheckStatus(status);


    }
    public override void Trigger1()
    {
        foreach (Status s in statusList)
        {
            if (!s.eternal)
            {
                s.time--;
            }
        }
    }
}
public class FieldStatusControl : StatusControl
{
    public FieldStatusControl() : base()
    {

    }
    public void EnterStatus(FieldStatus status)
    {
        CheckStatus(status);
    }
    public override void Trigger1()
    {
        foreach (Status s in statusList)
        {
            if (!s.eternal)
            {
                s.time--;
            }
        }
    }
}