using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IPlayerBasicAction
{

    [SerializeField]
    float speed;

    Rigidbody rigid;
    Animator ani;

    public GameObject Gun;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Attack();
        Crouch();
    }
    void Move()
    {
        GetComponent<IPlayerBasicAction>().Move(speed, rigid, gameObject, ani);
    }
    void Attack()
    {
        GetComponent<IPlayerBasicAction>().Attack(3, rigid, gameObject, ani);
    }
    void Crouch()
    {
        GetComponent<IPlayerBasicAction>().Crouch(ani);
    }

    void Shoot()
    {
        Gun.GetComponent<MP_2158>().Gun_Shoot_Bullet(transform);
    }
}