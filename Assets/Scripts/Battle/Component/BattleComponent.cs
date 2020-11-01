using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharTeam
{
    public CharData front;
    public CharData middle;
    public CharData back;
    public CharData GetChar(TeamPos pos)
    {
        if (pos == TeamPos.Front)
            return front;
        if (pos == TeamPos.Middle)
            return middle;
        if (pos == TeamPos.Back)
            return back;
        return null;
    }
}

public enum CardPos
{
    Hand,
    Deck,
    Cemetery,
    Banish,
    Null
}

public enum TeamPos
{
    Front,
    Middle,
    Back,
    Null
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

    DealthBefore,
    DealthAfter,

    GainMagic,
    CardBurst,
    CardMove,
    Draw

}

public enum SkillTarget
{
    Self,
    All,
    Allies,
    Enemies,
    FrontEnemy,
}
public enum CardTarget
{
    All,
    Allies,
    Enemies,
    CardSelf
}

public enum DamageType
{
    Null,
    Normal,

}