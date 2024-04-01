using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public interface IDroneBasicAction
{
    // 드론 평상시의 움직임
    public void DroneIdleMove(Transform own,GameObject player,Rigidbody rigid ,Vector3 offSet)
    {
        if(player.GetComponent<PlayerController>().MoveType == 0){
            own.position = Vector3.Lerp(own.position,
            new Vector3(player.transform.position.x + rigid.transform.forward.x * offSet.x,
            player.transform.position.y + offSet.y, player.transform.position.z), 6f * Time.deltaTime);
            own.LookAt(new Vector3(player.transform.position.x, player.transform.position.y + 0.6f, player.transform.position.z));
        }
        else if (player.GetComponent<PlayerController>().MoveType == 1)
        {
            own.position = Vector3.Lerp(own.position,
            new Vector3(player.transform.position.x,
            player.transform.position.y + offSet.y, player.transform.position.z + rigid.transform.forward.z * offSet.x), 6f * Time.deltaTime);
            own.LookAt(new Vector3(player.transform.position.x, player.transform.position.y + 0.6f, player.transform.position.z));
        }
        else if (player.GetComponent<PlayerController>().MoveType == 2)
        {
            own.position = Vector3.Lerp(own.position,
            new Vector3(player.transform.position.x,
            player.transform.position.y + offSet.y, player.transform.position.z + rigid.transform.forward.x * offSet.x), 6f * Time.deltaTime);
            own.LookAt(new Vector3(rigid.transform.forward.z, rigid.transform.forward.z, rigid.transform.forward.z));
        }

    }
}
