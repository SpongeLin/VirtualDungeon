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
    //-----------------------
    public class BeastDance : CardStatus
    {
        CardInfo cif;
        TurnInfo tif;
        bool active;
        int powerNum;
        public BeastDance(int _power)
        {
            eternal = true;
            original = true;
            powerNum = _power;
        }
        public override void Enter()
        {
            cif = SetSubscription<CardInfo>(TriggerType.CardMove, 1);
            tif = SetSubscription<TurnInfo>(TriggerType.TurnStarting, 2);
            SetSubscription<TurnInfo>(TriggerType.TurnEnding, 3);
        }
        public override void Trigger1()
        {
            if (cif.to == CardPos.Hand && cif.card==card)
            {
                if (!FieldManager.instance.currentActionCharacter.isEnemy)
                {
                    if (active == false)
                    {
                        active = true;
                        FieldManager.instance.currentActionCharacter.power += powerNum;
                    }
                }
            }
            if(cif.from == CardPos.Hand && cif.card == card)
            {
                if (!FieldManager.instance.currentActionCharacter.isEnemy)
                {
                    if (active == true)
                    {
                        active = false;
                        FieldManager.instance.currentActionCharacter.power -= powerNum;
                    }
                }
            }
        }
        public override void Trigger2()
        {
            if (!tif.isEnemyTurn)
            {
                if (CardManager.instance.GetCardPosition(card) == CardPos.Hand)
                {
                    if (active == false)
                    {
                        active = true;
                        FieldManager.instance.currentActionCharacter.power += powerNum;
                    }
                }
            }
        }
        public override void Trigger3()
        {
            if (!tif.isEnemyTurn)
            {
                if (CardManager.instance.GetCardPosition(card) == CardPos.Hand)
                {
                    if (active == true)
                    {
                        active = false;
                        FieldManager.instance.currentActionCharacter.power -= powerNum;
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
            cif = SetSubscription<CardInfo>(TriggerType.CardBurst, 1);
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