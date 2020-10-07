using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharViewUIControl : MonoBehaviour
{
    public List<CharViewUI> viewList;
    public CharViewUI temp;

    public Transform ViewUIPos;

    private void Awake()
    {
        for(int i = 0; i < 10; i++)
        {
            GameObject go = Instantiate(temp.gameObject, ViewUIPos);
            viewList.Add(go.GetComponent<CharViewUI>());
        }
    }
    public void ShowCurrentTurnMark(CharData character,bool active)
    {
        GetView(character).SetCurrentTurnMark(active);
    }




    public void OpenCharViewUI(CharView charView)
    {
        CharViewUI cvu = GetView();
        cvu.character = charView.character;
        cvu.transform.position = charView.transform.position;
    }
    public void CloseCharViewUI(CharView charView)
    {
        CharViewUI cvu = GetView(charView.character);
        cvu.character = null;
        cvu.transform.localPosition = Vector3.zero;

        cvu.isWorking = false;
    }

    CharViewUI GetView()
    {
        foreach(CharViewUI cvu in viewList)
        {
            if (!cvu.isWorking)
            {
                cvu.isWorking = true;
                return cvu;
            }
        }
        Debug.LogError("Dont Found CharViewUI");
        return null;
    }
    CharViewUI GetView(CharData charData)
    {
        foreach (CharViewUI cvu in viewList)
        {
            if(cvu.character == charData)
            {
                return cvu;
            }
        }


        Debug.LogError("Dont Found this character's CharViewUI");
        return null;
    }

    // Update is called once per frame
    void Update()
    {
        foreach(CharViewUI cvu in viewList)
        {
            cvu.ViewUpdate();
        }
    }
}
