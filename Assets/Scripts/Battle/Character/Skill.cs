using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SkillControl : Subscriber
{
    CharData character;

    public Skill skill1;
    public Skill skill2;
    public Skill skill3;

    public SkillControl(CharData _chara)
    {
        character = _chara;
        isWorking = true;
        TriggerManager.instance.AddUpdateList(this);
    }
    public void EnterSkill(int skillSlot,Skill skill,int _currentCoolDown=0)
    {
        skill.character = character;
        skill.currentCoolDown = _currentCoolDown;
        if (skillSlot == 0)
        {
            skill3 = skill;
            skill3.Enter();
        }else if (skillSlot == 1)
        {
            skill1 = skill;
            skill1.Enter();
        }else if (skillSlot == 2)
        {
            skill2 = skill;
            skill2.Enter();
        }
    }

    public override void Update()
    {
        if (skill1 != null) skill1.Update();
        if (skill2 != null) skill2.Update();
        if (skill3 != null) skill3.Update();
    }
    public void TurnEnd()
    {
        if (skill1 != null) skill1.CoolDown();
        if (skill2 != null) skill2.CoolDown();
        if (skill3 != null) skill3.CoolDown();
    }
}

public abstract class Skill : Subscriber
{
    public string skillName;
    public string skillShowName;
    public string skillDescription;

    public bool talent;

    public int actionPoint;
    public int magicPoint;

    public int coolDown;
    public int currentCoolDown;

    public int banUseCount = 0;

    public CharData character;

    public SkillTarget target = SkillTarget.All;

    public string GetName()
    {
        string showName = skillShowName;
        if(!talent)
            showName += "" + actionPoint + "AP " + coolDown + "CD";

        return showName;
    }
    public string GetDescription()
    {
        return skillDescription;
    }

    public Skill(int ap,int cd)
    {
        actionPoint = ap;
        coolDown = cd;

        isWorking = true;
    }
    public abstract void Enter();
    public abstract void Exit();
    public bool CanUseSkill()
    {
        if (talent)
            return false;
        if (character.energy < actionPoint)
            return false;
        if (currentCoolDown > 0)
            return false;
        if (banUseCount > 0)
            return false;

        return true;
    }
    public void CoolDown()
    {
        if (currentCoolDown > 0)
            currentCoolDown--;
    }

    public abstract void Excite(CharData target);
    
}
namespace nSkill
{
    public class MagicLight : Skill
    {
        int damageValue;
        UseCardInfo ucif;
        int num = 0;
        public MagicLight( int ap, int cd) : base( ap, cd)
        {
            skillShowName = "星輝";
            talent = true;
            skillName = "MagicLight";
            skillDescription = "你每打出3張牌，獲得1點魔莉。";
        }

        public override void Excite(CharData target)
        {
        }

        public override void Update()
        {

        }
        public override void Trigger1()
        {
            if (FieldManager.instance.currentActionCharacter == character)
            {
                num++;
                if (num >= 3)
                {
                    num = 0;
                    OrderManager.instance.AddOrder(new sysOrder.GainMagic(character, 1));
                }
            }
        }
        public override void Enter()
        {
            ucif = SetSubscription<UseCardInfo>(TriggerType.UseCardAfter, 1);
        }

        public override void Exit()
        {
            //throw new System.NotImplementedException();
        }
    }
    public class StarLose : Skill
    {
        public StarLose(int ap, int cd) : base( ap, cd)
        {
            skillShowName = "星塵";
            target = SkillTarget.Self;
            skillName = "StarLose";
            skillDescription = "丟棄手中所有牌";
        }

        public override void Excite(CharData target)
        {
            CardData[] cards = CardManager.instance.handCards.ToArray();
            foreach (CardData card in cards)
            {
                OrderManager.instance.AddOrder(new sysOrder.DiscardOrder(card));
            }
        }

        public override void Update()
        {

        }
        public override void Enter()
        {
        }

