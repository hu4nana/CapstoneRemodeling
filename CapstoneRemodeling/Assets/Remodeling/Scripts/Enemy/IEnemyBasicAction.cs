using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface IEnemyBasicAction
{
    // �� �������θ� �̵� ( ���ٽ����� )
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


    // ���� �ð��� ���� On, Off
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

    // ����ü ����
    public void Shoot()
    {

    }

    // �÷��̾�� ��� ���� feat ����
    public void InertiaChargeAttack()
    {

    }
    public void InertiaMove()
    {

    }
}