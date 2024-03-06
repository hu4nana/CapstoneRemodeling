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
            Debug.Log(own.gameObject.name + "�� " + collisionLayer + "�� �浹�Ͽ����ϴ�.");
            curDir *= -1;
        }

        rigid.velocity = Vector3.right * xDir * moveSpeed;

        //�浹�ϰ� xDir�� 1�� �ٲپ����
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

    // ����ü �߻�
    public void Shoot()
    {

    }
}