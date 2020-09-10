using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstTest : MonoBehaviour
{
    public GameObject character1;
    public GameObject character2;
    public GameObject character3;
    public Status s;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start");

        CharData data = new CharData();
        data.charShowName = "理事";
        data.charView = character1.GetComponent<CharView>();
        character1.GetComponent<CharView>().character = data;
        FieldManager.instance.heros.front = data;

        data = new CharData();
        data.charShowName = "麻耶";
        data.charView = character2.GetComponent<CharView>();
        character2.GetComponent<CharView>().character = data;
        FieldManager.instance.heros.middle = data;

        data = new CharData();
        data.charShowName = "惠";
        data.charView = character3.GetComponent<CharView>();
        character3.GetComponent<CharView>().character = data;
        FieldManager.instance.heros.back = data;


        //s = new TestStatus();
        //s.Enter();


    }
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            FieldManager.instance.DamageChar(character1.GetComponent<CharView>().character, 50, DamageType.Magic);
        }
    }
}

public class TestStatus : CharStatus
{
    DamageInfo dif;
    public override void Enter()
    {
        dif = SetSubscription<DamageInfo>(TriggerType.DamageTotalCheck, 1);
        isWorking = true;
    }
    public override void Trigger1()
    {
        dif.damageValue/=2;
        Debug.Log("TRIGGER DAMAGE :" + dif.damageValue + " To " + dif.damagedChar);
    }
    public override string GetDescription()
    {
        throw new System.NotImplementedException();
    }

    public override void Update()
    {
        throw new System.NotImplementedException();
    }
}
public class TestStatus2 : CharStatus
{
    DamageInfo dif;
    public override void Enter()
    {
        dif = SetSubscription<DamageInfo>(TriggerType.DamageCheck, 1);
        isWorking = true;
    }
    public override void Trigger1()
    {
        dif.damageValue -= 10;
    }
    public override string GetDescription()
    {
        throw new System.NotImplementedException();
    }

    public override void Update()
    {
        throw new System.NotImplementedException();
    }
}