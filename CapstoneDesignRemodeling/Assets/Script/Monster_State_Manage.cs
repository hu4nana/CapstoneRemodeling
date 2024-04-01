using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_State_Manage : MonoBehaviour, IDamageable
{
    private Renderer monsterRenderer;
    //private Renderer monster_Original_Renderer;
    //[SerializeField]
    private Material defaultMaterial;  // ������ �⺻ ��Ƽ����

    public Color original_Color;

    Animator ani;
    //[SerializeField]
    //public Material HitMaterial;  // ������ ��Ʈ��Ƽ����


    [SerializeField]
    protected Define.MonsterMoveType _M_M_Type;

    [SerializeField]
    ParticleSystem GroundType_Vfx;

    [SerializeField]
    ParticleSystem FlyType_Vfx;


    

    public Define.MonsterMoveType M_M_Type { get { return _M_M_Type; } set { _M_M_Type = value; } }


    [SerializeField]
    protected int _max_monsterHp;
    public int M_Hp { get { return _max_monsterHp; } set { _max_monsterHp = value; } }


    [SerializeField]
    protected int _monsterHp;
    public int Hp { get { return _monsterHp; } set { _monsterHp = value; } }

    [SerializeField]
    protected int _damage;
    public int Damage { get { return _damage; } set { _damage = value; } }

    [SerializeField]
    protected bool _isMonster; // �� �ڽ��� �����ΰ�?
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
        //M_Hp = 2500;
        Hp = M_Hp;
        IsMonster = true;
        monsterRenderer = GetComponentInChildren<Renderer>();
        ani = GetComponentInChildren<Animator>();
        //defaultMaterial = monsterRenderer.material;
        //defaultMaterial = GetComponentInChildren<Material>();
    }
    void Start()
    {
        Init();
    }



    private void OnTriggerEnter(Collider collision)
    {
        //if(collision.gameObject.tag=="Player")
        //Debug.Log($"�浹�� ������Ʈ �̸� : {collision.gameObject.name }");

        IDamageable damageable = collision.GetComponent<IDamageable>();


        if (damageable != null)
        {
            Debug.Log("���Ͱ� �浹�� �Ͽ���");
            Damaged(damageable, M_Type, damageable.A_Type, Hp, damageable.Damage, IsMonster, IsAttackObject);
        }

    }


    public void Damaged(IDamageable damageable, Define.MonsterType M_Type, Define.AttackType A_Type, int monster_hp, int Damage, bool isMonster, bool isAttackObject)
    {
        Debug.Log($" ���� ��ü ���� ����{isMonster}");
        Debug.Log($" ���� ��ü ���� Ÿ��{M_Type}");

        Debug.Log($" �浹 ��ü Damage : {Damage}");
        Debug.Log($" �浹 ��ü Ÿ�� : {A_Type}");


        if (isMonster)//������ ��� ������ ���� ����
        {
            ShowHitEffect();
            switch (M_Type)
            {
                case Define.MonsterType.Normal:
                    monster_hp -= Damage;
                    Hp = monster_hp;
                    Debug.Log($" ���� ��ü �븻 ������ ����  ���� ü�� : {Hp}");
                    break;
                case Define.MonsterType.Armor:
                    if (A_Type == Define.AttackType.Normal)
                    {
                        monster_hp -= (int)(Damage * 0.5f);
                        Hp = monster_hp;
                        Debug.Log($" ���� ��ü �븻������ ����  ���� ü�� : {Hp}");
                    }
                    else if (A_Type == Define.AttackType.Penetrate)
                    {
                        Hp = monster_hp - Damage * 2;
                        Debug.Log($" ���� ��ü ��������� ����  ���� ü�� : {Hp}");
                    }
                    break;
                case Define.MonsterType.Energy:
                    Hp = monster_hp - Damage;
                    if (isAttackObject)
                    {
                        Destroy(this.gameObject);
                    }
                    break;

            }

            Debug.Log($"���Ͱ� �������� �Ծ��� ����ü�� {Hp}");
        }
        else//�÷��̾��� ��� ������ ���� �� ����
        {
            Hp -= 1;
        }
        if (Hp <= 0 && IsAlive)
        {
            Dead();
            IsAlive = false;
        }
        //Debug.Log("���������޾Ҵ�");
    }

    public void ShowHitEffect()
    {
        StartCoroutine(HitEffectCoroutine());
    }
    IEnumerator HitEffectCoroutine()
    {
        //defaultMaterial.color = monsterRenderer.material.color;
        //original_Color = monsterRenderer.material.color;

        // ���������� ����
        //monsterRenderer.material = HitMaterial;
        //monsterRenderer.material.color = Color.white;

        //defaultMaterial

        original_Color = monsterRenderer.material.GetColor("_EmissionColor");
        monsterRenderer.material.SetColor("_EmissionColor", Color.white * 1.0f);

        //monsterRenderer.material

        // ��� ���
        yield return new WaitForSeconds(0.1f);  // ���÷� 0.5�� ���� ���������� ����

        // �⺻ �������� ����
        //monsterRenderer.material.color = Color.white;
        //monsterRenderer.material = defaultMaterial;
        //monsterRenderer.material.color = original_Color;

        monsterRenderer.material.SetColor("_EmissionColor", original_Color);
    }

    public void Dead()
    {
        ani.SetBool("isDead", true);
            //this.gameObject.GetComponent<BoxCollider>().enabled = false;
            //if (ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            //{
            //    play_Particle();
            //}
        Invoke("play_Particle", 2.0f);
            //Destroy(this.gameObject,0.5f);
    }
    void play_Particle()
    {
        switch (M_M_Type)
        {
            case Define.MonsterMoveType.Ground:
                if (GroundType_Vfx == null) { return; }
                GroundType_Vfx.Play();
                Debug.Log("���� ��� ��ƼŬ ���");
                Destroy(this.gameObject, 0.5f);
                break;
            case Define.MonsterMoveType.Fly:
                if (FlyType_Vfx == null) { return; }
                FlyType_Vfx.Play();
                Destroy(this.gameObject, 0.5f);
                break;
        }
    }

}
