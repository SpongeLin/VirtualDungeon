using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : MonoBehaviour
{
    public static FieldManager instance { get; private set; }

    public bool playerTurn;
    public bool isWorking = false;

    //public List<CharData> heros;
    //public List<CharData> enemies;


    public CharTeam heros;
    public CharTeam enemies;

    private void Awake()
    {
        instance = this;
        heros = new CharTeam();
        enemies = new CharTeam();

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
