using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    //protected int hp;
    public int Hp { get; set; } // 체력 값
    public int Damage { get; set; } // 데미지 값
    public bool IsMonster { get; set; }//이 스크립트가 들어가 있는 오브젝트가 몬스터인지 체크
    public bool IsAlive { get; set; } // 생사여부 체크
    public int M_Hp { get; set; } // 최대 체력

    public bool IsAttackObject { get; set; } //지금 이 오브젝트가 공격 물체인지 체크 

    Define.MonsterType M_Type { get; set; }

    Define.AttackType A_Type { get; set; }

    void Init()
    {
        IsAlive = true;
    }

    void Damaged(IDamageable damageable, Define.MonsterType M_Type, Define.AttackType A_Type, int Hp, int Damage, bool isMonster, bool isAttackObject)
    {
        if (isMonster)//몬스터일 경우 데미지 공식 적용
        {
            switch (M_Type)
            {
                case Define.MonsterType.Normal:
                    Hp -= Damage;
                    break;
                case Define.MonsterType.Armor:
                    if (A_Type == Define.AttackType.Normal)          { Hp -= (int)(Damage * 0.5f);  }
                    else if (A_Type == Define.AttackType.Penetrate)  { Hp -= Damage * 2; }
                    break;
                case Define.MonsterType.Energy:
                    Hp -= Damage;
                    if (isAttackObject)//
                    {
                        //Destroy();
                        //삭제 명령
                    }
                    break;
            }
        }
        else//플레이어일 경우 데미지 공식 미 적용
        {
            Hp -= 1;
        }
        if (Hp <= 0)
        {
            IsAlive = false;
        }
        Debug.Log("데미지를받았다");
    }

    void OnTriggerEnter(Collider collision)
    {
        //if(collision.gameObject.tag=="Player")
        //Debug.Log($"충돌한 오브젝트 이름 : {collision.gameObject.name }");

        IDamageable damageable = collision.GetComponent<IDamageable>();

        if (damageable != null)
        {
            Damaged(damageable, damageable.M_Type, damageable.A_Type,Hp,Damage,IsMonster, IsAttackObject);
        }
    }
}
