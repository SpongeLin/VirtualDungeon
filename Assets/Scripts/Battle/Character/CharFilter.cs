using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharFilter 
{
    public string filterType;
    public int conditionInt1;
    public int conditionInt2;
    public bool conditionBool;

    public bool soulLink;

    public CharFilter(string type,int cond1)
    {
        filterType = type;
        conditionInt1 = cond1;
    }
    public CharFilter(string type,bool condBool)
    {
        filterType = type;
        conditionBool = condBool;
    }
    public CharFilter(string type)
    {
        filterType = type;
    }

    public bool CheckFilter(CharData chara)
    {
        bool result = false;
        switch (filterType)
        {
            case "Camps":
                if (conditionInt1 == 1)
                {
                    if (!chara.isEnemy)
                        result = true;
                }else if (conditionInt1 == 2)
                {
                    if (chara.isEnemy)
                        result = true;
                }
                break;
            case "Front":
                if (FieldManager.instance.CheckCharIsFrontest(chara))
                    result = true;
                break;
            case "NotCurrent":
                if (chara != FieldManager.instance.currentActionCharacter)
                    result = true;
                break;
        }
        return result;
    }
}
