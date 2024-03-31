using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public List<GameObject> cameras;
    public GameObject player;
    PlayerController pController;
    // Start is called before the first frame update
    void Start()
    {

        pController = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            pController.MoveType = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) { pController.MoveType = 1;}
        else if(Input.GetKeyDown(KeyCode.Alpha3)) { pController.MoveType = 2;}
        Debug.Log(pController.MoveType);
        switch(pController.MoveType)
        {
            case 0:
                cameras[0].SetActive(true);
                cameras[1].SetActive(false);
                cameras[2].SetActive(false);
                break;
            case 1:
                cameras[0].SetActive(false);
                cameras[1].SetActive(true);
                cameras[2].SetActive(false);
                break;
            case 2:
                cameras[0].SetActive(false);
                cameras[1].SetActive(false);
                cameras[2].SetActive(true);
                break;
        }
    }
}
