using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleText : MonoBehaviour
{
    Text text;
    TextGradient textGradient;
    public bool isWorking = false;

    private void Awake()
    {
        text = GetComponent<Text>();
        textGradient = GetComponent<TextGradient>();
    }

    public void SetText(string content)
    {
        text.text = content;
    }
    public void SetColor(Color top,Color bottom)
    {
        textGradient.topColor = top;
        textGradient.bottomColor = bottom;
    }
    public void OpenText()
    {
        isWorking = true;
        StartCoroutine(Close());
    }

    public void CloseText()
    {
        isWorking = false;
        transform.localPosition = Vector3.zero;
    }

    IEnumerator Close()
    {
        yield return new WaitForSeconds(1.05f);
        CloseText();
    }
}
