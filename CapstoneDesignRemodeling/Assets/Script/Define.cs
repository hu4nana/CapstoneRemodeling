using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//public enum CoreType
//{
//    Magenta,
//    Yellow,
//    Saian
//}

public class Define
{
    public enum State
    {
        Idle,
        Die,
        Moving,
        Jumping,
        Falling,
        NormalAttack,
        Special_Skill_A,
        Special_Skill_B,
        Special_Skill_C,
        PrepareAttack,
        AfterAttackDelay
    }
    public enum MonsterType
    {
        Normal,
        Armor,
        Energy,
    }
    public enum AttackType
    {
        Normal,
        Penetrate,
        Energy,
    }
}