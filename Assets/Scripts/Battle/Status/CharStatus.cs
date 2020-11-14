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
            statusShowName = "絕對和平";
            statusName = "Invincible";
            byTime = true;
            time = _time;
            statusDescription = "所有傷害歸零";
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

        public override void Update()
        {
            
        }
    }
    public class Fragile : CharStatus
    {
        DamageInfo dif;
        public Fragile(int _time)
        {
            statusShowName = "脆弱";
            statusName = "Fragile";
            byTime = true;
            time = _time;
            statusDescription = "受到的傷害提高50%";
        }
        public override void Enter()
        {
            dif = SetSubscription<DamageInfo>(TriggerType.DamageTotalCheck,1);
        }
        public override void Trigger1()
        {
            if (dif.damagedChar == character)
            {
                dif.extraDamageValue += dif.damageValue / 2;
            }
        }

        public override void Exit()
        {
           // throw new System.NotImplementedException();
        }

        public override void Update()
        {
            //throw new System.NotImplementedException();
        }
    }

    public class Weak : CharStatus
    {
        DamageInfo dif;
        public Weak(int _time)
        {
            statusShowName = "虛弱";
            statusName = "Weak";
            byTime = true;
            time = _time;
            statusDescription = "造成的傷害減少50%";
        }
        public override void Enter()
        {
            dif = SetSubscription<DamageInfo>(TriggerType.DamageTotalCheck, 1);
        }
        public override void Trigger1()
        {
            if (dif.damagerChar == character)
            {
                dif.extraDamageValue -= dif.damageValue / 2;
            }
        }

        public override void Exit()
        {
            // throw new System.NotImplementedException();
        }

        public override void Update()
        {
            //throw new System.NotImplementedException();
        }
    }
    public class Guard : CharStatus
    {
        public Guard(int _time)
        {
            statusShowName = "守護";
            statusName = "Guard";
            byTime = true;
            time = _time;
            statusDescription = "必須優先選擇這個角色";

            notDisplay = true;
        }
        public override void Enter()
        {
            character.guardCount++;
        }
        public override void Exit()
        {
            character.guardCount--;
            // throw new System.NotImplementedException();
        }

        public override void Update()
        {
            //throw new System.NotImplementedException();
        }
    }

}
