using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface IEnemyBasicAction
{
    // �� �������θ� �̵�
    public void OneDirectionMove(float moveSpeed,Rigidbody rigid,GameObject own,LayerMask collisionLayer)
    {
        int curDir = -1;
        int xDir = curDir;
        float rotation = 90;
        RaycastHit hit;
        if(Physics.Raycast(own.transform.position,Vector3.left*100,out hit, collisionLayer))
        {
            Debug.Log(own.gameObject.name + "�� " + collisionLayer + "�� �浹�Ͽ����ϴ�.");
            xDir *= -1;
        }
        rigid.velocity = new Vector3(xDir * moveSpeed, rigid.velocity.y, 0);
        Debug.Log("����");
        
        own.transform.rotation = Quaternion.Slerp(own.transform.rotation,
    Quaternion.LookRotation(Vector3.right * xDir), Time.deltaTime * 24);
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
}