        public override void Exit()
        {
            //throw new System.NotImplementedException();
        }
    }
    public class StarGuide : Skill
    {
        public StarGuide( int ap, int cd) : base( ap, cd)
        {
            skillShowName = "星光指引";
            target = SkillTarget.Self;
            skillName = "StarGuide";
            skillDescription = "一張隨機手牌花費減少(1)。魔導(3)：所有手牌花費減少(1)";
        }

        public override void Excite(CharData target)
        {
            if (CardManager.instance.handCards.Count == 0) return;
            if (FieldManager.instance.currentActionCharacter.magicPoint < 3)
            {
                CardData[] cards = CardManager.instance.handCards.ToArray();
                CardData card = cards[Random.Range(0, cards.Length)];
                OrderManager.instance.AddOrder(new sysOrder.CardCostAdjust(card, -1));
            }
            else
            {
                FieldManager.instance.CharLoseMagic(FieldManager.instance.currentActionCharacter, 3);
                CardData[] cards = CardManager.instance.handCards.ToArray();
                foreach(CardData card in cards)
                    OrderManager.instance.AddOrder(new sysOrder.CardCostAdjust(card, -1));
            }
        }

        public override void Update()
        {

        }
        public override void Enter()
        {
        }

        public override void Exit()
        {
            //throw new System.NotImplementedException();
        }
    }
    //---------------------
    public class InsidePower : Skill
    {
        CardInfo cif;
        public InsidePower(int ap, int cd) : base(ap, cd)
        {
            skillShowName = "內功";
            talent = true;
            skillName = "InsidePower";
            skillDescription = "當你抽出強化牌時，為其強化2次。";
        }

        public override void Excite(CharData target)
        { }

        public override void Update()
        {

        }
        public override void Enter()
        {
            cif = SetSubscription<CardInfo>(TriggerType.Draw, 1);
        }
        public override void Trigger1()
        {
            if(FieldManager.instance.currentActionCharacter == character)
            {
                if (cif.card.canBurst)
                {
                    OrderManager.instance.AddOrder(new sysOrder.CardBurst(cif.card));
                    OrderManager.instance.AddOrder(new sysOrder.CardBurst(cif.card));
                }
            }
        }

        public override void Exit()
        {
            //throw new System.NotImplementedException();
        }
    }
    public class Bone : Skill
    {
        public Bone(int ap, int cd) : base(ap, cd)
        {
            skillShowName = "筋骨神功";
            target = SkillTarget.Self;
            skillName = "Bone";
            skillDescription = "本賽局獲得5點力量";
        }

        public override void Excite(CharData target)
        {
            OrderManager.instance.AddOrder(new sysOrder.GainPower(character, 5));
        }

        public override void Update()
        {

        }
        public override void Enter()
        {
        }

        public override void Exit()
        {
            //throw new System.NotImplementedException();
        }
    }
    public class Clap : Skill
    {
        public Clap(int ap, int cd) : base(ap, cd)
        {
            skillShowName = "拍掌";
            target = SkillTarget.Self;
            skillName = "Clap";

            skillDescription = "消耗你手中所有負面卡牌";
        }

        public override void Excite(CharData target)
        {
            CardData[] cards = CardManager.instance.handCards.ToArray();
            foreach (CardData card in cards)
            {
                if (card.negativeCard)
                {
                    OrderManager.instance.AddOrder(new sysOrder.MoveCard(card, CardPos.Banish));
                }
            }
        }

        public override void Update()
        {

        }
        public override void Enter()
        {
        }

        public override void Exit()
        {
            //throw new System.NotImplementedException();
        }
    }
    //---------------------------
    public class GoatMilk : Skill
    {
        CharInfo cif;
        public GoatMilk(int ap, int cd) : base(ap, cd)
        {
            skillShowName = "羊奶";
            talent = true;
            skillName = "GoatMilk";

            skillDescription = "當你賦予其他友方角色護盾時，為其治療5點生命值";
        }

        public override void Excite(CharData target)
        { }

