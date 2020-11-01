using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace nCardStatus
{
    public class OriExhasut : CardStatus
    {
        public OriExhasut()
        {
            eternal = true;
            original = true;
        }
        public override void Enter()
        {
            card.exhasutCount++;
        }

        public override void Exit()
        {
            card.exhasutCount--;
        }

        public override void Update()
        {
        }
    }
    public class Overload : CardStatus
    {
        public int energyNum;
        UseCardInfo cif;
        public Overload(int _energyNum)
        {
            energyNum = _energyNum;
            eternal = true;
            original = true;
        }
        public override void Enter()
        {
            cif = SetSubscription<UseCardInfo>(TriggerType.UseCardCheck, 1);
            FieldManager.instance.overloadNum += energyNum;
        }
        public override void Trigger1()
        {
            if (cif.card == card)
            {
                FieldManager.instance.GiveFieldStatus("AllyDownEnergyAtTurnStart",energyNum);
            }
        }

        public override void Exit()
        {
            FieldManager.instance.overloadNum -= energyNum;
        }

        public override void Update()
        {
            //throw new System.NotImplementedException();
        }
    }

    public class SoulLink : CardStatus
    {
        CardInfo cif;
        public SoulLink()
        {
            eternal = true;
            original = true;
        }
        public override void Enter()
        {
            cif = SetSubscription<CardInfo>(TriggerType.CardMove, 1);
        }
        public override void Trigger1()
        {
            if (cif.card == card)
            {
                if (cif.to == CardPos.Hand)
                {
                    if (card.linkChar == null)
                    {
                        if (FieldManager.instance.currentActionCharacter != null)
                            if (!FieldManager.instance.currentActionCharacter.isEnemy)
                                card.linkChar = FieldManager.instance.currentActionCharacter;
                    }
                }
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
    public class CostDownAtBurst : CardStatus
    {
        public int costDown;
        CardInfo cif;
        public CostDownAtBurst(int _costDown)
        {
            costDown = _costDown;
            eternal = true;
            original = true;
        }
        public override void Enter()
        {
            cif = SetSubscription<CardInfo>(TriggerType.UseCardCheck, 1);
        }
        public override void Trigger1()
        {
            if (cif.card == card)
            {
                if (cif.burstNum > 0)
                {
                    cif.card.CostAdjust(-costDown);
                }
            }
        }

        public override void Exit()
        {
        }

        public override void Update()
        {
            //throw new System.NotImplementedException();
        }
    }

}