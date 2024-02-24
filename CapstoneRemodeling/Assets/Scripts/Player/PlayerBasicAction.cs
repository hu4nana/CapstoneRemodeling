using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBasicAction : MonoBehaviour
{
    public void Move(float moveSpeed, Rigidbody rigid)
    {
        float xInput = Input.GetAxis("Horizontal");

        rigid.velocity = new Vector3(xInput * moveSpeed, 0, 0);
    }
    public bool Crouch()
    {
        bool yInput = Input.GetKey(KeyCode.DownArrow);
        bool isCrouch = yInput;
        return isCrouch;
    }
}
