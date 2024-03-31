using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraViewTrigger : MonoBehaviour
{
    [Range(0, 2)]
    public int moveType;
    [Range(0, 2)]
    public int returnMoveType;

    GameObject player;
    PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            player = other.gameObject;
            playerController = player.GetComponent<PlayerController>();
            if (playerController.MoveType != moveType)
            {
                playerController.MoveType = moveType;
            }
            else
            {
                playerController.MoveType = returnMoveType;
            }
        }
    }
    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
    //    {
    //        Debug.Log("a");
    //        player = other.gameObject;
    //        playerController = player.GetComponent<PlayerController>();
    //        if (playerController.MoveType != moveType)
    //        {
    //            playerController.MoveType = moveType;
    //        }
    //    }
    //}
    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
    //    {
    //        player = other.gameObject;
    //        playerController = player.GetComponent<PlayerController>();
    //        playerController.MoveType = 0;
    //    }
    //}
}
