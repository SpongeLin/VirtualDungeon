using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyCharInfoView : MonoBehaviour
{
    public CharacterDataPack charPack;

    public Image charHead;
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
        health.text = charPack.currentHealth + "/" + charPack.maxHealth;
        healthBar.fillAmount = (float)charPack.currentHealth / charPack.maxHealth;

        power.text = charPack.power.ToString();
        magic.text = charPack.magic.ToString();
        energy.text = charPack.maxEnergy.ToString();
        armor.text = charPack.armor.ToString();
        agility.text = charPack.agility.ToString();

        skillCD1.text = charPack.skillCD1.ToString();
        skillCD2.text = charPack.skillCD2.ToString();

    }

}
