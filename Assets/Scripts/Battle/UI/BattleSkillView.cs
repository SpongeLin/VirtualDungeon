using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSkillView : MonoBehaviour
{
    public Skill skill;
    public Button button;

    public Text skillName;

    public Text ap;
    public Text cd;

    public Image skillImage;//it's unavailability now

    public bool canClick;
    public bool highLight;

    public bool sysCanClick = true; // can click by FieldManager 


    public void Gameupdate()
    {
        if (skill != null)
        {
            SetCanClick(skill.CanUseSkill() && sysCanClick && OrderManager.instance.IsEmptyStack() && FieldManager.instance.CheckCharTargetExist(skill));
        }
    }

    public void SetSkill(Skill _skill)
    {
        if (_skill == null)
        {
            skill = null;

            skillName.text = "";
            ap.text = "";
            cd.text = "";
            SetActiveSkill(false);

            return;
        }
        skill = _skill;

        skillName.text = skill.skillShowName;
        ap.text = skill.actionPoint.ToString();
        cd.text = skill.coolDown.ToString();

        SetActiveSkill(true);
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
            skillImage.color = Color.white;
            canClick = true;
        }
        else
        {
            skillImage.color = Color.gray;
            canClick = false;
        }
    }
    public void SetActiveSkill(bool active)
    {
        canClick = active;
        button.interactable = active;
    }

}
