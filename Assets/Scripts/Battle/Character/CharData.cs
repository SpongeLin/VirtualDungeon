using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharData
{
    public CharView charView;
    public CharStatusControl charStatusControl;
    public SkillControl skillControl; // 尚未確定

    public EnemyStrategy enemyStrategy = null;

    public CharData()
    {
        charStatusControl = new CharStatusControl(this);
        skillControl = new SkillControl(this);
    }
    

    public string charName;
    public string charShowName;

    public bool isEnemy { get { return enemyStrategy != null; } }
    public bool isDie;

    public int health;
    public int maxHealth;
    public int energy;
    public int maxEnergy;
    public int armor;

    public int magicPoint;
    public int maxMagicPoint;

    public int speedOrder;
    public int agility;
    public int guardCount;
    public int cardDamage;

    public Skill skill1;
    public Skill skill2;
    public Skill skill3;


    public void ReduceHealth(int damage)
    {
        if (damage <= 0) return;

        if (damage > armor)
        {
            armor = 0;
            damage -= armor;
            health -= damage;
        }
        else
        {
            armor -= damage;
        }
    }

    public void CharUpdate()
    {
        if (skill1 != null) skill1.Update();
        if (skill2 != null) skill2.Update();
        if (skill3 != null) skill3.Update();
    }

    public void TurnEnd()
    {
        //處理狀態減少!?

        if(skill1!=null )skill1.CoolDown();
        if (skill2 != null) skill2.CoolDown();
        if (skill3 != null) skill3.CoolDown();

        if (enemyStrategy != null)
            enemyStrategy.TurnEnd();
    }

}