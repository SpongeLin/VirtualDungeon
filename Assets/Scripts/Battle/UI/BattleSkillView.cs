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

    public void SetSkill(Skill _skill)
    {
        if (_skill == null)
        {
            skill = null;

            skillName.text = "";
            ap.text = "";
            cd.text = "";
            SetClick(false);

            return;
        }
        skill = _skill;

        skillName.text = skill.skillShowName;
        ap.text = skill.actionPoint.ToString();
        cd.text = skill.coolDown.ToString();

        SetClick(true);
    }
    public void Click()
    {

    }
    public void SetClick(bool active)
    {
        canClick = active;
        button.interactable = active;
    }

}