        public override void Update()
        {

        }
        public override void Enter()
        {
            cif = SetSubscription<CharInfo>(TriggerType.Armor, 1);
        }
        public override void Trigger1()
        {
            if (cif.argumentChar == character)
            {
                if (cif.character != character)
                {
                    OrderManager.instance.AddOrder(new sysOrder.HealOrder(cif.character, 5,character));
                }
            }
        }

        public override void Exit()
        {
            //throw new System.NotImplementedException();
        }
    }
    public class DemonArmor : Skill
    {
        public DemonArmor(int ap, int cd) : base(ap, cd)
        {
            skillShowName = "魔族加護";
            target = SkillTarget.Allies;
            skillName = "DemonArmor";
            skillDescription = "賦予一個友方角色10點護盾。魔導(1)：賦予的護盾量加倍";
        }

        public override void Excite(CharData target)
        {
            if (FieldManager.instance.currentActionCharacter.magicPoint < 1)
            {
                OrderManager.instance.AddOrder(new sysOrder.ArmorChar(target, 10, character));
            }
            else
            {
                FieldManager.instance.CharLoseMagic(FieldManager.instance.currentActionCharacter, 1);
                OrderManager.instance.AddOrder(new sysOrder.ArmorChar(target, 20, character));
            }
        }

        public override void Update()
        {

        }
        public override void Enter()
        {
        }

        public override void Exit()
        {
            //throw new System.NotImplementedException();
        }
    }
    public class Same : Skill
    {
        public Same(int ap, int cd) : base(ap, cd)
        {
            skillShowName = "鯊魚的祝福";
            target = SkillTarget.Allies;
            skillName = "Same";
            skillDescription = "賦予一個友方角色40點護盾以及守護直到他回合結束";
        }

        public override void Excite(CharData target)
        {
            OrderManager.instance.AddOrder(new sysOrder.ArmorChar(target, 40, character));
            OrderManager.instance.AddOrder(new sysOrder.GiveCharStatus(target, "Gaurd", 1));
        }

        public override void Update()
        {

        }
        public override void Enter()
        {
        }

        public override void Exit()
        {
            //throw new System.NotImplementedException();
        }
    }

}


namespace testSkill
{
    public class NormalAttack : Skill
    {
        int damageValue;
        public NormalAttack( int ap, int cd, int _damageValue) : base( ap, cd)
        {
            skillShowName = "普通打擊";
            damageValue = _damageValue;
            target = SkillTarget.Enemies;
        }

        public override void Excite(CharData target)
        {
            //FieldManager.instance.DamageChar(target, damageValue, DamageType.Normal, character);
            OrderManager.instance.AddOrder(new sysOrder.DamagePrepare(target, damageValue, DamageType.Normal, character));
        }

        public override void Update()
        {
            
        }
        public override void Enter()
        {

        }
        public override void Exit()
        {
            //throw new System.NotImplementedException();
        }
    }
    public class AttackSelf : Skill
    {
        int damageValue;
        public AttackSelf( int ap, int cd, int _damageValue) : base( ap, cd)
        {
            skillShowName = "衝撞";
            damageValue = _damageValue;

            target = SkillTarget.FrontEnemy;
        }
        public override void Excite(CharData target)
        {
            //FieldManager.instance.DamageChar(target, damageValue, DamageType.Normal, character);
            OrderManager.instance.AddOrder(new sysOrder.DamagePrepare(target, damageValue, DamageType.Normal, character));
        }
        public override void Update()
        {

        }
        public override void Enter()
        {

        }
        public override void Exit()
        {
            //throw new System.NotImplementedException();
        }
    }
    public class JustTest : Skill
    {
        public JustTest(int ap, int cd) : base( ap, cd)
        {
            skillShowName = "測試";

            target = SkillTarget.Self;
        }
        public override void Excite(CharData target)
        {
            //FieldManager.instance.DamageChar(target, damageValue, DamageType.Physical, character);
            Debug.Log("Test object : " + target.charShowName);
        }
        public override void Update()
        {

        }
        public override void Enter()
        {

        }
        public override void Exit()
        {
            //throw new System.NotImplementedException();
        }
    }

}

