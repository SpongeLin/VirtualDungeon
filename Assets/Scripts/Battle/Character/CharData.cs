using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharData
{
    public CharView charView;
    public CharStatusControl charStatusControl;
    public SkillControl skillControl; // 尚未確定

    public CharData()
    {
        charStatusControl = new CharStatusControl(this);
        skillControl = new SkillControl(this);
    }

    public string charName;
    public string charShowName;

    public bool isEnemy;
    public bool isDie;

    public int health;
    public int maxHealth;
    public int actionPoint;
    public int maxActionPoint;

    public int magicPoint;
    public int maxMagicPoint;

    public int speedOrder;

    public Skill skill1;
    public Skill skill2;
    public Skill skill3;

    public int physicalDamage;
    public int magicDamage;
    public int physicalDefense;
    public int magicDefense;
    public int agility;


    public void TurnEnd()
    {
        //處理狀態減少!?

        if(skill1!=null )skill1.CoolDown();
        if (skill2 != null) skill2.CoolDown();
        if (skill3 != null) skill3.CoolDown();
    }

}