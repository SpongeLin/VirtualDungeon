using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharLineViewControl : MonoBehaviour
{
    public class CharLinePack:IComparable<CharLinePack>
    {
        public CharLinePack(CharLineControl.LinePack line)
        {
            chara = line.character;
            oriAgility = chara.agility;
            currentLine = line.timeLine;
        }
        int oriAgility;
        float currentLine;
        public CharData chara;
        public void Update()
        {
            currentLine += oriAgility;
        }

        public int CompareTo(CharLinePack other)
        {
            if (currentLine > other.currentLine)
                return 1;
            if (currentLine < other.currentLine)
                return -1;
            return 0;
        }
    }
    public List<CharLinePack> charLinePackList;


    public List<CharLineView> charLineViewList;
    public float viewDistance;
    public int viewNumber;
    // Start is called before the first frame update
    public void Awake()
    {
        charLinePackList = new List<CharLinePack>();
    }

    public void UpdateCharLineView()
    {
        string xxg= "";
        foreach (CharLineControl.LinePack lp in FieldManager.instance.charLineControl.linePackList)
        {
            xxg += lp.timeLine + " ";
        }
        //Debug.Log(xxg);

        List<CharLinePack> slotResult = new List<CharLinePack>();
        charLinePackList.Clear();
        foreach(CharLineControl.LinePack lp in FieldManager.instance.charLineControl.linePackList)
        {
            charLinePackList.Add(new CharLinePack(lp));
        }
        if (FieldManager.instance.charLineControl.currentPack != null)
            charLinePackList.Add(new CharLinePack(FieldManager.instance.charLineControl.currentPack));
        for(int i = 0; i < viewNumber; i++)
        {
            charLinePackList.Sort();
            slotResult.Add(charLinePackList[0]);
            charLinePackList[0].Update();
            //Debug.Log("GG"+charLinePackList[0].chara.charShowName);
        }

        for(int i=0;i<viewNumber;i++)
        {
            charLineViewList[i].charName.text = slotResult[i].chara.charShowName;
            charLineViewList[i].charImage.sprite = Resources.Load<Sprite>("Character/" + slotResult[i].chara.charName);
        }

        string log = "";
        foreach (CharLinePack clp in slotResult)
            log += clp.chara.charShowName + "  ";
        //Debug.Log(log);
    }
    public void AddCharLineView(CharData chara)
    {

    }
    public void RemoveCharLineView(CharData chara)
    {

    }
}
