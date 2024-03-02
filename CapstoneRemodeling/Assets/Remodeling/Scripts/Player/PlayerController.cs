using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour,IPlayerBasicAction
{

    [SerializeField]
    float speed;

    Rigidbody rigid;
    Animator ani;

    IPlayerBasicAction basicAction;



    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        ani=GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //float xInput = Input.GetAxis("Horizontal");

        //int xDir = (int)xInput;
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(
        //        Vector3.right * xDir), Time.deltaTime * 24);
        //rigid.velocity = new Vector3(xDir * 5, rigid.velocity.y, 0);
        ////if (g_isFloor)
        ////{
        ////    g_ani.SetBool("isWalk", true);
        ////}
        //rigid.velocity = new Vector3(xInput * 5, 0, 0);
        //Debug.Log("¿òÁ÷ÀÓ");
        Move();
    }
    void Move()
    {
        //GetComponent<PlayerBasicAction>().Move(speed, rigid);
        basicAction.Move(4, rigid,this.gameObject);
    }
    void Crouch()
    {
        basicAction.GetHashCode();
    }
}
