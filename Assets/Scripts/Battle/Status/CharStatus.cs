using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
