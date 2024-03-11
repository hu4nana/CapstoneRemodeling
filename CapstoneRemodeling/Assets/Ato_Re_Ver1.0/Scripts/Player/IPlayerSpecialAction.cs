using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerSpecialAction
{
    public void GrapplingHook(GameObject own, GameObject target, Rigidbody rigid, Vector3 moveVelocity)
    {
        rigid.useGravity = false;
        rigid.velocity = moveVelocity;

    }
}
