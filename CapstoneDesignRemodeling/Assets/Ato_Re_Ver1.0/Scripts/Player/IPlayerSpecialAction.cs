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
        // 전방에 있는 플랫폼의 높이 y 가 플레이어의 y만큼 긴가?를 확인해서 메달리기
        // or 전방에 있는 플랫폼의레이어가 Wall인가를 확인하여 메달리기
    }
}
