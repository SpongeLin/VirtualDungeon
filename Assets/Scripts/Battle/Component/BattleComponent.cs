using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharTeam
{
    public CharData front;
    public CharData middle;
    public CharData back;
}

public enum TriggerType
{
    DamageCheck,
    DamageTotalCheck,
    DamageFinalCheck,
    DamageAfter,
    DamageBefore,
    UseCardCheck,
    UseCardAfter,
    UseCardBefore,
    TurnStarting,
    TurnStartBefore,
    TurnEnding,
    TurnEndBefore,

    UseSkillCheck,
    UseSkillAfter,
    UseSkillBefore,

}

public enum SkillTarget
{
    Self,
    All,
    Allies,
    Enemies,
    FrontEnemy,
}

public enum DamageType
{
    Null,
    Physical,
    Magic,
    True
}