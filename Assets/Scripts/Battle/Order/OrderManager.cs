using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{

    public static OrderManager instance { get; private set; }

    public Stack<Order> orderStack;

    float waitTime = 0;
    public bool isWaiting = false;

    private void Awake()
    {
        instance = this;
        orderStack = new Stack<Order>();
    }

    private void Update()
    {
        if (isWaiting) return;
        if (orderStack.Count != 0 && FieldManager.instance.isWorking)
        {
            if (waitTime <= 0)
            {
                Order currentOrder = orderStack.Pop();
                currentOrder.Execution();
                //Call CardUpdate()

                TriggerManager.instance.GameUpdate();
            }
        }

    }
    private void FixedUpdate()
    {
        if (waitTime > 0)
            waitTime -= Time.deltaTime;
    }
    public void SetWait(float _waitTime)
    {
        waitTime = _waitTime;
    }
    public void AddOrder(Order order)
    {
        orderStack.Push(order);
    }
    public bool IsEmptyStack()
    {
        if (orderStack.Count == 0 && waitTime<=0)
            return true;
        return false;
    }
    public void SetWait(bool enable)
    {
        isWaiting = enable;
    }


}
