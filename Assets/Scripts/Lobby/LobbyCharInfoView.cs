using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyCharInfoView : MonoBehaviour
{
    public CharacterDataPack charPack;

    public Image charHead;
    public Text charName;
    public Image healthBar;
    public Text health;

    public Text power;
    public Text magic;
    public Text energy;
    public Text armor;
    public Text agility;

    public Image skill1;
    public Text skillCD1;
    public Image skill2;
    public Text skillCD2;
    public Image skill3;

    public void SetCharInfo(CharacterDataPack cdp)
    {
        charPack = cdp;
        UpdateCharInfo();
        

    }
    public void UpdateCharInfo()
    {
        if (charPack == null) return;
        CharData chara = CharacterCreator.TestCreat(charPack);

        charName.text = chara.charShowName;

        health.text = charPack.currentHealth + "/" + charPack.maxHealth;
        healthBar.fillAmount = (float)charPack.currentHealth / charPack.maxHealth;

        power.text = charPack.power.ToString();
        magic.text = charPack.magic.ToString();
        energy.text = (charPack.maxEnergy+chara.maxEnergy).ToString();
        armor.text = charPack.armor.ToString();
        agility.text = (charPack.agility+chara.agility).ToString();

        if (charPack.skillCD1 == 0)
        {
            skillCD1.text = "";
        }
        else
        {
            skillCD1.text = charPack.skillCD1.ToString();
        }
        if (charPack.skillCD2 == 0)
        {
            skillCD2.text = "";
        }
        else
        {
            skillCD2.text = charPack.skillCD2.ToString();
        }

        skill1.sprite = Resources.Load<Sprite>("Skill/" + chara.skillControl.skill1.skillName);
        skill2.sprite = Resources.Load<Sprite>("Skill/" + chara.skillControl.skill2.skillName);
        skill3.sprite = Resources.Load<Sprite>("Skill/" + chara.skillControl.skill3.skillName);

    }

}
