using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : MonoBehaviour
{
    public static FieldManager instance { get; private set; }
    public SlotControl slotControl;
    public BattleTextControl battleTextControl;
    public CharViewUIControl charViewUIControl;
    public BattleHubControl battleHubControl;

    public CharLineControl charLineControl;

    public bool playerTurn;
    public bool isWorking = false;

    public Skill selectingSkill = null;

    public CharData currentActionCharacter;

    //public List<CharData> heros;
    //public List<CharData> enemies;


    public CharTeam heros;
    public CharTeam enemies;
    public List<CharData> allChar;

    private void Awake()
    {
        instance = this;
        heros = new CharTeam();
        enemies = new CharTeam();
        allChar = new List<CharData>();

        charLineControl = new CharLineControl();

    }
    private void Start()
    {
        isWorking = true;
        OrderManager.instance.isWaiting = false;

        //GameStart();//TEST!
        OrderManager.instance.AddOrder(new sysOrder.GameStart());
    }


    public void GameStart()
    {

        //character create!!
        allChar.Add(heros.front);
        allChar.Add(heros.middle);
        allChar.Add(heros.back);
        allChar.Add(enemies.front);
        allChar.Add(enemies.middle);
        allChar.Add(enemies.back);

        charLineControl.LoadAllCharacters();
        CharTurnStart(charLineControl.Next());
    }

    public void GameUpdate()
    {

    }


    public void CharTurnStart(CharData character)
    {
        Debug.Log("現在是" + character.charShowName + "的回合了!!");
        charViewUIControl.ShowCurrentTurnMark(character, true);
        battleHubControl.SetCharSkill(character);

        currentActionCharacter = character;

        TurnInfo tif = TriggerManager.instance.GetTriggerInfo<TurnInfo>();
        tif.SetInfo(character);
        tif.GOTrigger(TriggerType.TurnStarting);
        tif.GOTrigger(TriggerType.TurnStartBefore);

        character.actionPoint = character.maxActionPoint;

        //character turn start trigger
        //character turn start excutive
    }
    public void CharTurnEnd()
    {
        if (!OrderManager.instance.IsEmptyStack()) return;

        OrderManager.instance.AddOrder(new sysOrder.TurnEndOrder());
        OrderManager.instance.AddOrder(new sysOrder.WaitOrder(0.35f));

        TurnInfo tif = TriggerManager.instance.GetTriggerInfo<TurnInfo>();
        tif.SetInfo(currentActionCharacter);
        tif.GOTrigger(TriggerType.TurnEnding);
        tif.GOTrigger(TriggerType.TurnEndBefore);
    }
    public void RealTurnEnd()
    {
        currentActionCharacter.TurnEnd();

        charViewUIControl.ShowCurrentTurnMark(currentActionCharacter, false);
        currentActionCharacter = null;
        if (!OrderManager.instance.IsEmptyStack())
            Debug.LogWarning("Can't be any Order exist while End Turn!!!!");

        CharTurnStart(charLineControl.Next());
    }

    //---------------------------------------------------------------------

    public void SkillSelecctTarget(Skill skill)
    {
        if (selectingSkill==null)
        {
            selectingSkill = skill;
            SetCharTarget(skill);
            // this Skill need to HighLight

            
        }
        else
        {
            selectingSkill = skill;
            CancelCharTarget();
            SetCharTarget(skill);
            // this Skill need to HighLight, original skill need to cancel HighLight
        }
    }
    public void CancelSkillSelectTarget()
    {
        selectingSkill = null;
        CancelCharTarget();
        //cancel HighLight
    }



    public void SetCharTarget(Skill skill)
    {
        //Debug.Log("SELECT!!");
        switch (skill.target)
        {
            case SkillTarget.All:
                foreach (CharData charData in allChar)
                {
                    charData.charView.SetClickTarget(true);

                }
                break;
            case SkillTarget.Self:
                foreach (CharData charData in allChar)
                {
                    if (charData == skill.character)
                        charData.charView.SetClickTarget(true);

                }
                break;
            case SkillTarget.Allies:
                foreach (CharData charData in allChar)
                {
                    if (charData.isEnemy == skill.character.isEnemy)
                        charData.charView.SetClickTarget(true);

                }
                break;
            case SkillTarget.Enemies:
                foreach (CharData charData in allChar)
                {
                    if (charData.isEnemy != skill.character.isEnemy)
                        charData.charView.SetClickTarget(true);

                }
                break;
            case SkillTarget.FrontEnemy:
                foreach (CharData charData in allChar)
                {
                    if ((charData.isEnemy != skill.character.isEnemy) && ((charData==heros.front) || (charData == enemies.front)))
                        charData.charView.SetClickTarget(true);

                }
                break;
        }


    }
    public void CancelCharTarget()
    {
        foreach(CharData charData in allChar)
        {
            if (charData.charView.canClick)
                charData.charView.SetClickTarget(false);
        }
    }

    public void ClickCharTarget(CharData target)
    {
        CancelCharTarget();
        //再次CHECK 目標可行性

        selectingSkill.character.actionPoint -= selectingSkill.actionPoint;
        selectingSkill.currentCoolDown = selectingSkill.coolDown;

        SkillInfo sif = TriggerManager.instance.GetTriggerInfo<SkillInfo>();
        sif.SetInfo(selectingSkill, target);

        sif.GOTrigger(TriggerType.UseSkillCheck);

        sif.GOTrigger(TriggerType.UseSkillAfter);
        OrderManager.instance.AddOrder(new sysOrder.UseSkillOrder(selectingSkill,target));
        sif.GOTrigger(TriggerType.UseSkillBefore);
    
    }


    //---------------------------------------------------------------------
    public void DamageChar(CharData character, int damageValue,DamageType type,CharData damager=null)
    {
        DamageInfo dif = TriggerManager.instance.GetTriggerInfo<DamageInfo>();
        dif.SetInfo(character, damageValue, type, damager);

        dif.GOTrigger(TriggerType.DamageCheck);
        dif.GOTrigger(TriggerType.DamageTotalCheck);
        dif.GOTrigger(TriggerType.DamageFinalCheck);

        dif.GOTrigger(TriggerType.DamageAfter);
        OrderManager.instance.AddOrder(new sysOrder.DamageOrder(dif.damagedChar, dif.damageValue, dif.damageType, dif.damagerChar));
        dif.GOTrigger(TriggerType.DamageBefore);
    }

    //---------------------------------------------------
    public bool CheckCharIsFront(CharData chara)
    {
        return true; // 待確認!!
    }
    public bool CheckCharIsAlive(CharData chara)
    {
        return !chara.isDie;
    }

}
