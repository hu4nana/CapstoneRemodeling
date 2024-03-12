using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    Rigidbody rigid;
    [SerializeField]
    float lifeTime;
    // Start is called before the first frame update
    void Start()
    {
        rigid=GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime-=Time.deltaTime;
        if (lifeTime < 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
