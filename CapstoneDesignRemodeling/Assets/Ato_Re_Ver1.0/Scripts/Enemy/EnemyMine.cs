using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMine : MonoBehaviour,
    IEnemyBasicAction
{
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
