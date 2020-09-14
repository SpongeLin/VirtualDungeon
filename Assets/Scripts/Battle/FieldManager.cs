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

    public CharData currentActionCharacter;

    //public List<CharData> heros;
    //public List<CharData> enemies;


    public CharTeam heros;
    public CharTeam enemies;

    private void Awake()
    {
        instance = this;
        heros = new CharTeam();
        enemies = new CharTeam();

        charLineControl = new CharLineControl();

    }
    private void Start()
    {
        isWorking = true;
        OrderManager.instance.isWaiting = false;

        GameStart();//TEST!
    }




    public void GameStart()
    {
        isWorking = true;

        //character create!!

        charLineControl.LoadAllCharacters();
        CharTurnStart(charLineControl.Next());
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
        charViewUIControl.ShowCurrentTurnMark(currentActionCharacter, false);
        currentActionCharacter = null;
        if (!OrderManager.instance.IsEmptyStack())
            Debug.LogWarning("Can't be any Order exist while End Turn!!!!");

        CharTurnStart(charLineControl.Next());
    }


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



}
