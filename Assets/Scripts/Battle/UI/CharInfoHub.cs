using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharInfoHub : MonoBehaviour
{
    Text info;

    private void Awake()
    {
        info = GetComponentInChildren<Text>();
    }
    public void SetInfo(CharData character)
    {
        string content = character.charShowName;
        content += "\n";
        content += "物理攻擊 : "+character.physicalDamage.ToString();
        content += "\n";
        content += "魔法攻擊 : " + character.magicDamage.ToString();
        content += "\n";
        content += "物理防禦 : " + character.physicalDefense.ToString();
        content += "\n";
        content += "魔法防禦 : " + character.magicDefense.ToString();

        info.text = content;
    }

    public void Clear()
    {

    }

}
