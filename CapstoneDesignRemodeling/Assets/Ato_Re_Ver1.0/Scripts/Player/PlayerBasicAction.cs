using System.Collections;
using System.Collections.Generic;
using System.Drawing;
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
        LayerMask layersToIgnore = (1 << LayerMask.NameToLayer("Enemy")) | (1 << LayerMask.NameToLayer("EnemyAttack"));
        RaycastHit hit;
        
        if (Physics.BoxCast(own.transform.position,
            own.transform.localScale/6.5f,
            Vector3.down, 
            out hit, 
            own.transform.rotation
            ,own.transform.localScale.y,~layersToIgnore))
        {
            if(hit.collider.gameObject.layer==LayerMask.NameToLayer("Floor")||
                hit.collider.gameObject.layer == LayerMask.NameToLayer("Wall"))
            {
                ani.SetBool("isGround", true);
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    ani.SetBool("isJump", true);
                    rigid.velocity = new Vector3(rigid.velocity.x,0,0);
                    rigid.AddForce(Vector3.up * upVelocity, ForceMode.VelocityChange);
                }
            }
        }
        else
        {
            ani.SetBool("isGround", false);
        }
        if (rigid.velocity.y <= -1.5f)
        {
            ani.SetBool("isJump", false);
        }
        ani.SetFloat("velocityY", rigid.velocity.y);
    }

    public void Attack(float Dmage, Rigidbody rigid, GameObject own, Animator ani)
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
    public void Crouch(Animator ani,BoxCollider col)
    {
        if (Input.GetKey(KeyCode.DownArrow))
        {
            ani.SetBool("isCrouch", true);
            col.center = new Vector3(0, -0.15f, 0);
            col.size = new Vector3(0.2f, 0.9f, 0.3f);
        }
        else
        {
            ani.SetBool("isCrouch", false);
            
            col.center = new Vector3(0, 0.03f, 0);
            col.size = new Vector3(0.2f, 1.22f, 0.3f);
        }
    }
    public void Crouch(Animator ani,CapsuleCollider col)
    {
        if (Input.GetKey(KeyCode.DownArrow))
        {
            ani.SetBool("isCrouch", true);
            col.center = new Vector3(0, -0.1f, 0);
            col.height = 1;
        }
        else
        {
            ani.SetBool("isCrouch", false);
            col.center = new Vector3(0, 0.07f, 0);
            col.height = 1.3f;
        }
    }
    public void KnockBack(GameObject own, Animator ani,Renderer[] renderers, UnityEngine.Color originalColor)
    {
    }
}
