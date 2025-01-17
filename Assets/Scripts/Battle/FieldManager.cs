﻿using System.Collections;
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
    public IntroViewControl introViewControl;
    public FieldStatusControl fieldStatusControl;

    public BattleRecorder battleRecorder;

    public GameOverContorl gameOverContorl;
    public ArrowRenderer arrow;
    bool arrowActive;

    public bool playerTurn;
    public bool isWorking = false;
    public bool gameStart = false;

    public CharData currentActionCharacter;

    public GameObject charViewObject;
    public Transform heroPlace;


    public Skill selectingSkill = null;

    public CharData currentMouseOverCharacter;


    //public List<CharData> heros;
    //public List<CharData> enemies;

    public int handCardNum;


    public int overLoadNum;



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

        battleRecorder = new BattleRecorder();

    }
    private void Update()
    {
        if (arrowActive)
            ArrowUpdate();
    }
    private void Start()
    {
        isWorking = true;
        OrderManager.instance.isWaiting = false;

        //GameStart();//TEST!
        OrderManager.instance.AddOrder(new sysOrder.GameStart());
        OrderManager.instance.AddOrder(new sysOrder.WaitOrder(0.4f));
        OrderManager.instance.AddOrder(new sysOrder.StartPrepare());
        OrderManager.instance.AddOrder(new sysOrder.WaitOrder(0.84f));


        CharCreate(GameData.instance.front, TeamPos.Front, false);
        CharCreate(GameData.instance.middle, TeamPos.Middle, false);
        CharCreate(GameData.instance.back, TeamPos.Back, false);
        CharCreate(GameData.instance.frontEnemy, TeamPos.Front, true);
        CharCreate(GameData.instance.middleEnemy, TeamPos.Middle, true);
        CharCreate(GameData.instance.backEnemy, TeamPos.Back, true);

        //for (int i = 0; i < handCardNum; i++)
        //    OrderManager.instance.AddOrder(new sysOrder.DrawOrder());

        //Create hero by GameData
        //instatiate(CharView)
        //CharCreator(CharView,name)
        //chardata set heros.front

        //Create enemy by EnemyManager


    }

    public void CharCreate(CharacterDataPack cdp, TeamPos pos, bool isEnemy)
    {
        GameObject go = Instantiate(charViewObject, heroPlace);
        CharView cv = go.GetComponent<CharView>();
        CharacterCreator.TestCreat( cdp, cv);

        GameObject avatar = Resources.Load<GameObject>("CharAvatar/" + cdp.heroName);
        GameObject avatarInstance = Instantiate(avatar, cv.transform);
        cv.animator = avatarInstance.GetComponent<Animator>();
        if (isEnemy) avatarInstance.transform.localScale = new Vector3(-1, 1, 1);

        SetCharTeamPos(cv.character, pos, isEnemy);
        allChar.Add(cv.character);
        cv.character.charStatusControl.EnterStatus(new nCharStatus.PowerDisplay());
        cv.character.charStatusControl.EnterStatus(new nCharStatus.MagicDisplay());

        slotControl.SetSlotPos(cv, pos, isEnemy, true); //wait
        //charViewUIControl.OpenCharViewUI(cv);
    }
    public void StartPrepare()
    {
        foreach (int cardNo in GameData.instance.deck)
        {
            CardManager.instance.deck.Add(CardCreator.CreateCard(cardNo));
        }
        CardManager.instance.Shuffle();
        handCardNum = GameData.instance.handCardNum;

        foreach (CharData chara in allChar)
        {
            charViewUIControl.OpenCharViewUI(chara.charView);
        }
    }


    public void GameStart()
    {
        gameStart = true;
        //character create!!


        //Hero go to field
        //slotControl.SetSlotPos(heros.front.charView, TeamPos.Front, false);
        //slotControl.SetSlotPos(heros.middle.charView, TeamPos.Middle, false);
        //slotControl.SetSlotPos(heros.back.charView, TeamPos.Back, false);

        charLineControl.LoadAllCharacters();
        CharTurnStart(charLineControl.Next());
    }

    public void GameUpdate()
    {
        foreach (CharData chara in allChar)
        {
            if (!chara.isDie)
                if (chara.health <= 0)
                    OrderManager.instance.AddOrder(new sysOrder.DealthOrder(chara));
        }
        if (CardManager.instance.handCards.Count < handCardNum && OrderManager.instance.IsEmptyStack())
            if (CardManager.instance.deck.Count != 0 || CardManager.instance.cemetery.Count != 0)
            {
                OrderManager.instance.AddOrder(new sysOrder.DrawOrder());
                OrderManager.instance.AddOrder(new sysOrder.WaitOrder(0.15f));
            }



        battleHubControl.Gameupdate();

        if (CheckTeamLose(true))
            GameOver(true);
        else if (CheckTeamLose(false))
            GameOver(false);

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

            if (overLoadNum > 0)
            {
                character.ReduceEnergy(overLoadNum);
                overLoadNum = 0;
            }
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
        if (playerTurn)
        {
            battleHubControl.SetTurnEndButton(false);
            CancelTarget();
        }


        OrderManager.instance.AddOrder(new sysOrder.TurnEndOrder());
        OrderManager.instance.AddOrder(new sysOrder.WaitOrder(0.35f));

        TurnInfo tif = TriggerManager.instance.GetTriggerInfo<TurnInfo>();
        tif.SetInfo(currentActionCharacter);
        tif.GoTrigger(TriggerType.TurnEnding);
        tif.GoTrigger(TriggerType.TurnEndBefore);
    }
    public void RealTurnEnd()
    {
        battleRecorder.ResetTurn();
        currentActionCharacter.TurnEnd();

        charViewUIControl.ShowCurrentTurnMark(currentActionCharacter, false);
        currentActionCharacter = null;
        if (!OrderManager.instance.IsEmptyStack())
            Debug.LogWarning("Can't be any Order exist while End Turn!!!!");

        CharTurnStart(charLineControl.Next());
    }

    public void UpdateCharLineView(bool anima=false)
    {
        if (anima)
        {
            charLineViewControl.AnimaNext();
        }
        else
        {
            charLineViewControl.UpdateCharLineView();
        }

    }

    //---------------------------------------------------------------------

    public void SkillSelecctTarget(Skill skill)
    {
        if (selectingSkill != null)
        {
            CancelTarget();
        }
        selectingSkill = skill;
        SetTarget(skill);
        // this Skill need to HighLight, original skill need to cancel HighLight

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
        targets.AddRange(GetConditionChar(card.card.charFilters.ToArray()));
        /*
        switch (card.card.cardTarget)
        {
            case CardTarget.CardSelf:
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
        */
        foreach (CharData chara in targets)
            chara.charView.SetClickTarget(true);
    }
    public void CancelTarget()
    {
        foreach (CharData charData in allChar)
        {
            if (charData.charView.canClick)
                charData.charView.SetClickTarget(false);
        }
        CancelSkillSelectTarget();

        FieldManager.instance.currentMouseOverCharacter = null;
        selectingSkill = null;
    }
    void CancelSkillSelectTarget()
    {

        //cancel selectingSkill HighLight
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
                    if ((charData.isEnemy != skill.character.isEnemy) && CheckCharIsFrontest(charData))
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
        OrderManager.instance.AddOrder(new sysOrder.CharMoveOrder());
        sif.GoTrigger(TriggerType.UseSkillBefore);

    }
    //---------------------------------------------------------------
    public void SetArrow(bool active)
    {
        //battleHubControl.SetArrow(active);

        arrow.gameObject.SetActive(active);
        arrowActive = active;
    }
    void ArrowUpdate()
    {

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos = new Vector3(mousePos.x, mousePos.y, 0);
        Vector3 start = new Vector3(CardManager.instance.currentDragCard.transform.position.x, CardManager.instance.currentDragCard.transform.position.y, 0);
        if((mousePos - start).magnitude < 1.4f)
        {
            if (arrow.gameObject.activeSelf)
                arrow.gameObject.SetActive(false);
        }
        else
        {
            if (!arrow.gameObject.activeSelf)
                arrow.gameObject.SetActive(true);
            arrow.SetPositions(start, mousePos);
        }
    }



    //---------------------------------------------------------------------
    public void DealthChar(CharData character)
    {
        CharInfo cif = TriggerManager.instance.GetTriggerInfo<CharInfo>();
        cif.SetInfo(character);

        cif.GoTrigger(TriggerType.DealthAfter);
        OrderManager.instance.AddOrder(new sysOrder.DealthOrder(character));
        cif.GoTrigger(TriggerType.DealthBefore);

    }
    //--------------------------------------------------
    public void RealDamage(CharData character, int damageValue, DamageType type, CharData damager = null)
    {
        character.ReduceHealth(damageValue);

        FieldManager.instance.battleTextControl.ShowDamageText(character, damageValue, TextType.Damage);
        Debug.Log(character.charShowName + " 受到 " + damageValue + " 點傷害!");
    }
    public void RealDealth(CharData character)
    {
        if (character.isDie)
        {
            Debug.LogWarning("Something is wrong, character was died twice.");
            return;
        }

        character.isDie = true;
        charViewUIControl.CloseCharViewUI(character.charView);
        charLineControl.RemoveChar(character);
        character.charStatusControl.ExitAll();
        character.charView.CharDie();
        //skillControl.???
    }
    public void CharGainMagic(CharData chara,int magicNum)
    {
        if (magicNum <= 0) return;
        chara.magicPoint += magicNum;

        CharInfo cif = TriggerManager.instance.GetTriggerInfo<CharInfo>();
        cif.SetInfo(chara, magicNum);
        cif.GoTrigger(TriggerType.GainMagic);
    }
    public void CharMagicEffect(CharData character,int loseNum,bool triggerEffect = true)
    {
        if (loseNum < 0) return;
        character.magicPoint -= loseNum;

        if (triggerEffect)
        {
            battleRecorder.Magic(loseNum);
            //Trigger!!
        }
    }
    public void OverLoadEffect(int overLoad)
    {
        if (overLoadNum < 0) return;

        overLoadNum += overLoad;
        battleRecorder.OverLoad(overLoad);
        //Update OverLoad Mark

        //Trigger!!
    }
    public void OverLoadEffect()
    {
        overLoadNum = 0;
        //Update OverLoad Mark
    }
    //---------------------------------------------------
    public void ArmorChar(CharData chara ,int armorNum,CharData user=null)
    {
        if (armorNum < 0) return;
        chara.armor += armorNum;

        CharInfo cif = TriggerManager.instance.GetTriggerInfo<CharInfo>();
        cif.SetInfo(chara, armorNum,user);
        cif.GoTrigger(TriggerType.Armor);
        battleTextControl.ShowDamageText(chara, armorNum, TextType.Armor);
    }
    public void HealChar(CharData chara, int healNum, CharData user = null)
    {
        if (healNum < 0) return;
        chara.HealHealth(healNum);

        CharInfo cif = TriggerManager.instance.GetTriggerInfo<CharInfo>();
        cif.SetInfo(chara, healNum, user);
        cif.GoTrigger(TriggerType.Heal);
        battleTextControl.ShowDamageText(chara, healNum, TextType.Heal);
    }
    public void GainEnergy(CharData character,int energyNum)
    {
        if (energyNum <= 0) return;
        character.energy += energyNum;
    }
    public void GainPower(CharData character, int powerNum)
    {
        if (powerNum <= 0) return;
        character.power += powerNum;

        CharInfo cif = TriggerManager.instance.GetTriggerInfo<CharInfo>();
        cif.SetInfo(character, powerNum);
        cif.GoTrigger(TriggerType.GainPower);
    }
    public void GiveCharStatus(CharData chara, string statusName, int statusNum)
    {
        CharStatus cs = StatusCreator.CreateCharStatus(statusName, statusNum);
        if (cs != null)
            chara.charStatusControl.EnterStatus(cs);
    }
    public void GiveCardStatus(CardData card, string statusName, int statusNum)
    {
        CardStatus cs = StatusCreator.CreateCardStatus(statusName, statusNum);
        if (cs != null)
            card.cardStatusControl.EnterStatus(cs);
    }
    public void GiveFieldStatus(string statusName, int statusNum)
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


    public void CharMove(CharData chara,bool isEnemy)
    {
        chara.charView.CharMove(isEnemy);
    }
    public void CurrentCharMove()
    {
        currentActionCharacter.charView.CharMove(currentActionCharacter.isEnemy);
    }

    //=================================================
    public CharData[] GetConditionChar(CharFilter[] filters, bool dontCardGuard = false)
    {
        List<CharData> charList = new List<CharData>();

        foreach(CharData chara in allChar)
        {
            if (chara.isDie) continue;

            bool active = true;
            foreach(CharFilter cf in filters)
            {
                if (!cf.CheckFilter(chara))
                {
                    active = false;
                    break;
                }
            }

            if (active)
                charList.Add(chara);
        }
        if (charList.Count == 0)
            return null;

        if (!dontCardGuard)
        {
            bool guard = false;
            foreach (CharData chara in charList)
                if (chara.guardCount >= 1)
                {
                    guard = true;
                    break;
                }
            if (guard)
                charList.RemoveAll(NotGuard);

            if (charList.Count == 0)
                return null;
        }

        return charList.ToArray();
    }
    public CharData GetRandomChar(CharFilter[] filters, bool careGuard = false)
    {
        CharData[] charList = GetConditionChar(filters, !careGuard);
        if (charList == null)
            return null;

        int r = Random.Range(0, charList.Length);
        return charList[r];
    }
    bool NotGuard(CharData chara)
    {
        return chara.guardCount == 0;
    }

    public bool CheckCharIsFrontest(CharData chara)
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
            if (chara == charTeam.middle && !chara.isDie)
                return true;
        if (charTeam.front.isDie)
            if (charTeam.middle.isDie)
                if (chara == charTeam.back)
                    return true;

        return false;
    }
    public bool CheckCharIsBackest(CharData chara)
    {
        CharTeam charTeam;
        if (chara.isEnemy)
            charTeam = enemies;
        else
            charTeam = heros;

        if (!charTeam.back.isDie)
            if (chara == charTeam.back)
                return true;
        if (charTeam.back.isDie)
            if (chara == charTeam.middle)
                return true;
        if (charTeam.back.isDie)
            if (charTeam.middle.isDie)
                if (chara == charTeam.front)
                    return true;

        return false;
    }
    public void ChangeTeamPos(CharData chara,TeamPos pos)
    {
        CharTeam charTeam;
        if (chara.isEnemy)
            charTeam = enemies;
        else
            charTeam = heros;

        if (GetTeamPos(chara) == pos)
            return;
        switch (GetTeamPos(chara))
        {
            case TeamPos.Front:
                if (pos == TeamPos.Middle) { 
                    slotControl.SetSlotPos(chara.charView,TeamPos.Middle, chara.isEnemy);
                    slotControl.SetSlotPos(charTeam.middle.charView, TeamPos.Front, chara.isEnemy);

                    CharData temp = charTeam.front;
                    charTeam.front = charTeam.middle;
                    charTeam.middle = temp;
                }else if(pos == TeamPos.Back)
                {
                    slotControl.SetSlotPos(chara.charView, TeamPos.Back, chara.isEnemy);
                    slotControl.SetSlotPos(charTeam.middle.charView, TeamPos.Front, chara.isEnemy);
                    slotControl.SetSlotPos(charTeam.back.charView, TeamPos.Middle, chara.isEnemy);

                    CharData temp = charTeam.back;
                    charTeam.back = charTeam.front;
                    charTeam.front = charTeam.middle;
                    charTeam.middle = temp;
                }
                break;
            case TeamPos.Middle:
                if(pos == TeamPos.Front)
                {
                    slotControl.SetSlotPos(chara.charView, TeamPos.Front, chara.isEnemy);
                    slotControl.SetSlotPos(charTeam.front.charView, TeamPos.Middle, chara.isEnemy);

                    CharData temp = charTeam.front;
                    charTeam.front = charTeam.middle;
                    charTeam.middle = temp;
                }
                else if(pos == TeamPos.Back)
                {
                    slotControl.SetSlotPos(chara.charView, TeamPos.Back, chara.isEnemy);
                    slotControl.SetSlotPos(charTeam.back.charView, TeamPos.Middle, chara.isEnemy);

                    CharData temp = charTeam.back;
                    charTeam.back = charTeam.middle;
                    charTeam.middle = temp;
                }
                break;
            case TeamPos.Back:
                if (pos == TeamPos.Middle)
                {
                    slotControl.SetSlotPos(chara.charView, TeamPos.Middle, chara.isEnemy);
                    slotControl.SetSlotPos(charTeam.middle.charView, TeamPos.Back, chara.isEnemy);

                    CharData temp = charTeam.back;
                    charTeam.back = charTeam.middle;
                    charTeam.middle = charTeam.back;
                }
                else if (pos == TeamPos.Front)
                {
                    slotControl.SetSlotPos(chara.charView, TeamPos.Front, chara.isEnemy);
                    slotControl.SetSlotPos(charTeam.middle.charView, TeamPos.Back, chara.isEnemy);
                    slotControl.SetSlotPos(charTeam.front.charView, TeamPos.Middle, chara.isEnemy);

                    CharData temp = charTeam.front;
                    charTeam.front = charTeam.back;
                    charTeam.back = charTeam.middle;
                    charTeam.middle = temp;
                }
                break;
        }

    }
    public TeamPos GetTeamPos(CharData chara)
    {
        if (chara == heros.front || chara == enemies.front)
            return TeamPos.Front;
        if (chara == heros.middle || chara == enemies.middle)
            return TeamPos.Middle;
        if (chara == heros.back || chara == enemies.back)
            return TeamPos.Back;

        return TeamPos.Null;
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
    void SetCharTeamPos(CharData chara,TeamPos pos ,bool isEnemy)
    {
        if (pos == TeamPos.Front)
        {
            if (!isEnemy)
                heros.front = chara;
            else
                enemies.front = chara;
        }else if (pos == TeamPos.Middle)
        {
            if (!isEnemy)
                heros.middle = chara;
            else
                enemies.middle = chara;
        }
        else if (pos == TeamPos.Back)
        {
            if (!isEnemy)
                heros.back = chara;
            else
                enemies.back = chara;
        }
    }


    void GameOver(bool win)
    {
        isWorking = false;
        string content = "";
        if (win)
        {
            content = "Win";
            GameData.instance.battleResult = true;
        }
        else
        {
            content = "lose";
            GameData.instance.battleResult = true;
        }

        WriteCharToGameData(heros.front);
        WriteCharToGameData(heros.middle);
        WriteCharToGameData(heros.back);

        gameOverContorl.Open(content);
    }
    void WriteCharToGameData(CharData chara)
    {
        if (chara.characterDataPack == null) return;
        chara.characterDataPack.currentHealth = chara.health;
        if (chara.characterDataPack.currentHealth <= 0)
            chara.characterDataPack.currentHealth = 1;
        chara.characterDataPack.skillCD1 = chara.skillControl.skill2.currentCoolDown;
        chara.characterDataPack.skillCD2 = chara.skillControl.skill3.currentCoolDown;

    }
    public void TestWin()
    {
        GameOver(true);
    }
}
