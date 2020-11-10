using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHubControl : MonoBehaviour
{
    public Text cardInfo;
    public Text energyInfo;

    public GameObject turnEndButton;

    public BattleSkillView skillView1;
    public BattleSkillView skillView2;
    public BattleSkillView skillView3;

    public GameObject arrowImage;
    bool arrowIsWorking;

    public void Update()
    {
        if (arrowIsWorking)
        {
            if (CardManager.instance.currentDragCard == null) return;
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            arrowImage.transform.position = new Vector3(mousePos.x, mousePos.y, CardManager.instance.cardViewControl.handCardPlace.transform.position.z);

            Vector3 cardPos = CardManager.instance.currentDragCard.transform.position;
            Vector2 r = (cardPos - mousePos).normalized;
            arrowImage.transform.rotation = Quaternion.LookRotation(r);
            arrowImage.transform.Rotate(0, 90, 0);

        }
    }

    public void Gameupdate()
    {
        skillView1.Gameupdate();
        skillView2.Gameupdate();
        skillView3.Gameupdate();

        cardInfo.text = "牌組：" + CardManager.instance.deck.Count + "\n棄牌：" + CardManager.instance.cemetery.Count + "\n守牌：" +CardManager.instance.handCards.Count + "\n除外：" + CardManager.instance.banish.Count;
        if (FieldManager.instance.currentActionCharacter != null)
        {
            cardInfo.text += "\n卡牌傷害：" + FieldManager.instance.currentActionCharacter.power;
            cardInfo.text += "\n能量：" + FieldManager.instance.currentActionCharacter.energy + "/" + FieldManager.instance.currentActionCharacter.maxEnergy;
            if (!FieldManager.instance.currentActionCharacter.isEnemy)
                energyInfo.text = FieldManager.instance.currentActionCharacter.energy + "/" + FieldManager.instance.currentActionCharacter.maxEnergy;
            else
                energyInfo.text = "XXX";
        }
    }

    public void SetCharSkill(CharData character)
    {
        skillView1.SetSkill(character.skillControl.skill1);
        skillView2.SetSkill(character.skillControl.skill2);
        skillView3.SetSkill(character.skillControl.skill3);
    }
    public void SetArrow(bool active)
    {
        arrowImage.SetActive(active);
        arrowIsWorking = active;
    }

    /*
    public void ShowCharInfo(CharData character)
    {
        if (!character.isEnemy)
        {
            heroInfo.SetInfo(character);
        }
        else
        {
            enemyInfo.SetInfo(character);
        }
    }
    */

    public void SetTurnEndButton(bool active)
    {
        turnEndButton.SetActive(active);
    }
    public void TurnEndButton()
    {
        FieldManager.instance.CharTurnEnd();
    }

}
