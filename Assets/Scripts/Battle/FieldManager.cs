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
    public CharLineViewControl charLineViewControl;

    public FieldStatusControl fieldStatusControl;

    public bool playerTurn;
    public bool isWorking = false;
    public bool gameStart = false;


    public Skill selectingSkill = null;
    public CardView selectingCard = null;

    public CharData currentActionCharacter;


    //public List<CharData> heros;
    //public List<CharData> enemies;

    public int handCardNum;



    public CharTeam heros;
    public CharTeam enemies;
    public List<CharData> allChar;

    private void Awake()
    {
        instance = this;

        fieldStatusControl = new FieldStatusControl();
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

        foreach(int cardNo in GameData.instance.deck)
        {
            CardManager.instance.deck.Add(CardCreator.CreateCard(cardNo));
        }
        handCardNum = GameData.instance.handCardNum;
        //for (int i = 0; i < handCardNum; i++)
        //    OrderManager.instance.AddOrder(new sysOrder.DrawOrder());

        //Create hero by GameData
        //instatiate(CharView)
        //CharCreator(CharView,name)
        //chardata set heros.front

        //Create enemy by EnemyManager


    }


    public void GameStart()
    {
        gameStart = true;
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
        if (CardManager.instance.handCards.Count < handCardNum)
            if (CardManager.instance.deck.Count != 0 || CardManager.instance.cemetery.Count != 0)
                OrderManager.instance.AddOrder(new sysOrder.DrawOrder());



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
        playerTurn = !character.isEnemy;
        currentActionCharacter = character;
        if (!character.isEnemy)
        {
            battleHubControl.SetCharSkill(character);
            character.energy = character.maxEnergy;
            battleHubControl.SetTurnEndButton(true);

        }
        else
        {
            character.enemyStrategy.TurnStart();
            //CharSkill close.
        }

        TurnInfo tif = TriggerManager.instance.GetTriggerInfo<TurnInfo>();
        tif.SetInfo(character);
        tif.GoTrigger(TriggerType.TurnStarting);
        tif.GoTrigger(TriggerType.TurnStartBefore);



        //character turn start trigger
        //character turn start excutive
    }
    public void CharTurnEnd()
    {
        if (!OrderManager.instance.IsEmptyStack()) return;
        if(playerTurn)
            battleHubControl.SetTurnEndButton(false);

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

    public void UpdateCharLineView()
    {
        charLineViewControl.UpdateCharLineView();
    }
    
    //---------------------------------------------------------------------
    public void CardSelectTarget(CardView card)
    {
        if (selectingSkill != null || selectingCard != null)
        {
            CancelTarget();
        }
        selectingCard = card;
        SetTarget(card);
    }

    public void SkillSelecctTarget(Skill skill)
    {
        if (selectingSkill != null || selectingCard !=null)
        {
            CancelTarget();
        }
        selectingSkill = skill;
        SetTarget(skill);
        // this Skill need to HighLight, original skill need to cancel HighLight

    }
    public void CancelSkillSelectTarget()
    {
        selectingSkill = null;
        CancelTarget();
        //cancel HighLight
    }



    public void SetTarget(Skill skill)
    {
        CharData[] targets = GetCharTarget(skill);
        foreach (CharData chara in targets)
            chara.charView.SetClickTarget(true);
    }
    public void SetTarget(CardView card)
    {
        List<CharData> targets = new List<CharData>();
        switch (card.card.cardTarget)
        {
            case CardTarget.CardSelf:
                card.SetCkickTarget(true);
                break;
            case CardTarget.All:
                foreach (CharData charData in allChar)
                {
                    if (charData.isDie)
                        continue;
                    targets.Add(charData);

                }
                break;
            case CardTarget.Allies:
                foreach (CharData charData in allChar)
                {
                    if (charData.isDie)
                        continue;
                    if (!charData.isEnemy)
                        targets.Add(charData);
                }
                break;
            case CardTarget.Enemies:
                foreach (CharData charData in allChar)
                {
                    if (charData.isDie)
                        continue;
                    if (charData.isEnemy)
                        targets.Add(charData);
                }
                break;
        }
        foreach (CharData chara in targets)
            chara.charView.SetClickTarget(true);
    }
    public void CancelTarget()
    {
        if (selectingCard != null)
        {
            selectingCard.SetCkickTarget(false);
        }
        foreach(CharData charData in allChar)
        {
            if (charData.charView.canClick)
                charData.charView.SetClickTarget(false);
        }

        selectingCard = null;
        selectingSkill = null;
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

        if (selectingSkill != null)
        {
            //再次CHECK 目標可行性
            UseSkill(target);
            return;
        }
        if (selectingCard != null)
        {
            //再次CHECK 目標可行性
            UseCardPrepare(target);
            return;
        }
        Debug.LogWarning("This is seem wrong.");
    }
    void UseSkill(CharData chara)
    {
        Skill useSkill = selectingSkill;
        CancelTarget();

        currentActionCharacter.energy -= useSkill.actionPoint;
        useSkill.currentCoolDown = useSkill.coolDown;

        SkillInfo sif = TriggerManager.instance.GetTriggerInfo<SkillInfo>();
        sif.SetInfo(useSkill, chara);

        sif.GoTrigger(TriggerType.UseSkillCheck);

        sif.GoTrigger(TriggerType.UseSkillAfter);
        OrderManager.instance.AddOrder(new sysOrder.UseSkillOrder(useSkill, chara));
        sif.GoTrigger(TriggerType.UseSkillBefore);

    }
    public void UseCardPrepare(CharData chara=null)
    {
        selectingCard.card.targetChar = chara;
        CardManager.instance.UseCard(selectingCard.card);

        CancelTarget();
    }
    //---------------------------------------------------------------




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
    public void GainEnergy(int energyNum)
    {
        if (energyNum <= 0) return;
        currentActionCharacter.energy += energyNum;
    }
    public void GiveCharStatus(CharData chara,string statusName,int statusNum)
    {
        CharStatus cs = StatusCreator.CreateCharStatus(statusName, statusNum);
        if (cs != null)
            chara.charStatusControl.EnterStatus(cs);
    }
    public void GiveCardStatus(CardData card,string statusName,int statusNum)
    {
        CardStatus cs = StatusCreator.CreateCardStatus(statusName, statusNum);
        if (cs != null)
            card.cardStatusControl.EnterStatus(cs);
    }
    public void GiveFieldStatus(string statusName,int statusNum)
    {
        FieldStatus fs = StatusCreator.CreateFieldStatus(statusName, statusNum);
        if (fs != null)
            fieldStatusControl.EnterStatus(fs);
    }

    //---------------------------------------------------

    public bool CheckSkillCanUse(Skill skill)
    {


        return true;
    }

    //=================================================
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
