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
        }
        public override void Enter()
        {
            card.exhasutCount++;
        }

        public override void Exit()
        {
            card.exhasutCount--;
        }

        public override string GetDescription()
        {
            return "";
        }

        public override void Update()
        {
        }
    }
}