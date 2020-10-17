using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class StatusCreator
{
    public static CharStatus CreateCharStatus(string charStatusName,int statusNum)
    {
        switch (charStatusName)
        {
            case "DamageToZero":
                return new nCharStatus.DamageToZero(statusNum);
        }

        Debug.LogWarning("This charStatus is not exist.");
        return null;
    }
    //===============================================================
    public static CardStatus CreateCardStatus(string cardStatusName, int statusNum)
    {
        switch (cardStatusName)
        {

        }

        Debug.LogWarning("This cardStatus is not exist.");
        return null;
    }
    //===============================================================
    public static FieldStatus CreateFieldStatus(string fieldStatusName, int statusNum)
    {
        switch (fieldStatusName)
        {

        }

        Debug.LogWarning("This fieldStatus is not exist.");
        return null;
    }
}
namespace nCharStatus
{
    public class DamageToZero : CharStatus
    {
        DamageInfo dif;
        public DamageToZero(int _time)
        {
            statusName = "Invincible";
            byTime = true;
            time = _time;
        }
        public override void Enter()
        {

            dif = SetSubscription<DamageInfo>(TriggerType.DamageTotalCheck, 1);
        }
        public override void Trigger1()
        {
            Debug.Log("STATUS");
            dif.damageValue = 0;
        }

        public override void Exit()
        {
            
        }

        public override string GetDescription()
        {
            return "受到的所有傷害降至0。";
        }

        public override void Update()
        {
            
        }
    }
}