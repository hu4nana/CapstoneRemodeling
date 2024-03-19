using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_FlameTanker : MonoBehaviour,
    IEnemyBasicAction
{
    [SerializeField]
    float moveSpeed;
    [SerializeField]
    float detectDistance;
    [SerializeField]
    float maxMoveDistance;
    [SerializeField]
    int direction;
    [SerializeField]
    GameObject lFlameAttack;
    [SerializeField]
    GameObject rFlameAttack;


    GameObject target;
    Rigidbody rigid;
    Animator ani;
    Vector3 startPos;
    
    // Start is called before the first frame update
    void Start()
    {
        rigid=GetComponent<Rigidbody>();
        ani=GetComponent<Animator>();
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        target = GetComponent<IEnemyBasicAction>().DetectTarget(gameObject, target, detectDistance);
        BeOnGuard();
        FlameAttack();
        ani.SetInteger("velocityX",(int)rigid.velocity.x*direction);
    }
    void BeOnGuard()
    {
        GetComponent<IEnemyBasicAction>().BeOnGuard(rigid, gameObject, target, startPos, maxMoveDistance, moveSpeed);
    }
    void FlameAttack()
    {
        if (target)
        {
            lFlameAttack.SetActive(true);
            rFlameAttack.SetActive(true);
        }
        else
        {
            lFlameAttack.SetActive(false);
            rFlameAttack.SetActive(false);
        }
    }
}
