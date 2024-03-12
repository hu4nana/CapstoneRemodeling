using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TestEnemy : MonoBehaviour, IEnemyBasicAction
{

    Rigidbody rigid;
    GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        rigid= GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position,
            Vector3.right * (transform.rotation.y / Mathf.Abs(transform.rotation.y))
            , out hit, 5))
        {
            if (hit.collider.gameObject.layer == 10)
            {
                target = hit.collider.gameObject;

            }
            if (hit.collider.gameObject.layer != 10)
            {
                target = null;
            }
        }

        GetComponent<IEnemyBasicAction>().Charge(target, gameObject, rigid, 3);
        //OneDirectionMove();
        //ChargeToTarget();
    }

    // One Direction Move_LIke Goomba
    void OneDirectionMove()
    {
        GetComponent<IEnemyBasicAction>().OneDirectionMove(3,rigid,gameObject,0);
    }
    void ChargeToTarget()
    {
        GetComponent<IEnemyBasicAction>().ChargeToTarget(1,gameObject, rigid, 6);
    }
}
