using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStrategy 
{
    public EnemyAction currentAction;

    public bool strategyShow = true;

    bool firstAction = true;
    int linkNum = 0;
    public void AddAction(EnemyAction action)
    {
        if (firstAction)
        {
            currentAction = action;
            action.next = action;
            

            firstAction = false;
        }
        else
        {
            EnemyAction temp = currentAction;
            for (int i = 0; i < linkNum; i++)
                temp = temp.next;
            temp.next = action;
            action.next = currentAction;
            linkNum++;
        }
    }

    public void TurnStart()
    {
        OrderManager.instance.AddOrder(new pOrder.TurnEndButton());
        OrderManager.instance.AddOrder(new sysOrder.WaitOrder(0.5f));
        OrderManager.instance.AddOrder(new pOrder.EnemyActionOrder(currentAction));
        OrderManager.instance.AddOrder(new sysOrder.CharMoveOrder());
        OrderManager.instance.AddOrder(new sysOrder.WaitOrder(0.5f));
        currentAction = currentAction.next;

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

    public string actionName;
    public virtual string GetDescription()
    {
        return "無說明";
    }
    public abstract void Action();
}
namespace nEnemyAction
{
    public class DamageRandom : EnemyAction
    {

        public int damageValue;
        public DamageRandom(int _damageValue)
        {
            damageValue = _damageValue;
            actionContent = _damageValue.ToString();
            actionImage = "NormalDamage";
        }

        public override void Action()
        {
            OrderManager.instance.AddOrder(new sysOrder.DamagePapareRandom(new CharFilter[] {new CharFilter("Camps",1) }, damageValue, DamageType.Normal, FieldManager.instance.currentActionCharacter));
        }
    }
    public class DamageRandomTimes : EnemyAction
    {

        public int damageValue;
        int times;
        public DamageRandomTimes(int _damageValue,int _times)
        {
            damageValue = _damageValue;
            actionContent = _damageValue.ToString();
            if (_times > 1)
                actionContent += "x" + _times;
            actionImage = "NormalDamage";

            times = _times;
        }

        public override void Action()
        {
            OrderManager.instance.AddOrder(new sysOrder.DamagePapareRandom(new CharFilter[] { new CharFilter("Camps", 1) }, damageValue, DamageType.Normal, FieldManager.instance.currentActionCharacter,times));
        }
    }
    public class DamageFront : EnemyAction
    {

        public int damageValue;
        public DamageFront(int _damageValue)
        {
            damageValue = _damageValue;
            actionContent = _damageValue.ToString();
            actionImage = "DamageFront";
        }

        public override void Action()
        {
            OrderManager.instance.AddOrder(new sysOrder.DamagePapareFront( damageValue, DamageType.Normal, FieldManager.instance.currentActionCharacter));
        }
    }
    public class DamageAllHero : EnemyAction
    {

        public int damageValue;
        public DamageAllHero(int _damageValue)
        {
            damageValue = _damageValue;
            actionContent = _damageValue.ToString();
            actionImage = "NormalDamage";
        }

        public override void Action()
        {
            OrderManager.instance.AddOrder(new sysOrder.DamagePapareMulti(new CharFilter[] {new CharFilter("Camps",1) },damageValue, DamageType.Normal, FieldManager.instance.currentActionCharacter));
        }
    }

    public class DamageWithFragment : EnemyAction
    {

        public int damageValue;
        public int cardNum;
        public DamageWithFragment(int _damageValue, int _cardNum)
        {
            damageValue = _damageValue;
            cardNum = _cardNum;

            actionContent = _damageValue.ToString();
            actionImage = "SpecialDamage";
        }

        public override void Action()
        {
            OrderManager.instance.AddOrder(new sysOrder.DamagePapareRandom(new CharFilter[] { new CharFilter("Camps", 1) }, damageValue, DamageType.Normal, FieldManager.instance.currentActionCharacter));
            OrderManager.instance.AddOrder(new sysOrder.NewCardToHand(700));
            //        Fragile
        }
    }
    public class DamageGiveFragile : EnemyAction
    {

        public int damageValue;
        public int statusNum;
        public DamageGiveFragile(int _damageValue,int _statusNum)
        {
            damageValue = _damageValue;
            statusNum = _statusNum;

            actionContent = _damageValue.ToString();
            actionImage = "SpecialDamage";
        }

        public override void Action()
        {
            OrderManager.instance.AddOrder(new sysOrder.DamagePapareRandomGiveStatus("Fragile", statusNum,new CharFilter[] { new CharFilter("Camps", 1) }, damageValue, DamageType.Normal, FieldManager.instance.currentActionCharacter));
            //        Fragile
        }
    }

    public class DamageGiveWeak : EnemyAction
    {

        public int damageValue;
        public int statusNum;
        public DamageGiveWeak(int _damageValue, int _statusNum)
        {
            damageValue = _damageValue;
            statusNum = _statusNum;

            actionContent = _damageValue.ToString();
            actionImage = "SpecialDamage";
        }

        public override void Action()
        {
            OrderManager.instance.AddOrder(new sysOrder.DamagePapareRandomGiveStatus("Weak", statusNum, new CharFilter[] { new CharFilter("Camps", 1) }, damageValue, DamageType.Normal, FieldManager.instance.currentActionCharacter));
            //        Fragile
        }
    }
    public class HealRandomAlly : EnemyAction
    {

        public int healValue;
        public HealRandomAlly(int _healValue)
        {
            healValue = _healValue;

            actionContent = healValue.ToString();
            actionImage = "Heal";
        }

        public override void Action()
        {
            OrderManager.instance.AddOrder(new sysOrder.HealRandom(new CharFilter[] { new CharFilter("Camps", 2) }, healValue, FieldManager.instance.currentActionCharacter));
        }
    }
    public class HealAllAlly : EnemyAction
    {

        public int healValue;
        public HealAllAlly(int _healValue)
        {
            healValue = _healValue;

            actionContent = healValue.ToString();
            actionImage = "HealAllAlly";
        }

        public override void Action()
        {
            OrderManager.instance.AddOrder(new sysOrder.HealOrderMulti(new CharFilter[] { new CharFilter("Camps", 2) }, healValue, FieldManager.instance.currentActionCharacter));
        }
    }
    public class ArmorSelf : EnemyAction
    {

        public int armorValue;
        public ArmorSelf(int _armorValue)
        {
            armorValue = _armorValue;
            actionContent = _armorValue.ToString();
            actionImage = "Armor";
        }

        public override void Action()
        {
            OrderManager.instance.AddOrder(new sysOrder.ArmorChar(FieldManager.instance.currentActionCharacter, armorValue, FieldManager.instance.currentActionCharacter));
        }
    }
    public class GainPower : EnemyAction
    {

        public int power;
        public GainPower(int powerNum)
        {
            power = powerNum;
            actionContent = powerNum.ToString();
            actionImage = "Power";
        }

        public override void Action()
        {
            OrderManager.instance.AddOrder(new sysOrder.GainPower(FieldManager.instance.currentActionCharacter,power));
        }
    }

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
            OrderManager.instance.AddOrder(new sysOrder.DamagePrepare(FieldManager.instance.currentActionCharacter,damageValue,DamageType.Normal,FieldManager.instance.currentActionCharacter));
        }
    }
    public class Test : EnemyAction
    {
        public string actionDescription;
        public Test(string text)
        {
            actionDescription = text;
        }
        public override void Action()
        {
            Debug.Log("testText");
        }
        public override string GetDescription()
        {
            return actionDescription;
        }
    }
}