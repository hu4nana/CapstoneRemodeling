using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerBasicAction
{
    public void Move(float moveSpeed, Rigidbody rigid,GameObject player)
    {
        float xInput = Input.GetAxis("Horizontal");

        int xDir = (int)xInput;
        player.transform.rotation = Quaternion.Slerp(player.transform.rotation, Quaternion.LookRotation(
                Vector3.right * xDir), Time.deltaTime * 24);
        rigid.velocity = new Vector3(xDir * moveSpeed, rigid.velocity.y, 0);
        //if (g_isFloor)
        //{
        //    g_ani.SetBool("isWalk", true);
        //}
        rigid.velocity = new Vector3(xInput * moveSpeed, 0, 0);
        Debug.Log("¿òÁ÷ÀÓ");
    }
    public bool Crouch()
    {
        bool yInput = Input.GetKey(KeyCode.DownArrow);
        bool isCrouch = yInput;
        return isCrouch;
    }
    //public void BasicAttack();
}
