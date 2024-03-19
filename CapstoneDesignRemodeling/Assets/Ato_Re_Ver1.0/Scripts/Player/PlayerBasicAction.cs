using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public interface IPlayerBasicAction
{
    public void Move(float moveSpeed, Rigidbody rigid, GameObject own, Animator ani)
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            float xInput = Input.GetAxis("Horizontal");
            float xDir = xInput;
            if (ani.GetBool("isCrouch"))
                rigid.velocity = new Vector3(xDir * moveSpeed / 2, rigid.velocity.y, 0);
            else
                rigid.velocity = new Vector3(xDir * moveSpeed, rigid.velocity.y, 0);

            ani.SetBool("isRun", true);

            if (xDir != 0)
            {
                own.transform.rotation = Quaternion.Slerp(own.transform.rotation,
                    Quaternion.LookRotation(Vector3.right * xDir), Time.deltaTime * 24);
            }
        }
        else
        {
            Vector3 targetVelocity = new Vector3(0, rigid.velocity.y, 0);
            rigid.velocity = Vector3.SmoothDamp(rigid.velocity,
                targetVelocity, ref targetVelocity, 0.012f);
            ani.SetBool("isRun", false);
        }
    }

    public void Jump(float upVelocity, Rigidbody rigid, GameObject own, Animator ani)
    {
        RaycastHit hit;
        if (Physics.Raycast(own.transform.position,
            Vector3.down
            , out hit, own.transform.localScale.y))
        {
            if (hit.collider.gameObject.layer != 8||
                hit.collider.gameObject.layer!=9)
            {
                ani.SetBool("isGround", true);
                if(Input.GetKeyDown(KeyCode.Z))
                {
                    ani.SetBool("isJump", true);
                    //rigid.velocity = new Vector3(rigid.velocity.x,5,0);
                    rigid.AddForce(Vector3.up * upVelocity, ForceMode.Impulse);
                }
            }
        }
        else
        {
            ani.SetBool("isJump", false);
            ani.SetBool("isGround", false);
            ani.SetFloat("velocityY", rigid.velocity.y);
        }
    }

    public void Attack(float Dmage, Rigidbody rigid, GameObject player, Animator ani)
    {
        if (Input.GetKey(KeyCode.X))
        {
            ani.SetBool("isAttack", true);
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
