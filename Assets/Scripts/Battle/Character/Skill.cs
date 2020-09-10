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

    public SkillTarget target = SkillTarget.All;

    public Skill(string name,int ap,int cd)
    {
        skillName = name;
        actionPoint = ap;
        coolDown = cd;

    }

    public abstract void Excite();
    
}

public class NormalAttack : Skill
{
    public NormalAttack(string name, int ap, int cd):base(name,ap,cd)
    {

    }
    public override void Excite()
    {
        throw new System.NotImplementedException();
    }
}