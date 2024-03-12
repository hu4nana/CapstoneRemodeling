using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface IEnemyBasicAction
{
    // 한 방향으로만 이동 ( 굼바스럽게 )
    public void OneDirectionMove(float moveSpeed,Rigidbody rigid,GameObject own,LayerMask collisionLayer)
    {
        RaycastHit hit;
        if (Physics.Raycast(own.transform.position, 
            Vector3.right* (own.transform.rotation.y / Mathf.Abs(own.transform.rotation.y))
            , out hit, own.transform.localScale.x / 2))
        {
            if (hit.collider.gameObject.layer == 6)
            {
                rigid.velocity = new Vector3(0, rigid.velocity.y, 0);
                own.transform.rotation = Quaternion.Slerp(own.transform.rotation,
                       Quaternion.LookRotation(Vector3.right *
                           -(own.transform.rotation.y / Mathf.Abs(own.transform.rotation.y)))
                       , Time.deltaTime * 24);
            }
        }
        else
        {
            if (0 <= own.transform.rotation.y && own.transform.rotation.y < 90)
            {
                own.transform.rotation = Quaternion.Slerp(own.transform.rotation,
                           Quaternion.LookRotation(Vector3.right), Time.deltaTime * 24);
                rigid.velocity = new Vector3(0, rigid.velocity.y, 0);
            }
            if (0 >= own.transform.rotation.y && own.transform.rotation.y > -90)
            {
                own.transform.rotation = Quaternion.Slerp(own.transform.rotation,
                           Quaternion.LookRotation(Vector3.left), Time.deltaTime * 24);
                rigid.velocity = new Vector3(0, rigid.velocity.y, 0);
            }
        }
        rigid.velocity=new Vector3(moveSpeed*(own.transform.rotation.y/ Mathf.Abs(own.transform.rotation.y))
            , rigid.velocity.y, 0);    
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
   
    
    // 전방에 투사체 발사
    public void Shoot()
    {

    }

    // 범위 내의 Target한테 돌진
    public void ChargeToTarget(float startSpeed,GameObject own, Rigidbody rigid,LayerMask collisionLayer)
    {
        RaycastHit hit;
        float accelation = 1.2f;
        int maxSpeed = 10;
        if (Physics.Raycast(own.transform.position,
            Vector3.right * (own.transform.rotation.y / Mathf.Abs(own.transform.rotation.y))
            , out hit, 5))
        {
            if (hit.collider.gameObject.layer == 10)
            {
                
                if (rigid.velocity.x >= maxSpeed)
                {
                    rigid.velocity = new Vector3(maxSpeed, rigid.velocity.y, 0);
                }
                else
                {
                    rigid.velocity += new Vector3(startSpeed * accelation, rigid.velocity.y, 0);
                }
                    
            }
            if (hit.collider.gameObject.layer != 10)
            {
                rigid.velocity=new Vector3(0,rigid.velocity.y, 0);
            }
        }
    }    

    // 범위 내의 Target한테 거리두기
    public void KeepDistanceToTarget(float distance,GameObject own,Rigidbody rigid)
    {
        RaycastHit hit;
        if (Physics.Raycast(own.transform.position,
            Vector3.right * (own.transform.rotation.y / Mathf.Abs(own.transform.rotation.y))
            , out hit, 5))
        {
            if (hit.collider.gameObject.layer == 10)
            {

            }
            if (hit.collider.gameObject.layer != 10)
            {
                rigid.velocity = new Vector3(0, rigid.velocity.y, 0);
            }
        }
    }

    // 플레이어에게 계속 돌진 feat 관성
    public void InertiaChargeAttack()
    {

    }
    public void InertiaMove()
    {

    }


    // Target을 매개변수로 받아오기
    public void Charge(GameObject target,GameObject own, Rigidbody rigid,float endTimer)
    {
        int minSpeed = 1;
        int maxSpeed = 15;
        float accelation = 1.1f;
        if(target!=null)
        {
            rigid.velocity += new Vector3(minSpeed * accelation, rigid.velocity.y, 0);
        }
        else
        {
            endTimer-=Time.deltaTime;
            if (endTimer <= 0)
            {
                rigid.velocity=new Vector3(0,rigid.velocity.y, 0);
            }
        }
        if (rigid.velocity.x >= maxSpeed)
        {
            rigid.velocity = new Vector3(maxSpeed, rigid.velocity.y, 0);
        }
    }
}