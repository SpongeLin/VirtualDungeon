using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CharacterCreator
{

    public static CharData TestCreat( CharacterDataPack cdp, CharView view=null)
    {
        CharData data = new CharData();
        data.charName = cdp.heroName;
        data.maxHealth = cdp.maxHealth;
        //data.charShowName = charName;

        SetChar(data, cdp);//Set SKILL
        data.health = cdp.currentHealth == 0 ? data.maxHealth : cdp.currentHealth;
        data.skillControl.SetCurrentCoolDown(cdp.skillCD1, cdp.skillCD2);

        if (view != null)
        {
            data.charView = view;
            view.character = data;
        }
        return data;
    }

    static void SetChar(CharData charData, CharacterDataPack pack)
    {
        charData.characterDataPack = pack;
        switch (pack.heroName)
        {
            case "Iro":
                charData.charShowName = "彩";
                charData.energy = 1;
                charData.maxEnergy = 3;
                charData.agility = 23;
                charData.skillControl.EnterSkill(0,new nSkill.MagicLight( 0, 0));
                charData.skillControl.EnterSkill(1, new nSkill.StarLose(0, 5));
                charData.skillControl.EnterSkill(2, new nSkill.StarGuide(0, 3));
                break;
            case "Nao":
                charData.charShowName = "奈央";
                charData.energy = 2;
                charData.maxEnergy = 4;
                charData.agility = 15;
                charData.skillControl.EnterSkill(0, new nSkill.InsidePower(0, 0));
                charData.skillControl.EnterSkill(1, new nSkill.Bone(2, 1));
                charData.skillControl.EnterSkill(2, new nSkill.Clap(0, 3));
                //charData.skillControl.skill1 = new testSkill.NormalAttack( 1, 0, 15).SetChar(charData);
                //charData.skillControl.skill2 = new testSkill.JustTest( 2, 2).SetChar(charData);
                break;
            case "ShouMe":
                charData.charShowName = "翔咩";
                charData.energy = 2;
                charData.maxEnergy = 3;
                charData.agility = 28;
                charData.skillControl.EnterSkill(0, new nSkill.GoatMilk(0, 0));
                charData.skillControl.EnterSkill(1, new nSkill.DemonArmor(1, 1));
                charData.skillControl.EnterSkill(2, new nSkill.Same(0, 5));
                break;
            case "X":
                charData.charShowName = "敵人X";
                charData.agility = 20;
                charData.enemyStrategy = new EnemyStrategy();
                charData.enemyStrategy.AddAction(new nEnemyAction.DamageSelf(23));
                break;
            case "NovitiateKnight":
                charData.charShowName = "見習騎士";
                charData.agility = 21;
                charData.enemyStrategy = new EnemyStrategy();
                charData.enemyStrategy.AddAction(new nEnemyAction.DamageRandom(15));
                charData.enemyStrategy.AddAction(new nEnemyAction.DamageRandom(15));
                charData.enemyStrategy.AddAction(new nEnemyAction.ArmorSelf(25));
                charData.enemyStrategy.AddAction(new nEnemyAction.DamageGiveFragile(25,1));
                break;
            case "NovitiateMage":
                charData.charShowName = "見習法師";
                charData.agility = 26;
                charData.enemyStrategy = new EnemyStrategy();
                charData.enemyStrategy.AddAction(new nEnemyAction.DamageGiveWeak(7,2));
                charData.enemyStrategy.AddAction(new nEnemyAction.DamageRandom(17));
                charData.enemyStrategy.AddAction(new nEnemyAction.HealRandomAlly(15));
                charData.enemyStrategy.AddAction(new nEnemyAction.DamageWithFragment(15, 1));
                break;
            case "NovitiateLancer":
                charData.charShowName = "見習槍兵";
                charData.agility = 21;
                charData.enemyStrategy = new EnemyStrategy();
                charData.enemyStrategy.AddAction(new nEnemyAction.DamageAllHero(12));
                charData.enemyStrategy.AddAction(new nEnemyAction.DamageRandomTimes(7,3));
                charData.enemyStrategy.AddAction(new nEnemyAction.GainPower(3));
                break;
        }




        //Debug.LogWarning("Can't create character !!!");
    }

}
