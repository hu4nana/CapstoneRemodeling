using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_BulletSpreader : MonoBehaviour,
    IEnemyBasicAction
{
    private Monster_State_Manage _monster_State_Manage;

    public Monster_State_Manage monster_State_Manage
    {
        get { return _monster_State_Manage; }
        set { _monster_State_Manage = value; }
    }

    Rigidbody rigid;
    GameObject target;

    [SerializeField]
    GameObject bullet;

    float bulletTimer = 2;
    float bulletTime;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        bulletTime = bulletTimer;
    }

    // Update is called once per frame
    void Update()
    {
        InstinateBullet();
        OneDirectionMove();
    }

    void InstinateBullet()
    {
        bulletTime -= Time.deltaTime;
        if (bulletTime < 0)
        {
            GetComponent<IEnemyBasicAction>().Instinate8DirectionBullet(rigid, gameObject, bullet);
            bulletTime = bulletTimer;
        }
    }
    void OneDirectionMove()
    {
        GetComponent<IEnemyBasicAction>().OneDirectionMove(3, rigid, gameObject);
    }
}
