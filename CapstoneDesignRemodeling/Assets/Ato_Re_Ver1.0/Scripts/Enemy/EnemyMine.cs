using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMine : MonoBehaviour,
    IEnemyBasicAction
{
    [SerializeField]
    GameObject explosion;

    bool isAcive = false;

    float timer = 1f;

    float boomTimer = 0.75f;
    int boomCount=1;
    private void Update()
    {
        if (isAcive)
        {
            timer-=Time.deltaTime;
            boomTimer-=Time.deltaTime;
            if(boomCount==1&&boomTimer<=0)
            {
                GetComponent<IEnemyBasicAction>().InstinateEnemyObject(gameObject, explosion);
                boomCount = 0;
            }
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
            isAcive = true;
        }
    }
}
