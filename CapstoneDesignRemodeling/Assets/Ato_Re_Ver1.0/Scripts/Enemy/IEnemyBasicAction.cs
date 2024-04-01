using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using VLB;

public interface IEnemyBasicAction
{
    public Monster_State_Manage monster_State_Manage { get; set; }



    /*========================== 이동과 관련된 함수===============================*/
    // target설정
    public GameObject DetectTarget(GameObject own, GameObject target, float length)
    {
        LayerMask layersToIgnore = (1 << LayerMask.NameToLayer("Enemy")) | (1 << LayerMask.NameToLayer("EnemyAttack"));
        RaycastHit hit;
        if (Physics.Raycast(own.transform.position,
            Vector3.right * (own.transform.rotation.y / Mathf.Abs(own.transform.rotation.y))
            , out hit, length, ~layersToIgnore))
        {
            if (hit.collider.gameObject.layer == 10)
            {
                target = hit.collider.gameObject;
            }
        }
        else
        {
            target = null;
        }

        return target;
    }

    // 한 방향으로만 이동, Wall Layer인식 시 반대방향으로 이동
    public void OneDirectionMove(float moveSpeed, Rigidbody rigid, GameObject own)
    {
        RaycastHit hit;
        if (Physics.Raycast(own.transform.position,
            Vector3.right * (own.transform.rotation.y / Mathf.Abs(own.transform.rotation.y))
            , out hit, own.transform.localScale.z + own.transform.localScale.z / 3, ~LayerMask.NameToLayer("EnemyAttack")))
        {
            //Debug.Log(hit.collider.name);
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Wall"))
            {
                rigid.velocity = new Vector3(0, rigid.velocity.y, 0);
                own.transform.rotation = Quaternion.Slerp(own.transform.rotation,
                       Quaternion.LookRotation(Vector3.right *
                           -(own.transform.rotation.y / Mathf.Abs(own.transform.rotation.y)))
                       , Time.deltaTime * 24);
            }
        }
        else
        {
            if (0 <= own.transform.rotation.y && own.transform.rotation.y < 90)
            {
                own.transform.rotation = Quaternion.Slerp(own.transform.rotation,
                           Quaternion.LookRotation(Vector3.right), Time.deltaTime * 24);
                rigid.velocity = new Vector3(0, rigid.velocity.y, 0);
            }
            if (0 >= own.transform.rotation.y && own.transform.rotation.y > -90)
            {
                own.transform.rotation = Quaternion.Slerp(own.transform.rotation,
                           Quaternion.LookRotation(Vector3.left), Time.deltaTime * 24);
                rigid.velocity = new Vector3(0, rigid.velocity.y, 0);
            }
        }
        rigid.velocity = new Vector3(moveSpeed * (own.transform.rotation.y / Mathf.Abs(own.transform.rotation.y))
            , rigid.velocity.y, 0);
    }
    // Target인식 시 해당방향으로 빠르게 이동
    public void ChargeToTarget(float startSpeed, GameObject own, Rigidbody rigid, LayerMask collisionLayer)
    {
        RaycastHit hit;
        float accelation = 1.2f;
        int maxSpeed = 10;
        if (Physics.Raycast(own.transform.position,
            Vector3.right * (own.transform.rotation.y / Mathf.Abs(own.transform.rotation.y))
            , out hit, 5))
        {
            if (hit.collider.gameObject.layer == 10)
            {
                if (rigid.velocity.x >= maxSpeed)
                {
                    rigid.velocity = new Vector3(maxSpeed, rigid.velocity.y, 0);
                }
                else
                {
                    rigid.velocity += new Vector3(startSpeed * accelation, rigid.velocity.y, 0);
                }

            }
            if (hit.collider.gameObject.layer != 10)
            {
                rigid.velocity = new Vector3(0, rigid.velocity.y, 0);
            }
        }
    }

    // 공중에서 Target인식 시 해당 방향을 바라보고, 이동
    public void ChaseTheTargetFromAir(float startSpeed, GameObject own, GameObject target, Rigidbody rigid)
    {
        //Vector3 direction = (target.transform.position - own.transform.position).normalized;
        //Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);

        //// y와 z 축 회전을 제거합니다.
        //Vector3 euler = targetRotation.eulerAngles;
        //euler.y = -90;
        //euler.z = 0;
        //targetRotation = Quaternion.Euler(euler);

        //// 부드러운 회전을 위해 Quaternion.Slerp()를 사용합니다.
        //own.transform.rotation = Quaternion.Slerp(own.transform.rotation, targetRotation, 3 * Time.deltaTime);
        //rigid.velocity = rigid.transform.up * 3;

        //own.transform.LookAt(new Vector3(target.gameObject.transform.position.x, target.gameObject.transform.position.y + 0.6f, 0));
        //rigid.velocity = own.transform.up * 1.5f;

        //Vector3 direction = target.transform.position - own.transform.position;
        //direction.y = 0f; // Y 축 고정
        // 방향을 향해 회전
        //own.transform.rotation = Quaternion.LookRotation(direction, Vector3.up);

    }


