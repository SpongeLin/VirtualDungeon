using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharViewUI : MonoBehaviour
{
    public CharData character;

    public Image health;
    public Text healthPoint;

    public Image currentMark;

    public bool isWorking;

    public List<CharStatusView> charStatusViewList;
    public Transform charStatusViewTransform;
    public GameObject charStatusViewObject;

    public void Awake()
    {
        charStatusViewList = new List<CharStatusView>();
        for (int i = 0; i < 15; i++)
        {
            GameObject go = Instantiate(charStatusViewObject, charStatusViewTransform);
            charStatusViewList.Add(go.GetComponent<CharStatusView>());
            go.SetActive(false);
        }
    }
    public void ViewUpdate()
    {
        if (character == null) return;
        if (!isWorking) return;
        float rate = character.health / (float)character.maxHealth;
        health.fillAmount = rate;
        healthPoint.text = character.health.ToString() + "/" + character.maxHealth.ToString();

        //actionPoint.text = character.energy.ToString()+"/"+character.maxEnergy.ToString() ;
        UpdateCharStatusView();
    }
    public void SetCurrentTurnMark(bool active)
    {
        currentMark.gameObject.SetActive(active);
    }

    void UpdateCharStatusView()
    {
        foreach(CharStatus cs in character.charStatusControl.statusList)
        {
            bool isShow = false;
            foreach(CharStatusView csv in charStatusViewList)
                if(csv.charStatus == cs)
                {
                    csv.ViewUpdate();
                    isShow = true;
                    break;
                }
            if (!isShow)
            {
                CharStatusView newView = GetView();
                if (newView != null)
                    newView.SetCharStatusView(cs);
            }
        }
        foreach (CharStatusView csv in charStatusViewList)
            if (csv.charStatus != null)
                if (!csv.charStatus.isWorking)
                    csv.Close();
    }

    CharStatusView GetView()
    {
        foreach(CharStatusView csv in charStatusViewList)
        {
            if (csv.charStatus == null)
            {
                return csv;
            }
        }
        return null;
    }
}
