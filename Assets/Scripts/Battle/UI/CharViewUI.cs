using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharViewUI : MonoBehaviour
{
    public CharData character;

    public Image health;
    public Text healthPoint;
    public Text armor;
    public Text charName;

    public Animator currentMarkAnimator;
    public Animator guardMarkAnimator;

    public bool isWorking;

    public List<CharStatusView> charStatusViewList;
    public Transform charStatusViewTransform;
    public GameObject charStatusViewObject;

    public GameObject enemyIntention;
    public Image enemyIntentionImage;
    public Text enemyIntentionNum;
    bool enemyIntentionActive;

    bool canClick = false;
    public Animator targetMark;

    bool guard = false;

    public void Awake()
    {
        charStatusViewList = new List<CharStatusView>();

        charStatusViewObject.SetActive(false);
        enemyIntention.SetActive(false);
        enemyIntentionNum.gameObject.SetActive(false);
        enemyIntentionActive = false;
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
        armor.text = character.armor.ToString();

        charName.text = character.charShowName;

        //actionPoint.text = character.energy.ToString()+"/"+character.maxEnergy.ToString() ;
        UpdateCharStatusView();

        if (!canClick && character.charView.canClick)
        {
            canClick = true;
            targetMark.SetTrigger("TargetMark");
        }
        else if (canClick && !character.charView.canClick)
        {
            canClick = false;
            targetMark.SetTrigger("NotTarget");
        }

        if(!guard && character.guardCount > 0)
        {
            guardMarkAnimator.gameObject.SetActive(true);
            guard = true;
        }else if(guard && character.guardCount <= 0)
        {
            guardMarkAnimator.gameObject.SetActive(false);
            guard = false;
        }
        


        if (character.enemyStrategy != null)
        {
            UpdateEnemyIntention();
        }
    }
    public void SetCurrentTurnMark(bool active)
    {
        currentMarkAnimator.gameObject.SetActive(active);
        if (active)
            currentMarkAnimator.SetTrigger("Enter");
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
            if (!isShow && !cs.notDisplay)
            {
                CharStatusView newView = GetView();
                if (newView != null)
                    newView.SetCharStatusView(cs);
            }
        }
        foreach (CharStatusView csv in charStatusViewList)
            if (csv.charStatus != null)
                if (!csv.charStatus.isWorking || csv.charStatus.notDisplay)
                    csv.Close();
    }
    void UpdateEnemyIntention()
    {
        if (enemyIntentionActive)
        {
            if (!character.enemyStrategy.strategyShow)
            {
                enemyIntention.SetActive(false);
                enemyIntentionNum.gameObject.SetActive(false);
                enemyIntentionImage.sprite = null;
                enemyIntentionActive = false;
                return;
            }
        }
        else
        {
            if (character.enemyStrategy.strategyShow)
            {
                enemyIntention.SetActive(true);
                enemyIntentionNum.gameObject.SetActive(true);
                Sprite s = Resources.Load<Sprite>("EnemyIntention/" + character.enemyStrategy.currentAction.actionImage);
                enemyIntentionImage.sprite = s;
                enemyIntentionActive = true;
            }
            else return;
        }

        enemyIntentionNum.text = character.enemyStrategy.currentAction.actionContent;

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
