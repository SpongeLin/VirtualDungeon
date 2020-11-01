using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharData
{
    public CharView charView;
    public CharStatusControl charStatusControl;
    public SkillControl skillControl;

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

    public int speedOrder;
    public int agility;
    public int guardCount;
    public int cardDamage;



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
    public void ReduceEnergy(int num)
    {
        energy -= num;
        if (energy < 0)
            energy = 0;
    }

    public void TurnEnd()
    {
        //處理狀態減少!?
        skillControl.TurnEnd();

        if (enemyStrategy != null)
            enemyStrategy.TurnEnd();
    }

}