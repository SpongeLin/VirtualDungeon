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
            timeLine =  character.agility;
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
        foreach (CharData chara in FieldManager.instance.allChar)
            AddCharList(chara);

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

            UpdateCharLineView(true);
            return leastPack.character;
        }


        Debug.LogError("CharLineControl must have something wrong!!");
        return null;
    }
    public string[] GetCharQueue()
    {


        return null;
    }
    void UpdateCharLineView(bool anima = false)
    {
        //slot
        FieldManager.instance.UpdateCharLineView(anima);
    }



    void AddCharList(CharData charData)
    {
        if (charData == null) return;
        if (!charList.Contains(charData))
        {
            charList.Add(charData);
            linePackList.Add(new LinePack(charData));
        }

        UpdateCharLineView();
    }
    public void RemoveChar(CharData charData)
    {
        charList.Remove(charData);

        LinePack willRemovePack = null;
        foreach (LinePack lp in linePackList)
            if (lp.character == charData)
                willRemovePack = lp;
        if (willRemovePack != null)
        {
            linePackList.Remove(willRemovePack);
            //Debug.Log("移除" + charData.charShowName);
        }
        else if (currentPack.character == charData)
            currentPack = null;


        UpdateCharLineView();
    }




}
