using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstTest : MonoBehaviour
{
    public GameObject character1;
    public GameObject character2;
    public GameObject character3;

    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;

    public Status s;

    int ddd = 15;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start");

        CharData data = new CharData();
        data.charShowName = "理事";
        data.maxHealth = 64;
        data.health = 52;
        data.actionPoint = 1;
        data.maxActionPoint = 3;
        data.physicalDamage = 35;
        data.agility = 23;
        data.charView = character1.GetComponent<CharView>();
        character1.GetComponent<CharView>().character = data;
        //character1.GetComponent<CharView>().charViewUI.character = data;
        FieldManager.instance.heros.front = data;
        data.skill1 = new testSkill.NormalAttack("x", 1, 1, 15).SetChar(data);
        data.skill2 = new testSkill.AttackSelf("x", 2, 2, 12).SetChar(data);


        data = new CharData();
        data.charShowName = "麻耶";
        data.maxHealth = 100;
        data.health = 35;
        data.actionPoint = 2;
        data.maxActionPoint = 4;
        data.agility = 12;
        data.charView = character2.GetComponent<CharView>();
        character2.GetComponent<CharView>().character = data;
        //character2.GetComponent<CharView>().charViewUI.character = data;
        FieldManager.instance.heros.middle = data;
        data.skill1 = new testSkill.NormalAttack("x", 1, 0, 15).SetChar(data);
        data.skill2 = new testSkill.JustTest("x", 2, 2).SetChar(data);

        data = new CharData();
        data.charShowName = "惠";
        data.maxHealth = 75;
        data.health = 35;
        data.actionPoint = 2;
        data.maxActionPoint = 3;
        data.agility = 35;
        data.charView = character3.GetComponent<CharView>();
        character3.GetComponent<CharView>().character = data;
        //character3.GetComponent<CharView>().charViewUI.character = data;
        FieldManager.instance.heros.back = data;
        data.skill1 = new testSkill.NormalAttack("x", 1, 0, 15).SetChar(data);
        data.skill2 = new testSkill.AttackSelf("x", 2, 2, 12).SetChar(data);
        data.skill3 = new testSkill.JustTest("x", 2, 2).SetChar(data);

        data = new CharData();
        data.charShowName = "敵人1";
        data.isEnemy = true;
        data.maxHealth = 80;
        data.health = 75;
        data.actionPoint = 2;
        data.maxActionPoint = 4;
        data.agility = 21;
        data.skill3 = new testSkill.JustTest("x", 1, 0).SetChar(data);
        data.charView = enemy1.GetComponent<CharView>();
        enemy1.GetComponent<CharView>().character = data;
        FieldManager.instance.enemies.front = data;
        data = new CharData();
        data.charShowName = "敵人2";
        data.isEnemy = true;
        data.maxHealth = 100;
        data.health = 75;
        data.actionPoint = 2;
        data.maxActionPoint = 4;
        data.agility = 25;
        data.skill2 = new testSkill.AttackSelf("x", 2, 2, 30).SetChar(data);
        data.charView = enemy2.GetComponent<CharView>();
        enemy2.GetComponent<CharView>().character = data;
        FieldManager.instance.enemies.middle = data;
        data = new CharData();
        data.charShowName = "敵人3";
        data.isEnemy = true;
        data.maxHealth = 50;
        data.health = 30;
        data.actionPoint = 2;
        data.maxActionPoint = 4;
        data.agility = 16;
        data.charView = enemy3.GetComponent<CharView>();
        enemy3.GetComponent<CharView>().character = data;
        FieldManager.instance.enemies.back = data;

        FieldManager.instance.charViewUIControl.OpenCharViewUI(enemy1.GetComponent<CharView>());
        FieldManager.instance.charViewUIControl.OpenCharViewUI(enemy2.GetComponent<CharView>());
        FieldManager.instance.charViewUIControl.OpenCharViewUI(enemy3.GetComponent<CharView>());



        FieldManager.instance.charViewUIControl.OpenCharViewUI(character1.GetComponent<CharView>());
        FieldManager.instance.charViewUIControl.OpenCharViewUI(character2.GetComponent<CharView>());
        FieldManager.instance.charViewUIControl.OpenCharViewUI(character3.GetComponent<CharView>());
        //s = new TestStatus();
        //s.Enter();


    }
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("now is his turn : " + FieldManager.instance.charLineControl.Next().charShowName);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            FieldManager.instance.DamageChar(character1.GetComponent<CharView>().character, ddd, DamageType.Magic);
            ddd++;
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            FieldManager.instance.battleTextControl.ShowDamageText(character1.GetComponent<CharView>().character, 64, TextType.Damage);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            FieldManager.instance.CharTurnStart(character1.GetComponent<CharView>().character);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            FieldManager.instance.CharTurnStart(character2.GetComponent<CharView>().character);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            FieldManager.instance.CharTurnStart(character3.GetComponent<CharView>().character);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            FieldManager.instance.CharTurnEnd();
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