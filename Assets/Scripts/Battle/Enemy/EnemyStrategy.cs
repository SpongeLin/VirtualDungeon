using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStrategy 
{
    public EnemyAction currentAction;
    public EnemyAction nextAction;
    public bool strategyShow = true;

    bool firstAction = true;
    int linkNum = 0;
    public void AddAction(EnemyAction action)
    {
        if (firstAction)
        {
            currentAction = action;
            action.next = action;
            nextAction = action.next;

            firstAction = false;
        }
        else
        {

        }
    }

    public void TurnStart()
    {
        OrderManager.instance.AddOrder(new pOrder.TurnEndButton());
        OrderManager.instance.AddOrder(new sysOrder.WaitOrder(0.5f));
        OrderManager.instance.AddOrder(new pOrder.EnemyActionOrder(currentAction));
        OrderManager.instance.AddOrder(new sysOrder.WaitOrder(0.5f));
        currentAction = nextAction;
        nextAction = nextAction.next;

        strategyShow = false;
    }
    public void TurnEnd()
    {
        strategyShow = true;
    }

}

public abstract class EnemyAction
{
    public EnemyAction next;
    public string actionImage;
    public string actionContent;
    public string actionDescription;
    public abstract void Action();
}
namespace nEnemyAction
{
    public class DamageSelf : EnemyAction
    {

        public int damageValue;
        public DamageSelf(int _damageValue)
        {
            damageValue = _damageValue;
            actionContent = _damageValue.ToString();
        }

        public override void Action()
        {
            FieldManager.instance.DamageChar(FieldManager.instance.currentActionCharacter, damageValue, DamageType.Normal, FieldManager.instance.currentActionCharacter);
        }
    }
}