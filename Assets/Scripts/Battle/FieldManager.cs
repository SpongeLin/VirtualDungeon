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

        //Create hero by GameData
        //instatiate(CharView)
        //CharCreator(CharView,name)
        //chardata set heros.front
        
        //Create enemy by EnemyManager


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


        //Hero go to field
        //slotControl.SetSlotPos(heros.front.charView, TeamPos.Front, false);
        //slotControl.SetSlotPos(heros.middle.charView, TeamPos.Middle, false);
        //slotControl.SetSlotPos(heros.back.charView, TeamPos.Back, false);

        charLineControl.LoadAllCharacters();
        CharTurnStart(charLineControl.Next());
    }

    public void GameUpdate()
    {
        foreach(CharData chara in allChar)
        {
            if (!chara.isDie)
                if (chara.health <= 0)
                    OrderManager.instance.AddOrder(new sysOrder.DealthOrder(chara));
        }

        battleHubControl.Gameupdate();

        if (CheckTeamLose(true))
            Debug.Log("Player is Win！！！");
        else if(CheckTeamLose(false))
            Debug.Log("Player is Lose！！！");

    }


    public void CharTurnStart(CharData character)
    {
        Debug.Log("現在是" + character.charShowName + "的回合了!!");
        charViewUIControl.ShowCurrentTurnMark(character, true);
        battleHubControl.SetCharSkill(character);

        currentActionCharacter = character;

        TurnInfo tif = TriggerManager.instance.GetTriggerInfo<TurnInfo>();
        tif.SetInfo(character);
        tif.GoTrigger(TriggerType.TurnStarting);
        tif.GoTrigger(TriggerType.TurnStartBefore);

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
        tif.GoTrigger(TriggerType.TurnEnding);
        tif.GoTrigger(TriggerType.TurnEndBefore);
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
        CharData[] targets = GetCharTarget(skill);
        foreach (CharData chara in targets)
            chara.charView.SetClickTarget(true);
    }
    public void CancelCharTarget()
    {
        foreach(CharData charData in allChar)
        {
            if (charData.charView.canClick)
                charData.charView.SetClickTarget(false);
        }
    }

    public bool CheckCharTargetExist(Skill skill)
    {
        return true;
    }
    CharData[] GetCharTarget(Skill skill)
    {
        List<CharData> targets = new List<CharData>();
        switch (skill.target)
        {
            case SkillTarget.All:
                foreach (CharData charData in allChar)
                {
                    if (charData.isDie)
                        continue;
                    targets.Add(charData);

                }
                break;
            case SkillTarget.Self:
                foreach (CharData charData in allChar)
                {
                    if (charData.isDie)
                        continue;
                    if (charData == skill.character)
                        targets.Add(charData);

                }
                break;
            case SkillTarget.Allies:
                foreach (CharData charData in allChar)
                {
                    if (charData.isDie)
                        continue;
                    if (charData.isEnemy == skill.character.isEnemy)
                        targets.Add(charData);

                }
                break;
            case SkillTarget.Enemies:
                foreach (CharData charData in allChar)
                {
                    if (charData.isDie)
                        continue;
                    if (charData.isEnemy != skill.character.isEnemy)
                        targets.Add(charData);

                }
                break;
            case SkillTarget.FrontEnemy:
                foreach (CharData charData in allChar)
                {
                    if (charData.isDie)
                        continue;
                    if ((charData.isEnemy != skill.character.isEnemy) && CheckCharIsFront(charData))
                        targets.Add(charData);

                }
                break;
        }
        return targets.ToArray();
    }

    public void ClickCharTarget(CharData target)
    {
        CancelCharTarget();
        //再次CHECK 目標可行性

        selectingSkill.character.actionPoint -= selectingSkill.actionPoint;
        selectingSkill.currentCoolDown = selectingSkill.coolDown;

        SkillInfo sif = TriggerManager.instance.GetTriggerInfo<SkillInfo>();
        sif.SetInfo(selectingSkill, target);

        sif.GoTrigger(TriggerType.UseSkillCheck);

        sif.GoTrigger(TriggerType.UseSkillAfter);
        OrderManager.instance.AddOrder(new sysOrder.UseSkillOrder(selectingSkill,target));
        sif.GoTrigger(TriggerType.UseSkillBefore);
    
    }


    //---------------------------------------------------------------------
    public void DamageChar(CharData character, int damageValue,DamageType type,CharData damager=null)
    {
        DamageInfo dif = TriggerManager.instance.GetTriggerInfo<DamageInfo>();
        dif.SetInfo(character, damageValue, type, damager);

        dif.GoTrigger(TriggerType.DamageCheck);
        dif.GoTrigger(TriggerType.DamageTotalCheck);
        dif.GoTrigger(TriggerType.DamageFinalCheck);

        dif.GoTrigger(TriggerType.DamageAfter);
        OrderManager.instance.AddOrder(new sysOrder.DamageOrder(dif.damagedChar, dif.damageValue, dif.damageType, dif.damagerChar));
        dif.GoTrigger(TriggerType.DamageBefore);
    }
    public void DealthChar(CharData character)
    {
        DealthInfo dif = TriggerManager.instance.GetTriggerInfo<DealthInfo>();
        dif.SetInfo(character);

        dif.GoTrigger(TriggerType.DealthAfter);
        OrderManager.instance.AddOrder(new sysOrder.DealthOrder(character));
        dif.GoTrigger(TriggerType.DealthBefore);

    }
    //--------------------------------------------------
    public void RealDamage(CharData character, int damageValue, DamageType type, CharData damager = null)
    {

    }
    public void RealDealth(CharData character)
    {
        if (character.isDie)
            Debug.LogError("Something is wrong, character was died twice.");

        character.isDie = true;
        charViewUIControl.CloseCharViewUI(character.charView);
        charLineControl.RemoveChar(character);
        character.charStatusControl.ExitAll();
        //skillControl.???
    }

    //---------------------------------------------------
    public bool CheckCharIsFront(CharData chara)
    {
        CharTeam charTeam;
        if (chara.isEnemy)
            charTeam = enemies;
        else
            charTeam = heros;

        if (!charTeam.front.isDie)
            if (chara == charTeam.front)
                return true;
        if (charTeam.front.isDie)
            if (chara == charTeam.middle)
                return true;
        if (charTeam.front.isDie)
            if (charTeam.middle.isDie)
                if (chara == charTeam.back)
                    return true;

        return false;
    }
    public bool CheckCharIsAlive(CharData chara)
    {
        return !chara.isDie;
    }


    bool CheckTeamLose(bool isEnemy)
    {
        CharTeam temp;
        if (isEnemy)
            temp = enemies;
        else
            temp = heros;

        if (temp.front != null)
            if (!temp.front.isDie)
                return false;
        if (temp.middle != null)
            if (!temp.middle.isDie)
                return false;
        if (temp.back != null)
            if (!temp.back.isDie)
                return false;

        return true;
    }

}
