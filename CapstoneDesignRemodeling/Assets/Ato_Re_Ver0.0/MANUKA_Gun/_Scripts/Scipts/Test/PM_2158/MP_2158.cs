using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class MP_2158 : MonoBehaviour
{
    public Transform gunFireTransform;
    public GameObject gunFireEffect;
    public GameObject gunProjEffect;
    public GameObject gunHitEffect;
    public GameObject bullet;

    float yRotation;

    AudioSource aud;
    ParticleSystem ps;
    // Start is called before the first frame update
    void Start()
    {
        ps = gunProjEffect.GetComponent<ParticleSystem>();
        aud = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //Gun_Shoot_Bullet();
    }

    public void Gun_Shoot_Bullet(Transform a)
    {
        if(a.rotation.y > 0 && a.rotation.y<180)
        {
            yRotation = 90;
        }
        else
        {
            yRotation = -90;
        }
        Instantiate(gunFireEffect, gunFireTransform.position,
               Quaternion.Euler(a.rotation.eulerAngles) * gunFireEffect.transform.rotation);
        Instantiate(gunProjEffect, gunFireTransform.position,
            Quaternion.Euler(a.rotation.eulerAngles) * gunProjEffect.transform.rotation);
        Instantiate(bullet, gunFireTransform.position,
            Quaternion.Euler(a.rotation.eulerAngles));
        aud.Play();
        bullet.GetComponent<Rigidbody>().velocity = transform.right * 10;
    }
    //public void Gun_Shoot_Bullet(Transform parents)
    //{
    //    Instantiate(gunFireEffect, gunFireTransform.position,
    //           transform.rotation * gunFireEffect.transform.rotation);
    //    Instantiate(gunProjEffect, gunFireTransform.position,
    //       transform.rotation * gunProjEffect.transform.rotation);
    //    //Instantiate(gunHitEffect, gunFireTransform.position,
    //    //   transform.rotation * gunHitEffect.transform.rotation);

    //    Instantiate(bullet, gunFireTransform.position,
    //       parents.rotation);
    //    //bul.GetComponent<Rigidbody>().velocity = transform.right * 10;
    //}
}
