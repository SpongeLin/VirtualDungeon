using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharStatusView : MonoBehaviour
{
    public Image statusImage;
    public Text statusNum;

    public CharStatus charStatus;

    public void SetCharStatusView(CharStatus cs)
    {
        gameObject.SetActive(true);
        Sprite s = Resources.Load<Sprite>("CharStatus/" + cs.statusName);
        statusImage.sprite = s;

        charStatus = cs;
    }
    public void ViewUpdate()
    {
        if (charStatus != null)
        {
            statusNum.text = charStatus.GetNum();
        }
    }
    public void Close()
    {
        gameObject.SetActive(false);
        charStatus = null;
    }
}
