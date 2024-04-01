using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMine : MonoBehaviour,
    IEnemyBasicAction
{
    private Monster_State_Manage _monster_State_Manage;

    public Monster_State_Manage monster_State_Manage
    {
        get { return _monster_State_Manage; }
        set { _monster_State_Manage = value; }
    }

    [SerializeField]
    GameObject explosion;

    bool isAcive = false;

    float timer = 0.4f;

    private void Update()
    {
        if (isAcive)
        {
            timer-=Time.deltaTime;
            if (timer < 0)
            {
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10)
        {
            GetComponent<IEnemyBasicAction>().InstinateEnemyObject(gameObject, explosion);
            isAcive = true;
        }
    }
}
