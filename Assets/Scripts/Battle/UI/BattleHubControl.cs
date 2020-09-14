using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleHubControl : MonoBehaviour
{
    public CharInfoHub heroInfo;
    public CharInfoHub enemyInfo;

    public BattleSkillView skillView1;
    public BattleSkillView skillView2;
    public BattleSkillView skillView3;
    public BattleSkillView superSkillView;
    public BattleSkillView extraSkillView;


    public void SetCharSkill(CharData character)
    {
        skillView1.SetSkill(character.skill1);
        skillView2.SetSkill(character.skill2);
        skillView3.SetSkill(character.skill3);
        superSkillView.SetSkill(character.superSkill);
        extraSkillView.SetSkill(character.extraSkill);
    }


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

    public void TurnEndButton()
    {
        FieldManager.instance.CharTurnEnd();
    }

}
