using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerController : MonoBehaviour,
    IPlayerBasicAction,
    IPlayerSpecialAction
{
   [SerializeField]
    float speed;

    Rigidbody rigid;
    Animator ani;


    bool isHook = false;
    Vector3 hookDistance;
    Vector3 hookEndPoint;
    float hookTimer = 0.2f;

    public GameObject Gun;

    GameObject hookPoint;
    // Start is called before the first frame update
    void Start()
    {
        
        rigid = GetComponent<Rigidbody>();
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetKey(KeyCode.A))
        //{
        //    hookDistance = (hookPoint.transform.position - transform.position);
        //    transform.Translate(hookDistance.normalized * 1.4f);
        //}
        
        Move();
        Attack();
        Crouch();
        Hook();
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

    void Hook()
    {
        if (hookPoint != null && Input.GetKeyDown(KeyCode.V))
        {
            hookDistance = (hookPoint.transform.position - transform.position);
            hookEndPoint = hookDistance.normalized * 2;
            isHook = true;
            hookTimer = 0.2f;
        }
        if (isHook)
        {
            GetComponent<IPlayerSpecialAction>().GrapplingHook(gameObject, hookPoint, rigid, hookEndPoint);
            //hookTimer -= Time.deltaTime;
            //if (hookTimer <= 0)
            //{
            //    isHook = false;
            //}


            float stoppingDistance = 0.5f;
            Debug.Log((transform.position-hookEndPoint).magnitude);
            if ((hookEndPoint-transform.position).magnitude < stoppingDistance)
            {
                // 도착 지점에 도달했을 때의 동작
                Debug.Log("도착 지점에 도달했습니다.");

                // 이동을 멈추기 위해 속도를 0으로 설정
                isHook = false;
                rigid.velocity = Vector3.zero;
            }
        }
        else
        {
            rigid.useGravity = true;
        }
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Hook")
        {
            hookPoint = other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Hook")
        {
            hookPoint = null;
        }
    }
    void Shoot()
    {
        Gun.GetComponent<MP_2158>().Gun_Shoot_Bullet(transform);
    }
}