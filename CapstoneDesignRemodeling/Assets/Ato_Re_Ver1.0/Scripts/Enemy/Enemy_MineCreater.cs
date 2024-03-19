using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_MineCreater : MonoBehaviour,
    IEnemyBasicAction
{


    [SerializeField]
    float moveTimer;
    [SerializeField]
    GameObject mine;


    Rigidbody rigid;
    Animator ani;
    float moveTime = 0;
    bool isMove = true;

    float setMineTimer = 1f;
    float setMineTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        rigid=GetComponent<Rigidbody>();
        ani=GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        OneDirectionMove();
        CreateMine();
    }
    void CreateMine()
    {
        if(isMove)
        {
            if (moveTime >= moveTimer)
            {
                isMove = false;
                setMineTime = 0;
                ani.SetTrigger("setMine");
            }
            else
            {
                moveTime += Time.deltaTime;
            }
        }
        else
        {
            setMineTime += Time.deltaTime;
            if (setMineTime >= setMineTimer)
            {
                GetComponent<IEnemyBasicAction>().InstinateEnemyObject(gameObject, mine);
                moveTime = 0;
                isMove = true;
            }
        }
        
    }
    void OneDirectionMove()
    {
        if (isMove)
        {
            GetComponent<IEnemyBasicAction>().OneDirectionMove(2, rigid, gameObject);
        }
        GetComponent<IEnemyBasicAction>().SetAni_Move(rigid, gameObject, ani);
    }

}
