using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IntroViewContent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    CharStatusView statusView;
    BattleSkillView skillView;

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
                return statusView.charStatus.statusShowName;
        }
        if (skillView != null)
        {
            if (skillView.skill != null)
                return skillView.skill.skillShowName;
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
