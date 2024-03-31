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
    [SerializeField]
    float maxFallSpeed;

    Rigidbody rigid;
    //CapsuleCollider col;
    BoxCollider col;
    Animator ani;
    Renderer[] renderers;
    Color originalColor;


    bool isDamaged = false;
    bool isHook = false;
    bool isHookHolder = false;
    Vector3 hookEndPoint;
    float hookTimer = 0.2f;
    [SerializeField]
    int moveType;

    public GameObject Gun;

    GameObject hookPoint;


    public int MoveType
    {
        get { return moveType; }
        set { moveType = value; }
    }
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        //col = GetComponent<CapsuleCollider>();
        col=GetComponent<BoxCollider>();
        ani = GetComponent<Animator>();
        renderers = GetComponentsInChildren<Renderer>();
        originalColor = renderers[0].material.color;
    }

    // Update is called once per frame
    void Update()
    {
        if(rigid.velocity.y<=maxFallSpeed)
        {
            rigid.velocity = new Vector3(rigid.velocity.x, maxFallSpeed, 0);
        }
        if (rigid.velocity.y >= 30)
        {
            rigid.velocity = new Vector3(rigid.velocity.x, 30, 0);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            ani.SetTrigger("isDamaged");
        }
        Attack();
        Crouch();
        Jump();
        Hook();
    }
    private void FixedUpdate()
    {
        Move();
    }
    void Move()
    {
        GetComponent<IPlayerBasicAction>().Move(speed, rigid, gameObject, ani,moveType);
    }
    void Attack()
    {
        GetComponent<IPlayerBasicAction>().Attack(3, rigid, gameObject, ani);
    }
    void Crouch()
    {
        GetComponent<IPlayerBasicAction>().Crouch(ani,col);
    }
    void Jump()
    {
        GetComponent<IPlayerBasicAction>().Jump(7,rigid, gameObject, ani);
    }
    void Hook()
    {
        if (Input.GetKeyDown(KeyCode.V)&& hookPoint != null)
        {
            hookEndPoint = hookPoint.transform.position - transform.position;
            isHook = true;
            hookTimer = 0.1f;
        }
        if (isHook)
        {
            hookTimer -= Time.deltaTime;
            if(hookPoint.tag=="HookHolder")
            {
                GetComponent<IPlayerSpecialAction>().GrapplingHook(gameObject, rigid, hookPoint, 0.5f);
                if ((hookPoint.transform.position - transform.position).magnitude < 0.1f)
                {
                    rigid.useGravity = false;
                    transform.position = hookPoint.transform.position;
                    isHookHolder = true;
                    ani.SetBool("isGround", true);
                    rigid.velocity = Vector3.zero;
                }
                if (isHookHolder)
                {
                    if (Input.GetKeyDown(KeyCode.Z))
                    {
                        ani.SetBool("isJump", true);
                        rigid.AddForce(Vector3.up * 7, ForceMode.VelocityChange);
                        isHookHolder = false;
                        isHook = false;
                        rigid.useGravity = true;
                    }
                }
            }
            else
            {
                GetComponent<IPlayerSpecialAction>().GrapplingHook(gameObject, rigid, hookPoint, hookEndPoint, 0.5f);
                if (hookTimer <= 0 || hookPoint == null)
                    isHook = false;
            }
        }
    }

    void HookHolder()
    {
        if (Input.GetKeyDown(KeyCode.V) && hookPoint != null)
        {
            hookEndPoint = hookPoint.transform.position - transform.position;
            isHook = true;
            hookTimer = 0.1f;
        }
        if (isHook)
        {
            GetComponent<IPlayerSpecialAction>().GrapplingHook(gameObject, rigid, hookPoint, hookEndPoint, 0.5f);
            hookTimer -= Time.deltaTime;
            if (hookTimer <= 0)
            {
                isHook = false;
            }
            if (hookPoint == null)
            {
                isHook = false;
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Hook" || other.gameObject.tag == "HookHolder")
        {
            hookPoint = other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Hook" || other.gameObject.tag == "HookHolder")
        {
            hookPoint = null;
        }
    }
    void Shoot()
    {
        Gun.GetComponent<MP_2158>().Gun_Shoot_Bullet(transform);
    }
}