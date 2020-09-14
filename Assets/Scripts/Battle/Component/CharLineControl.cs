using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharLineControl
{
    public class LinePack
    {
        public CharData character;
        public float timeLine;
        public LinePack(CharData _character)
        {
            character = _character;
            ResetLine();
        }
        public void ResetLine()
        {
            timeLine = 999 - character.agility;
            if (!character.isEnemy) timeLine -= 0.5f;
        }
    }


    public List<CharData> charList;
    public List<LinePack> linePackList;

    public LinePack currentPack = null;


    public CharLineControl()
    {
        charList = new List<CharData>();
        linePackList = new List<LinePack>();
    }

    public void LoadAllCharacters()
    {
        AddCharList(FieldManager.instance.heros.front);
        AddCharList(FieldManager.instance.heros.middle);
        AddCharList(FieldManager.instance.heros.back);
        AddCharList(FieldManager.instance.enemies.front);
        AddCharList(FieldManager.instance.enemies.middle);
        AddCharList(FieldManager.instance.enemies.back);
        Debug.Log(linePackList.Count);
    }
    public CharData Next()
    {
        if (currentPack != null)
        {
            linePackList.Add(currentPack);
        }

        LinePack leastPack = null;
        float leastLine = 999;
        foreach(LinePack lp in linePackList)
        {
            if (lp.timeLine < leastLine)
            {
                leastLine = lp.timeLine;
                leastPack = lp;
            }
        }
        if (leastPack != null)
        {
            linePackList.Remove(leastPack);
            currentPack = leastPack;

            foreach (LinePack lp in linePackList)
                lp.timeLine -= leastPack.timeLine;
            leastPack.ResetLine();

            return leastPack.character;
        }

        Debug.LogError("CharLineControl must have something wrong!!");
        return null;
    }
    public string[] GetCharQueue()
    {


        return null;
    }



    void AddCharList(CharData charData)
    {
        if (charData == null) return;
        if (!charList.Contains(charData))
        {
            charList.Add(charData);
        }
        linePackList.Add(new LinePack(charData));
    }

    public void RemoveChar()
    {

    }

}
