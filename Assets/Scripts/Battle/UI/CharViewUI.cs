using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharViewUI : MonoBehaviour
{
    public CharData character;

    public Image health;
    public Text healthPoint;
    public Text actionPoint;

    public Image currentMark;

    public bool isWorking;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void ViewUpdate()
    {
        if (character == null) return;
        if (!isWorking) return;
        float rate = character.health / (float)character.maxHealth;
        health.fillAmount = rate;
        healthPoint.text = character.health.ToString() + "/" + character.maxHealth.ToString();

        actionPoint.text = character.actionPoint.ToString()+"/"+character.maxActionPoint.ToString() ;
    }
    public void SetCurrentTurnMark(bool active)
    {
        currentMark.gameObject.SetActive(active);
    }
}
