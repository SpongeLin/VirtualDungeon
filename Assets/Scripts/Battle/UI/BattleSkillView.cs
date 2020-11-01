using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSkillView : MonoBehaviour
{
    public Skill skill;
    public Button button;

    public Text ap;
    public Text cd;

    public Image skillImage;//it's unavailability now
    public Image costMask;

    public bool canClick;
    public bool highLight;

    public bool sysCanClick = true; // can click by FieldManager 


    public void Gameupdate()
    {
        if (skill != null)
        {
            SetCanClick(skill.CanUseSkill() && sysCanClick && OrderManager.instance.IsEmptyStack() && FieldManager.instance.CheckCharTargetExist(skill));

            SetSkillNum();
        }
    }

    public void SetSkill(Skill _skill)
    {
        if (_skill == null)
        {
            skill = null;

            ap.text = "";
            cd.text = "";
            SetActiveSkill(false);

            return;
        }
        skill = _skill;
        SetSkillNum();
        SetActiveSkill(true);
    }

    void SetSkillNum()
    {
        if (skill.talent)
            ap.text = "";
        else
            ap.text = skill.actionPoint.ToString();

        if (skill.currentCoolDown == 0)
        {
            cd.text = "";
            costMask.fillAmount = 0;
        }
        else
        {
            cd.text = skill.currentCoolDown.ToString();
            float r = skill.currentCoolDown / (float)skill.coolDown;
            costMask.fillAmount = r;
        }
    }
    public void Click()
    {
        if (!canClick) return;
        FieldManager.instance.SkillSelecctTarget(skill);

    }
    public void SetCanClick(bool active)
    {
        if (active)
        {
            canClick = true;
            button.interactable = true;
        }
        else
        {
            canClick = false;
            button.interactable = false;
        }
    }
    public void SetActiveSkill(bool active)
    {
        button.interactable = active;
    }

}
