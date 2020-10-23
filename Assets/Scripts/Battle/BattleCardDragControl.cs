using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCardDragControl : MonoBehaviour
{

    public DragMode.DragMode currentDragMode;
    DragMode.HandMode hand;
    DragMode.FieldMode field;
    DragMode.ArrowMode arrow;

    // Start is called before the first frame update
    void Awake()
    {
        hand = new DragMode.HandMode();
        field = new DragMode.FieldMode();
        arrow = new DragMode.ArrowMode();

        currentDragMode = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentDragMode != null)
        {
            currentDragMode.Update();
        }
    }
    public void MouseDown(CardView card)
    {
        if (CardManager.instance.currentDragCard != null)
            return;
        CardManager.instance.currentDragCard = card;
        GoToHandMode();
    }
    public void MouseUp()
    {
        currentDragMode.MouseUp();
        GoToNullMode();
    }

    public void GoToNullMode()
    {
        if (currentDragMode == null)
            return;
        currentDragMode.Exit();
        currentDragMode = null;
        CardManager.instance.currentDragCard = null;

        CardManager.instance.cardViewControl.HandCardPosUpdate();
    }
    public void GoToHandMode()
    {
        if (currentDragMode != null)
        {
            if (currentDragMode.modeName == "HandMode")
                return;
            else
                currentDragMode.Exit();
        }
        currentDragMode = hand;
        currentDragMode.Enter();
        
    }
    public void GoToFieldMode()
    {
        if (currentDragMode != null)
        {
            if (currentDragMode.modeName == "FieldMode")
                return;
            else
                currentDragMode.Exit();
        }
        currentDragMode = field;
        currentDragMode.Enter();
    }
    public void GoToArrowMode()
    {
        if (currentDragMode != null)
        {
            if (currentDragMode.modeName == "ArrowMode")
                return;
            else
                currentDragMode.Exit();
        }
        currentDragMode = arrow;
        currentDragMode.Enter();
    }
}

namespace DragMode
{
    public abstract class DragMode
    {
        public string modeName;
        public abstract void Enter();
        public abstract void Update();
        public abstract void Exit();
        public abstract void MouseUp();
    }
    public class HandMode : DragMode
    {
        public HandMode()
        {
            modeName = "HandMode";
        }
        public override void Enter()
        {
            FieldManager.instance.CancelTarget();
        }

        public override void Exit()
        {
            
        }

        public override void MouseUp()
        {
            
        }

        public override void Update()
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            CardManager.instance.currentDragCard.transform.position = new Vector3(pos.x,pos.y,CardManager.instance.cardViewControl.handCardPlace.transform.position.z);

            if(CardManager.instance.currentDragCard.transform.position.y > CardManager.instance.cardViewControl.useLine.transform.position.y)
            {
                if (CardManager.instance.currentDragCard.card.cardTarget == CardTarget.CardSelf)
                    CardManager.instance.battleCardDragControl.GoToFieldMode();
                else
                    CardManager.instance.battleCardDragControl.GoToArrowMode();
            }
        }
    }
    public class ArrowMode : DragMode
    {
        public ArrowMode()
        {
            modeName = "ArrowMode";
        }
        public override void Enter()
        {
            FieldManager.instance.SetArrow(true);
            FieldManager.instance.SetTarget(CardManager.instance.currentDragCard);
        }

        public override void Exit()
        {
            FieldManager.instance.SetArrow(false);
            FieldManager.instance.CancelTarget();
        }

        public override void MouseUp()
        {
            if (FieldManager.instance.currentMouseOverCharacter != null)
            {
                CardManager.instance.currentDragCard.card.targetChar = FieldManager.instance.currentMouseOverCharacter;
                CardManager.instance.UseCard(CardManager.instance.currentDragCard.card);
            }
        }

        public override void Update()
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (pos.y < CardManager.instance.cardViewControl.useLine.transform.position.y)
            {
                CardManager.instance.battleCardDragControl.GoToHandMode();
            }
        }
    }
    public class FieldMode : DragMode
    {
        public FieldMode()
        {
            modeName = "FieldMode";
        }
        public override void Enter()
        {
            //CardManager.instance.currentDragCard.transform.localScale = new Vector3(1.2f, 1.2f, 1);
            CardManager.instance.currentDragCard.SetSize(new Vector3(1.35f, 1.35f, 1),true);
            CardManager.instance.currentDragCard.SetSize(new Vector3(1.35f, 1.35f, 1),true);
        }

        public override void Exit()
        {
            //CardManager.instance.currentDragCard.transform.localScale = new Vector3(1, 1, 1);
            CardManager.instance.currentDragCard.SetSize(new Vector3(1, 1, 1),false);

        }

        public override void MouseUp()
        {
            CardManager.instance.UseCard(CardManager.instance.currentDragCard.card);
        }

        public override void Update()
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            CardManager.instance.currentDragCard.transform.position = new Vector3(pos.x, pos.y, CardManager.instance.cardViewControl.handCardPlace.transform.position.z);

            if (CardManager.instance.currentDragCard.transform.position.y < CardManager.instance.cardViewControl.useLine.transform.position.y)
            {
                CardManager.instance.battleCardDragControl.GoToHandMode();
            }
        }
    }

}