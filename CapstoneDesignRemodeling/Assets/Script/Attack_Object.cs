using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Object : MonoBehaviour, IDamageable
{
    //[SerializeField]
    //public IDamageable ParentsData;

    //[SerializeField]
    //public GameObject ParentsData_GameObject;
    
    
    //[SerializeField]
    //public GameObject ParentsData;

    

    protected int _max_monsterHp;

    
    public int M_Hp { get { return _max_monsterHp; } set { _max_monsterHp = value; } }


    protected int _monsterHp;
    public int Hp { get { return _monsterHp; } set { _monsterHp = value; } }


    [SerializeField]
    protected int _damage;
    public int Damage { get { return _damage; } set { _damage = value; } }

    [SerializeField]
    protected bool _isMonster;
    public bool IsMonster { get { return _isMonster; } set { _isMonster = true; } }

    protected bool _isAlive;
    public bool IsAlive { get { return _isAlive; } set { _isAlive = value; } }
    
    [SerializeField]
    protected bool _isAttackObject;
    
    public bool IsAttackObject { get { return _isAttackObject; } set { _isAttackObject = true; } }


    [SerializeField]
    protected Define.MonsterType _M_Type;


    public Define.MonsterType M_Type { get { return _M_Type; } set { _M_Type = value; } }


    [SerializeField]
    protected Define.AttackType _A_Type;
    public Define.AttackType A_Type { get { return _A_Type; } set { _A_Type = value; } }




    void Init() //�θ�� ���� �������� �Ӽ��� ���� ���� �޾ƿ�.
    {
        //ParentsData = ParentsData_GameObject.GetComponent<IDamageable>();

        //M_Hp = ParentsData.M_Hp;
        //Hp = ParentsData.Hp;
        //hp m_hp�� �޾ƿ� �ʿ�� ������ �׳� �޾ƿ�


        //Damage = ParentsData.Damage;
        //IsMonster = ParentsData.IsMonster;
        //IsAttackObject = true;
        //M_Type = ParentsData.M_Type;
        //A_Type = ParentsData.A_Type;

    }

    void Start()
    {
        //Init();
    }

    void Damaged()//����ü �� ���� ��ü�� �������� �Դ� ���� �ʿ� X
    {

    }

    void isEnergyType(IDamageable damageable , Define.MonsterType M_Type)
    {
        if (damageable.A_Type == Define.AttackType.Energy && M_Type == Define.MonsterType.Energy)
        {
            Destroy(gameObject);
        }
    }


    void OnTriggerEnter(Collider collision)
    {
        //if(collision.gameObject.tag=="Player")
        //Debug.Log($"�浹�� ������Ʈ �̸� : {collision.gameObject.name }");

        IDamageable damageable = collision.GetComponent<IDamageable>();

        if (damageable != null)
        {
            isEnergyType(damageable,this.M_Type);
        }
    }

}
