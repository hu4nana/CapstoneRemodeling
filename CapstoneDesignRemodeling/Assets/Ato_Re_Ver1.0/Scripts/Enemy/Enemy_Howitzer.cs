using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Howitzer : MonoBehaviour,
    IEnemyBasicAction
{
    private Monster_State_Manage _monster_State_Manage;

    public Monster_State_Manage monster_State_Manage
    {
        get { return _monster_State_Manage; }
        set { _monster_State_Manage = value; }
    }


    [SerializeField]
    GameObject missile;

    GameObject target;
    Rigidbody rigid;
    Animator ani;
    float moveSpeed = 3;
    bool isMove = true;

    float shootMissileTimer = 2f;
    float shootMissileTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        OneDirectionMove();
        DetectTarget();
        //CreateMissile();
    }

    void DetectTarget()
    {
        target = GetComponent<IEnemyBasicAction>().DetectTarget(gameObject, target, 3);
        if (target == null)
        {
            isMove = true;
            ani.SetBool("isMove", true);
            ani.SetBool("isAttack", false);
        }
        else
        {
            isMove= false;
            ani.SetBool("isMove", false);
            ani.SetBool("isAttack", true);
            rigid.velocity = new Vector3(0,rigid.velocity.y,0);
        }
    }
    void CreateMissile()
    {
        GetComponent<IEnemyBasicAction>().InstinateMissile(rigid, gameObject, target, missile,
                    new Vector3(transform.position.x, transform.position.y + 2f, 0), 5);
    }
    void OneDirectionMove()
    {
        GetComponent<IEnemyBasicAction>().OneDirectionMove(3, rigid, gameObject);
        //GetComponent<IEnemyBasicAction>().SetAni_Move(rigid, gameObject, ani);
    }
}
