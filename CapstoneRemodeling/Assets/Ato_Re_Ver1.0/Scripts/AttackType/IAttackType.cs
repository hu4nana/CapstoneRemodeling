using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackType
{
    Impact,
    Pierce,
    Energy,
    Explosive
}

public interface IAttackType
{
    AttackType attackType { get;set; }
    public int attackDamage { get; set; }
    public int ImpactAttack(int damage)
    {
        return damage;
    }
    public int PierceAttack(int damage)
    {
        return damage;
    }
    public int EnergyAttack(int damage)
    {
        return damage;
    }
    public int ExplosiveAttack(int damage)
    {
        return damage;
    }
}
