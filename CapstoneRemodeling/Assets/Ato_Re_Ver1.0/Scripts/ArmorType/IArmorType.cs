using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ArmorType
{
    LightArmor,
    HeavyArmor,
    EnergyArmor
}
public interface IArmorType
{
    ArmorType armorType { get; set; }
    public void GetDamaged(Collision collision,int hp)
    {
        int attackType = (int)collision.gameObject.GetComponent<IAttackType>().attackType;
        int attackDamage = collision.gameObject.GetComponent<IAttackType>().attackDamage;

        switch (attackType)
        {
            case 0:
                if ((int)armorType == 1)
                {
                    Debug.Log(" 공격 : Impact || 방어구 : " + armorType.ToString());
                    hp-=attackDamage/2;
                }
                else
                {
                    Debug.Log(" 공격 : Impact");
                    hp -= attackDamage;
                }
                break;
            case 1:
                if ((int)armorType == 1)
                {
                    Debug.Log(" 공격 : Pierce || 방어구 : " + armorType.ToString());
                    hp -= attackDamage * 2;
                }
                else
                {
                    Debug.Log(" 공격 : Pierce ");
                    hp -= attackDamage;
                }
                break;
            case 2:
                Debug.Log(" 공격 : Energy ");
                hp -= attackDamage;
                break;
            case 3:
                Debug.Log(" 공격 : Explosive ");
                hp -= attackDamage;
                break;
        }
    }
    public void LightArmor()
    {

    }
    public void HeavyArmor()
    {

    }
    public void EnergyArmor()
    {

    }
}
