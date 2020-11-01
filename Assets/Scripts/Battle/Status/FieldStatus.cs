using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace nFieldStatus
{
    public class AllyDownEnergyAtTurnStart : FieldStatus
    {
        TurnInfo tif;
        public int energyNum;
        public AllyDownEnergyAtTurnStart(int _energyNum)
        {
            energyNum = _energyNum;
            eternal = true;
        }
        public override void Enter()
        {
            tif = SetSubscription<TurnInfo>(TriggerType.TurnStartBefore, 1);
        }
        public override void Trigger1()
        {
            if (!FieldManager.instance.currentActionCharacter.isEnemy)
            {
                OrderManager.instance.AddOrder(new sysOrder.ReduceEnergy(energyNum));
                isWorking = false;
            }
        }

        public override void Exit()
        {
            //throw new System.NotImplementedException();
        }

        public override void Update()
        {
            //throw new System.NotImplementedException();
        }
    }
}