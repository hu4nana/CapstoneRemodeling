using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public interface IPlayerSpecialAction
{
    public void GrapplingHook(GameObject own, Rigidbody rigid, GameObject target,float time)
    {

        own.transform.position = Vector3.MoveTowards(
        own.transform.position, target.transform.position, time);
    }
    public void GrapplingHook(GameObject own, Rigidbody rigid, GameObject target, Vector3 moveVelocity, float time)
    {
        own.transform.position = Vector3.MoveTowards(
        own.transform.position, target.transform.position + moveVelocity, time);
    }
    public void WallHolder(GameObject own,Rigidbody rigid)
    {
        // ���濡 �ִ� �÷����� ���� y �� �÷��̾��� y��ŭ �䰡?�� Ȯ���ؼ� �޴޸���
        // or ���濡 �ִ� �÷����Ƿ��̾ Wall�ΰ��� Ȯ���Ͽ� �޴޸���
    }
}
