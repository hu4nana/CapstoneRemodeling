using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    //protected int hp;
    public int Hp { get; set; } // ü�� ��
    public int Damage { get; set; } // ������ ��
    public bool IsMonster { get; set; }//�� ��ũ��Ʈ�� �� �ִ� ������Ʈ�� �������� üũ
    public bool IsAlive { get; set; } // ���翩�� üũ
    public int M_Hp { get; set; } // �ִ� ü��

    public bool IsAttackObject { get; set; } //���� �� ������Ʈ�� ���� ��ü���� üũ 

    Define.MonsterType M_Type { get; set; }

    Define.AttackType A_Type { get; set; }

    void Init()
    {
        IsAlive = true;
    }

    void Damaged(IDamageable damageable, Define.MonsterType M_Type, Define.AttackType A_Type, int Hp, int Damage, bool isMonster, bool isAttackObject)
    {
        if (isMonster)//������ ��� ������ ���� ����
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
                        //���� ���
                    }
                    break;
            }
        }
        else//�÷��̾��� ��� ������ ���� �� ����
        {
            Hp -= 1;
        }
        if (Hp <= 0)
        {
            IsAlive = false;
        }
        Debug.Log("���������޾Ҵ�");
    }

    void OnTriggerEnter(Collider collision)
    {
        //if(collision.gameObject.tag=="Player")
        //Debug.Log($"�浹�� ������Ʈ �̸� : {collision.gameObject.name }");

        IDamageable damageable = collision.GetComponent<IDamageable>();

        if (damageable != null)
        {
            Damaged(damageable, damageable.M_Type, damageable.A_Type,Hp,Damage,IsMonster, IsAttackObject);
        }
    }
}
