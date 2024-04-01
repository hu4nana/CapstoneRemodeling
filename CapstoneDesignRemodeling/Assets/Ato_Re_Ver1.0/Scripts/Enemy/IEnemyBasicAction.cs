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



    /*========================== �̵��� ���õ� �Լ�===============================*/
    // target����
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

    // �� �������θ� �̵�, Wall Layer�ν� �� �ݴ�������� �̵�
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
    // Target�ν� �� �ش�������� ������ �̵�
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

    // ���߿��� Target�ν� �� �ش� ������ �ٶ󺸰�, �̵�
    public void ChaseTheTargetFromAir(float startSpeed, GameObject own, GameObject target, Rigidbody rigid)
    {
        //Vector3 direction = (target.transform.position - own.transform.position).normalized;
        //Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);

        //// y�� z �� ȸ���� �����մϴ�.
        //Vector3 euler = targetRotation.eulerAngles;
        //euler.y = -90;
        //euler.z = 0;
        //targetRotation = Quaternion.Euler(euler);

        //// �ε巯�� ȸ���� ���� Quaternion.Slerp()�� ����մϴ�.
        //own.transform.rotation = Quaternion.Slerp(own.transform.rotation, targetRotation, 3 * Time.deltaTime);
        //rigid.velocity = rigid.transform.up * 3;

        //own.transform.LookAt(new Vector3(target.gameObject.transform.position.x, target.gameObject.transform.position.y + 0.6f, 0));
        //rigid.velocity = own.transform.up * 1.5f;

        //Vector3 direction = target.transform.position - own.transform.position;
        //direction.y = 0f; // Y �� ����
        // ������ ���� ȸ��
        //own.transform.rotation = Quaternion.LookRotation(direction, Vector3.up);

    }


    // ���� ���� Target���� �Ÿ��α�
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

    // �����ֽ�, �÷��̾���� �ִ�Ÿ����� ����, �ִ�Ÿ����� ����
    // �÷��̾ �þ߿��� ����� �� ���ڸ��� �ް�����ġ�� ����
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



    // �νĵ� Target�� �������� �����ð����� ��¡
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

    /*^^^^^^^^^^^^^^^^ �̵��� ���õ� �Լ�^^^^^^^^^^^^^^*/


    // �ش� ��ġ���� obj�� �����ϴ� �Լ�
    public void InstinateEnemyObject(GameObject insPoint, GameObject obj)
    {
        Object.Instantiate(obj, insPoint.transform.position, Quaternion.identity);
    }

    // ź�� 8�������� ������ �߻�
    public void Instinate8DirectionBullet(Rigidbody rigid, GameObject own, GameObject bullet)
    {
        for (int i = 0; i < 360; i += 45)
        {
            // ������ �������� ��ȯ�մϴ�.
            float angle = i * Mathf.Deg2Rad;

            // ���� ���͸� ����մϴ�.
            Vector3 direction = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f);

            // �Ѿ��� �����ϰ� ������ �����մϴ�.
            GameObject obj = Object.Instantiate(bullet, own.transform.position, Quaternion.identity);
            obj.GetComponent<Rigidbody>().velocity = direction * 5;
        }
        // // ���� ���͸� ����մϴ�.
        //Vector3 direction = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f);

        // // �Ѿ��� �����ϰ� ������ �����մϴ�.
        //GameObject obj = Object.Instantiate(bullet, own.transform.position, Quaternion.identity);
        //obj.GetComponent<Rigidbody>().velocity = direction * 5;

    }

    // ����ü ���� �� �ӵ� ����
    public void InstinateMissile(Rigidbody rigid, GameObject own, GameObject target, GameObject missile, Vector3 inPos, float speed)
    {
        GameObject projectile = Object.Instantiate(missile, inPos, own.transform.rotation);
        projectile.GetComponent<Rigidbody>().velocity = own.transform.forward * speed;
    }



    /*========================== �ִϸ��̼ǰ� ���õ� �Լ� ===============================*/

    // isMove�� true,false��Ű�� �Լ�
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