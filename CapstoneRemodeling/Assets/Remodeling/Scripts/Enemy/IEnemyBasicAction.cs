using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyBasicAction
{
    public void OneDirectionMove(float moveSpeed,Rigidbody rigid,GameObject own,LayerMask collisionLayer)
    {
        int curDir = -1;
        int xDir = curDir;

        RaycastHit hit;
        if(Physics.Raycast(own.transform.position,own.transform.forward,out hit, Mathf.Infinity, collisionLayer))
        {
            Debug.Log(own.gameObject.name + "이 " + collisionLayer + "와 충돌하였습니다.");
            curDir *= -1;
        }

        rigid.velocity = Vector3.right * xDir * moveSpeed;

        //충돌하고 xDir를 1로 바꾸어야함
    }


    // 일정 시간에 맞춰 On, Off
    public void OneTriggerSwitch(float triggerTimer, Rigidbody rigid, GameObject own,Animator ani)
    {
        float timer = triggerTimer;

        bool isActive = true;

        if(timer < 0)
        {
            timer = triggerTimer;
            //if (isActive)
            //{
            //    isActive = false;
            //}
            //else
            //{
            //    isActive = true;
            //}
        }
        else
        {
            timer -= Time.deltaTime;
        }
        ani.SetBool("isTrigger", isActive);
    }

    // 투사체 발사
    public void Shoot()
    {

    }
}