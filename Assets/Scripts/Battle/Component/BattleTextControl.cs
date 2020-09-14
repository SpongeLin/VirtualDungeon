using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public enum TextType
{
    Damage,
    Heal,
    Null,
    Critical
}

public class BattleTextControl : MonoBehaviour
{
    public List<BattleText> textPool;
    public Transform textPoolTransform;

    public BattleText battleText;

    public Color damageColor1;
    public Color damageColor2;
    public Color healColor1;
    public Color healColor2;

    public float floatX;
    public float floatY;

    // Start is called before the first frame update
    void Awake()
    {
        for(int i = 0; i < 30; i++)
        {
            GameObject go = Instantiate(battleText.gameObject, textPoolTransform);
            textPool.Add(go.GetComponent<BattleText>());
        }
        
    }

    public void ShowDamageText(CharData character,int value,TextType type)
    {
        BattleText currentText = GetDamageText();
        if (currentText == null) return;

        currentText.transform.position = character.charView.transform.position;
        currentText.transform.position += new Vector3(Random.Range(-floatX, floatX), Random.Range(-floatY, floatY));
        currentText.SetText(value.ToString());
        if (type == TextType.Damage)
        {
            currentText.SetColor(damageColor1, damageColor2);
        }else if (type == TextType.Heal)
        {
            currentText.SetColor(healColor1, healColor2);
        }

        currentText.OpenText();
        currentText.transform.SetAsLastSibling();
        currentText.transform.DOPunchScale(new Vector3(1.2f, 1.2f, 1), 0.5f,5,1.3f);
    }

    BattleText GetDamageText()
    {
        foreach(BattleText dt in textPool)
        {
            if (!dt.isWorking)
            {
                return dt;                
            }
        }
        Debug.LogWarning("Dont found DamageText");
        return null;
    }
}
