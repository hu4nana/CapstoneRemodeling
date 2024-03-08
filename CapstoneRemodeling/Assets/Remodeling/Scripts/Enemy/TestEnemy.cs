using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TestEnemy : MonoBehaviour, IEnemyBasicAction
{

    Rigidbody rigid;

    // Start is called before the first frame update
    void Start()
    {
        rigid= GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        OneDirectionMove();
    }
    void OneDirectionMove()
    {
        GetComponent<IEnemyBasicAction>().OneDirectionMove(3,rigid,gameObject,0);
    }
}
