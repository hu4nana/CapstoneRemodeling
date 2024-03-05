using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public interface IPlayerBasicAction
{
    public void Move(float moveSpeed, Rigidbody rigid,GameObject player,Animator ani)
    {
        
        if (Input.GetAxis("Horizontal") != 0)
        {
            float xInput = Input.GetAxis("Horizontal");
            float xDir = xInput;

            rigid.velocity = new Vector3(xDir * moveSpeed, rigid.velocity.y, 0);

            ani.SetBool("isRun", true);

            if(xDir!=0)
            {
                player.transform.rotation = Quaternion.Slerp(player.transform.rotation, 
                    Quaternion.LookRotation(Vector3.right * xDir), Time.deltaTime * 24);
            }
        }
        else
        {
            ani.SetBool("isRun", false);
        }
    }
    public void Attack(float Dmage, Rigidbody rigid,GameObject player, Animator ani)
    {
        if(Input.GetKey(KeyCode.X))
        {
            ani.SetBool("isAttack",true);
        }
        else
        {
            ani.SetBool("isAttack", false);
        }
    }
    public void Crouch(Animator ani)
    {
        if (Input.GetKey(KeyCode.DownArrow))
        {
            ani.SetBool("isCrouch", true);
        }
        else
            ani.SetBool("isCrouch", false);
    }
}
