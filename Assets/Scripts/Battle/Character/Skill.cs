using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SkillControl // need to inheritance subscriber??
{
    CharData character;
    public SkillControl(CharData _chara)
    {
        character = _chara;
    }
}

public abstract class Skill : Subscriber
{
    public string skillName;
    public string skillShowName;

    public bool talent;

    public int actionPoint;
    public int magicPoint;

    public int coolDown;
    public int currentCoolDown;

    public int banUseCount = 0;

    public CharData character;

    public SkillTarget target = SkillTarget.All;


    public Skill(string name,int ap,int cd)
    {
        skillName = name;
        actionPoint = ap;
        coolDown = cd;

        isWorking = true;
    }
    public abstract void Enter();
    public bool CanUseSkill()
    {
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

    public Skill SetChar(CharData charData) // TEST!!
    {
        character = charData;
        return this;
    }

    public abstract void Excite(CharData target);
    
}


namespace testSkill
{
    public class NormalAttack : Skill
    {
        int damageValue;
        public NormalAttack(string name, int ap, int cd, int _damageValue) : base(name, ap, cd)
        {
            skillShowName = "普通打擊";
            damageValue = _damageValue;
            target = SkillTarget.Enemies;
        }

        public override void Excite(CharData target)
        {
            FieldManager.instance.DamageChar(target, damageValue, DamageType.Normal, character);
        }

        public override void Update()
        {
            
        }
        public override void Enter()
        {

        }
    }
    public class AttackSelf : Skill
    {
        int damageValue;
        public AttackSelf(string name, int ap, int cd, int _damageValue) : base(name, ap, cd)
        {
            skillShowName = "衝撞";
            damageValue = _damageValue;

            target = SkillTarget.FrontEnemy;
        }
        public override void Excite(CharData target)
        {
            FieldManager.instance.DamageChar(target, damageValue, DamageType.Normal, character);
        }
        public override void Update()
        {

        }
        public override void Enter()
        {

        }
    }
    public class JustTest : Skill
    {
        public JustTest(string name, int ap, int cd) : base(name, ap, cd)
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
    }

}

