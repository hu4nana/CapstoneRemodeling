using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour
{

    [SerializeField]
    float speed;

    Rigidbody rigid;
    Animator ani;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        ani=GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void Move()
    {
        GetComponent<PlayerBasicAction>().Move(speed, rigid);
    }
    void Crouch()
    {
        if (GetComponent<PlayerBasicAction>().Crouch())
        {
            ani.GetBool("isCrouch");
        }
    }
}
