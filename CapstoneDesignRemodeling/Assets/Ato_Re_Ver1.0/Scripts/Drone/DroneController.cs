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

    public Transform gunFireTransform;
    public List<GameObject> gunFireEffect;
    public List<GameObject> gunProjEffect;
    public List<GameObject> gunHitEffect;
    public List<GameObject> bullet;
    float yRotation;
    int droneMode = 0;
    AudioSource aud;

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
        Drone_Shoot_Bullet(transform);
    }
    private void FixedUpdate()
    {
        IdleMove();
    }
    void IdleMove()
    {
        GetComponent<IDroneBasicAction>().DroneIdleMove(gameObject.transform,player,playerRigid,offSet);
    }
    public void Drone_Shoot_Bullet(Transform a)
    {
        if (a.rotation.y > 0 && a.rotation.y < 180)
        {
            yRotation = 90;
        }
        else
        {
            yRotation = -90;
        }
        Instantiate(gunFireEffect[droneMode], gunFireTransform.position,
               Quaternion.Euler(a.rotation.eulerAngles) * gunFireEffect[droneMode].transform.rotation);
        Instantiate(gunProjEffect[droneMode], gunFireTransform.position,
            Quaternion.Euler(a.rotation.eulerAngles) * gunProjEffect[droneMode].transform.rotation);
        Instantiate(bullet[droneMode], gunFireTransform.position,
            Quaternion.Euler(a.rotation.eulerAngles));
        aud.Play();
        bullet[droneMode].GetComponent<Rigidbody>().velocity = transform.right * 10;
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
