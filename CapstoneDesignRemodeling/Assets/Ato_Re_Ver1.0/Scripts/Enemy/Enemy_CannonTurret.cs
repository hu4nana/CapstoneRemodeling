using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_CannonTurret : MonoBehaviour,
    IEnemyBasicAction
{
    [SerializeField]
    GameObject missile;
    [SerializeField]
    GameObject shootPos;

    bool isAttack = false;
    float attackTimer = 2f;
    float attackTime = 0;

    
    Rigidbody rigid;
    GameObject target;

    private void Start()
    {
        rigid=GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (target != null)
        {
            isAttack = true;
        }
        else
        {
            isAttack = false;
            attackTime = attackTimer;
        }
        InstantMissile();
    }

    void InstantMissile()
    {
        if(isAttack)
        {
            Vector3 direction = target.transform.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);

            // y축 회전값만 추출하여 적용합니다.
            float yRotation = targetRotation.eulerAngles.y;
            transform.rotation = Quaternion.Euler(0f, yRotation, 0f);

            attackTime -= Time.deltaTime;
            if (attackTime <= 0)
            {
                GetComponent<IEnemyBasicAction>().InstinateMissile(rigid, gameObject, target, missile,
            new Vector3(transform.position.x, transform.position.y, 0), 5);
                attackTime = attackTimer;
            }
        }
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 10)
        {
            target=other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 10)
        {
            target = null;
        }
    }
}
