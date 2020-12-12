using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IntroViewContent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    CharStatusView statusView;
    BattleSkillView skillView;
    [HideInInspector]
    public EnemyAction enemyAction;

    public void Awake()
    {
        statusView = null;
        skillView = null;
        CharStatusView csv = GetComponent<CharStatusView>();
        if (csv != null)
        {
            statusView = csv;
        }
        BattleSkillView bsv = GetComponent<BattleSkillView>();
        if (bsv != null)
        {
            skillView = bsv;
        }


    }
    public string GetName()
    {
        if (statusView != null)
        {
            if (statusView.charStatus != null)
                return statusView.charStatus.GetName();
        }
        if (skillView != null)
        {
            if (skillView.skill != null)
                return skillView.skill.GetName();
        }
        if (enemyAction != null)
        {
            return enemyAction.actionName;
        }
        return "";
    }
    public string GetDescription()
    {
        if (statusView != null)
        {
            if (statusView.charStatus != null)
                return statusView.charStatus.GetDescription();
        }
        if (skillView != null)
        {
            if (skillView.skill != null)
                return skillView.skill.GetDescription();
        }
        if (enemyAction != null)
        {
            return enemyAction.GetDescription();
        }
        return "";
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (skillView != null)
            FieldManager.instance.introViewControl.OpenIntroView(this, false);
        if (statusView != null)
        {
            if (statusView.charStatus != null)
            {
                if (statusView.charStatus.character.isEnemy)
                    FieldManager.instance.introViewControl.OpenIntroView(this, false);
                else
                    FieldManager.instance.introViewControl.OpenIntroView(this, true);
            }

        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        FieldManager.instance.introViewControl.CloseIntroView();
    }
}
