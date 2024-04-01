using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TestEnemy : MonoBehaviour, IEnemyBasicAction
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

    int bulletCount = 8;
    int curCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        rigid= GetComponent<Rigidbody>();
        bulletTime = bulletTimer;
    }

    // Update is called once per frame
    void Update()
    {
        //RaycastHit hit;
        //if (Physics.Raycast(transform.position,
        //    Vector3.right * (transform.rotation.y / Mathf.Abs(transform.rotation.y))
        //    , out hit, 5))
        //{
        //    if (hit.collider.gameObject.layer == 10)
        //    {
        //        target = hit.collider.gameObject;

        //    }
        //    if (hit.collider.gameObject.layer != 10)
        //    {
        //        target = null;
        //    }
        //}

        //GetComponent<IEnemyBasicAction>().Charge(target, gameObject, rigid, 3);


        /*Case1*/
        InstinateBullet();
        OneDirectionMove();
        /*Case1*/


        //ChargeToTarget();


        /*Case2*/
        //ChargeToTargetFromAir();
        /*Case2*/
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
        GetComponent<IEnemyBasicAction>().OneDirectionMove(3,rigid,gameObject);
    }
    void ChargeToTarget()
    {
        GetComponent<IEnemyBasicAction>().ChargeToTarget(1,gameObject, rigid, 6);
    }

    private void OnTriggerEnter(Collider other)
    {
    }
}
