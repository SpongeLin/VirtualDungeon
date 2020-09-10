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

}