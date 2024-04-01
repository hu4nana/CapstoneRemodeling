using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser : MonoBehaviour,
    IEnemyBasicAction
{
    private Monster_State_Manage _monster_State_Manage;

    public Monster_State_Manage monster_State_Manage
    {
        get { return _monster_State_Manage; }
        set { _monster_State_Manage = value; }
    }

    GameObject target = null;
    Rigidbody rigid;
    Animator ani;
    bool isActivated=false;
    // Start is called before the first frame update
    void Start()
    {
        ani= GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isActivated)
        {
            ani.SetBool("isActivated", true);
            GetComponent<IEnemyBasicAction>().ChaseTheTargetFromAir(0.4f, gameObject, target, rigid);

        }
        else
        {
            ani.SetBool("isActivated", false);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 10)
        {
            target=other.gameObject;
            isActivated=true;
        }
    }
}
