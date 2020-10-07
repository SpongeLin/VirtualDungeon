using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHubControl : MonoBehaviour
{
    public Text cardInfo;

    public BattleSkillView skillView1;
    public BattleSkillView skillView2;
    public BattleSkillView skillView3;

    public void Gameupdate()
    {
        skillView1.Gameupdate();
        skillView2.Gameupdate();
        skillView3.Gameupdate();

        cardInfo.text = "牌組：" + CardManager.instance.deck.Count + "\n棄牌：" + CardManager.instance.cemetery.Count;
    }

    public void SetCharSkill(CharData character)
    {
        skillView1.SetSkill(character.skill1);
        skillView2.SetSkill(character.skill2);
        skillView3.SetSkill(character.skill3);
    }

    /*
    public void ShowCharInfo(CharData character)
    {
        if (!character.isEnemy)
        {
            heroInfo.SetInfo(character);
        }
        else
        {
            enemyInfo.SetInfo(character);
        }
    }
    */

    public void TurnEndButton()
    {
        FieldManager.instance.CharTurnEnd();
    }

}
