using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CharacterCreator
{

    public static void TestCreat(CharView view , CharacterDataPack cdp)
    {
        CharData data = new CharData();
        data.charName = cdp.heroName;
        //data.charShowName = charName;

        SetChar(data, cdp);//Set SKILL
        data.health = cdp.currentHealth == 0 ? data.maxHealth : cdp.currentHealth;

        data.charView = view;
        view.character = data;
    }

    static void SetChar(CharData charData, CharacterDataPack pack)
    {
        switch (pack.heroName)
        {
            case "A":
                charData.charShowName = "人偶A";
                charData.maxHealth = 64;
                charData.energy = 1;
                charData.maxEnergy = 3;
                charData.agility = 23;
                charData.skill1 = new testSkill.NormalAttack("x", 1, 1, 15).SetChar(charData);
                charData.skill2 = new testSkill.AttackSelf("x", 2, 2, 12).SetChar(charData);
                break;
            case "B":
                charData.charShowName = "人偶B";
                charData.maxHealth = 100;
                charData.energy = 2;
                charData.maxEnergy = 4;
                charData.agility = 12;
                charData.skill1 = new testSkill.NormalAttack("x", 1, 0, 15).SetChar(charData);
                charData.skill2 = new testSkill.JustTest("x", 2, 2).SetChar(charData);
                break;
            case "C":
                charData.charShowName = "人偶C";
                charData.maxHealth = 75;
                charData.energy = 2;
                charData.maxEnergy = 3;
                charData.agility = 35;
                charData.skill1 = new testSkill.NormalAttack("x", 1, 0, 15).SetChar(charData);
                charData.skill2 = new testSkill.AttackSelf("x", 2, 2, 12).SetChar(charData);
                charData.skill3 = new testSkill.JustTest("x", 2, 2).SetChar(charData);
                break;
            case "X":
                charData.charShowName = "敵人X";
                charData.maxHealth = 75;
                charData.energy = 2;
                charData.maxEnergy = 3;
                charData.agility = 20;
                charData.enemyStrategy = new EnemyStrategy();
                charData.enemyStrategy.AddAction(new nEnemyAction.DamageSelf(23));
                break;
        }




        //Debug.LogWarning("Can't create character !!!");
    }

}
