using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Status : Subscriber
{
    public string statusName;
    public string statusShowName;
    public string statusDescription;
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


    public abstract void Enter();
    public abstract void Exit();

    public string statusImage;
    //public string showName;
    public string GetDescription(){return statusDescription; }
    public string GetName()
    {
        string showName = statusShowName;
        if (byNum)
            showName += num + "層";
        else if (byTime)
            showName += time + "回合";

        return showName;
    }
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
    public bool notDisplay = false;
    public bool negativeStatus;
    public CharData character;


}
public abstract class FieldStatus : Status
{

}
public abstract class CardStatus : Status
{
    public CardData card;
}