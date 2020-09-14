using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Skill
{
    public string skillName;
    public string skillShowName;

    public int actionPoint;
    public int magicPoint;

    public int coolDown;
    public int currentCoolDown;

    public CharData character;

    public SkillTarget target = SkillTarget.All;

    public Skill(string name,int ap,int cd)
    {
        skillName = name;
        actionPoint = ap;
        coolDown = cd;

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
        }
        public override void Excite(CharData target)
        {
            FieldManager.instance.DamageChar(target, damageValue, DamageType.Physical, character);
        }
    }
    public class AttackSelf : Skill
    {
        int damageValue;
        public AttackSelf(string name, int ap, int cd, int _damageValue) : base(name, ap, cd)
        {
            skillShowName = "自殘";
            damageValue = _damageValue;

            target = SkillTarget.Self;
        }
        public override void Excite(CharData target)
        {
            FieldManager.instance.DamageChar(target, damageValue, DamageType.Physical, character);
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
    }

}

