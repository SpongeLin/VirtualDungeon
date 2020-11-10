using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Status : Subscriber
{
    public string statusName;
    public string statusShowName;
    public int time = 1;
    public int num = 1;

    public bool byTime;
    public bool byNum;
    public bool onlyOne;
    public bool thisTurn;
    public bool eternal;
    public bool original;

    public bool canRepeat { get { return byTime || byNum; } }
    public void Repeat(Status s)
    {
        if (byTime)
            time += s.time;
        else if (byNum)
            num += s.num;
    }

    public bool display = false;

    public abstract void Enter();
    public abstract void Exit();

    public string statusImage;
    //public string showName;
    public virtual string GetDescription(){return "";}
    public string GetNum()
    {
        if (byNum)
            return num.ToString();
        if (byTime)
            return time.ToString();
        return "";
    }


}


// Character Status
public abstract class CharStatus : Status
{
    public CharData character;


}
public abstract class FieldStatus : Status
{

}
public abstract class CardStatus : Status
{
    public CardData card;
}