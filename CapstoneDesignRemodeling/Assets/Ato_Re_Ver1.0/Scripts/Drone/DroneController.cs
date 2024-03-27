using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneController : MonoBehaviour,
    IDroneBasicAction
{
    [SerializeField]
    GameObject player;
    [SerializeField]
    Vector3 offSet;

    Rigidbody playerRigid;
    Animator playerAni;

    float hoveringTimer = 0.7f;
    float hoveringTime = 0;
    bool isHovering = false;
    // Start is called before the first frame update
    void Start()
    {
        playerRigid = player.GetComponent<Rigidbody>();
        playerAni = player.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        DroneHovering();
    }
    private void FixedUpdate()
    {
        IdleMove();
    }
    void IdleMove()
    {
        GetComponent<IDroneBasicAction>().DroneIdleMove(gameObject.transform,player.transform,playerRigid,offSet);
    }
    void DroneHovering()
    {
        if (!playerAni.GetBool("isGround"))
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                isHovering = true;
                hoveringTime = 0;
            }
        }
        if(isHovering)
        {
            hoveringTime += Time.deltaTime;
            playerRigid.velocity = new Vector3(playerRigid.velocity.x,
                0, 0);
            if(hoveringTime>=hoveringTimer||Input.GetKeyUp(KeyCode.Z))
            {
                isHovering = false;
            }
        }
        else
        {
            playerRigid.velocity = new Vector3(playerRigid.velocity.x,
            playerRigid.velocity.y,
            0);
            isHovering = false;
            hoveringTime = 0;
        }
    }
}
