using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public interface IDroneBasicAction
{
    // 드론 평상시의 움직임
    public void DroneIdleMove(Transform own,Transform player,Rigidbody rigid ,Vector3 offSet)
    {
        own.position = Vector3.Lerp(own.position,
            new Vector3(player.position.x + rigid.transform.forward.x * offSet.x,
            player.position.y + offSet.y, 0), 6f * Time.deltaTime);
        own.LookAt(new Vector3(player.position.x,player.position.y+0.6f,0));
    }
}