    // 범위 내의 Target한테 거리두기
    public void KeepDistanceToTarget(float distance, GameObject own, Rigidbody rigid)
    {
        RaycastHit hit;
        if (Physics.Raycast(own.transform.position,
            Vector3.right * (own.transform.rotation.y / Mathf.Abs(own.transform.rotation.y))
            , out hit, 5))
        {
            if (hit.collider.gameObject.layer == 10)
            {

            }
            if (hit.collider.gameObject.layer != 10)
            {
                rigid.velocity = new Vector3(0, rigid.velocity.y, 0);
            }
        }
    }

    // 전방주시, 플레이어감지시 최대거리까지 전진, 최대거리에서 멈춤
    // 플레이어가 시야에서 벗어났을 때 제자리로 뒷걸음질치며 복귀
    public void BeOnGuard(Rigidbody rigid, GameObject own, GameObject target, Vector3 startPos, float maxDistance, float speed)
    {
        if (target != null)
        {
            if (((startPos.x - maxDistance) <= own.transform.position.x &&
                own.transform.position.x <= (startPos.x + maxDistance)))
            {
                rigid.velocity = own.transform.forward * speed;
            }
        }
        if (target == null)
        {
            if (own.transform.position.x != startPos.x)
            {
                if (own.transform.position.x > startPos.x)
                {
                    rigid.velocity = own.transform.forward * speed;
                }
                if (own.transform.position.x < startPos.x)
                {
                    rigid.velocity = own.transform.forward * -speed;
                }
                if (-0.1f < startPos.x - own.transform.position.x
                    && startPos.x - own.transform.position.x < 0.1f)
                {
                    rigid.velocity = Vector3.zero;
                }
            }
        }
    }



    // 인식된 Target의 방향으로 일정시간동안 돌징
    public void Charge(GameObject target, GameObject own, Rigidbody rigid, float endTimer)
    {
        int minSpeed = 1;
        int maxSpeed = 15;
        float accelation = 1.1f;
        if (target != null)
        {
            rigid.velocity += new Vector3(minSpeed * accelation, rigid.velocity.y, 0);
        }
        else
        {
            endTimer -= Time.deltaTime;
            if (endTimer <= 0)
            {
                rigid.velocity = new Vector3(0, rigid.velocity.y, 0);
            }
        }
        if (rigid.velocity.x >= maxSpeed)
        {
            rigid.velocity = new Vector3(maxSpeed, rigid.velocity.y, 0);
        }
    }

    /*^^^^^^^^^^^^^^^^ 이동과 관련된 함수^^^^^^^^^^^^^^*/


    // 해당 위치에서 obj를 생성하는 함수
    public void InstinateEnemyObject(GameObject insPoint, GameObject obj)
    {
        Object.Instantiate(obj, insPoint.transform.position, Quaternion.identity);
    }

    // 탄을 8방향으로 퍼지게 발사
    public void Instinate8DirectionBullet(Rigidbody rigid, GameObject own, GameObject bullet)
    {
        for (int i = 0; i < 360; i += 45)
        {
            // 각도를 라디안으로 변환합니다.
            float angle = i * Mathf.Deg2Rad;

            // 방향 벡터를 계산합니다.
            Vector3 direction = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f);

            // 총알을 생성하고 방향을 설정합니다.
            GameObject obj = Object.Instantiate(bullet, own.transform.position, Quaternion.identity);
            obj.GetComponent<Rigidbody>().velocity = direction * 5;
        }
        // // 방향 벡터를 계산합니다.
        //Vector3 direction = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f);

        // // 총알을 생성하고 방향을 설정합니다.
        //GameObject obj = Object.Instantiate(bullet, own.transform.position, Quaternion.identity);
        //obj.GetComponent<Rigidbody>().velocity = direction * 5;

    }

    // 투사체 생성 및 속도 설정
    public void InstinateMissile(Rigidbody rigid, GameObject own, GameObject target, GameObject missile, Vector3 inPos, float speed)
    {
        GameObject projectile = Object.Instantiate(missile, inPos, own.transform.rotation);
        projectile.GetComponent<Rigidbody>().velocity = own.transform.forward * speed;
    }



    /*========================== 애니메이션과 관련된 함수 ===============================*/

    // isMove를 true,false시키는 함수
    public void SetAni_Move(Rigidbody rigid, GameObject own, Animator ani)
    {
        if (rigid.velocity.x != 0)
        {
            ani.SetBool("isMove", true);
        }
        else
        {
            ani.SetBool("isMove", false);
        }
    }
}