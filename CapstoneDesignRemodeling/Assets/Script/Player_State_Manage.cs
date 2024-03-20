using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_State_Manage : MonoBehaviour, IDamageable
{
    [SerializeField]
    protected int _max_playerHp;
    public int M_Hp { get { return _max_playerHp; } set { _max_playerHp = value; } }


    [SerializeField]
    protected int _playerHp;
    public int Hp { get { return _playerHp; } set { _playerHp = value; } }

    [SerializeField]
    protected int _damage;
    public int Damage { get { return _damage; } set { _damage = value; } }

    [SerializeField]
    protected bool _isMonster;
    public bool IsMonster { get { return _isMonster; } set { _isMonster = true; } }


    [SerializeField]
    protected bool _isAlive;
    public bool IsAlive { get { return _isAlive; } set { _isAlive = value; } }


    [SerializeField]
    protected bool _isAttackObject;
    public bool IsAttackObject { get { return _isAttackObject; } set { _isAttackObject = false; } }


    [SerializeField]
    protected Define.MonsterType _M_Type;


    public Define.MonsterType M_Type { get { return _M_Type; } set { _M_Type = value; } }


    [SerializeField]
    protected Define.AttackType _A_Type;
    public Define.AttackType A_Type { get { return _A_Type; } set { _A_Type = value; } }




    void Init()
    {
        IsAlive = true;
        M_Hp = 2500;
        Hp = M_Hp;
        IsMonster = true;
    }


    private void OnTriggerEnter(Collider collision)
    {
        //if(collision.gameObject.tag=="Player")
        //Debug.Log($"충돌한 오브젝트 이름 : {collision.gameObject.name }");

        IDamageable damageable = collision.GetComponent<IDamageable>();


        if (damageable != null)
        {
            Debug.Log("몬스터가 충돌을 하였음");
            Damaged(damageable, M_Type, damageable.A_Type, Hp, damageable.Damage, IsMonster, IsAttackObject);
        }

    }


    public void Damaged(IDamageable damageable, Define.MonsterType M_Type, Define.AttackType A_Type, int monster_hp, int Damage, bool isMonster, bool isAttackObject)
    {
        Debug.Log($" 현재 객체 몬스터 여부{isMonster}");
        Debug.Log($" 현재 객체 몬스터 타입{M_Type}");

        Debug.Log($" 충돌 객체 Damage : {Damage}");
        Debug.Log($" 충돌 객체 타입 : {A_Type}");


        if (isMonster)//몬스터일 경우 데미지 공식 적용
        {
        }
        else//플레이어일 경우 데미지 공식 미 적용
        {
            Hp -= 1;
            //Debug.Log($"플레이어가 데미지를 입었음 남은체력 {Hp}");
        }
        if (Hp <= 0)
        {
            IsAlive = false;
        }
        //Debug.Log("데미지를받았다");
    }

}
