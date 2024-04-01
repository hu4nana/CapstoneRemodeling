using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Stage1_Event_Case2 : MonoBehaviour
{
    [SerializeField]
    GameObject eventTrigger;
    [SerializeField]
    List<GameObject> EnemyList;
    [SerializeField]
    List<GameObject> SpawnPointList;

    List<GameObject> SpawnedEnemy=new List<GameObject>();

    GameObject player;
    int maxSpawn = 10;
    int spawnCount = 0;
    bool isStart = false;
    bool isEnd = false;
    bool isAllNull = false;
    float spawnTimer = 1.5f;
    float EventTimer = 20f;
    // Start is called before the first frame update

    // 무작위 위치에, 무작위 적이, 1~2.5f초 간격으로 스폰...
    // 총 13번
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            isEnd = true;
        }
        Event();
        EventEndTrigger();
    }
    private void OnCollisionEnter(Collision collision)
    {
        foreach ( GameObject obj in SpawnedEnemy)
        {
            if(collision.gameObject == obj)
            {
                Destroy( obj );
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        EventStartTrigger(other);
    }
    public void EventStartTrigger(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") &&
            other.GetComponent<PlayerController>().MoveType == 2)
        {
            player = other.gameObject;
            eventTrigger.SetActive(false);
            isStart = true;
        }
        
    }
    public  void Event()
    {
        if (isStart && !isEnd)
        {
            if (spawnCount < maxSpawn)
            {
                spawnTimer-=Time.deltaTime;
                if(spawnTimer <= 0)
                {
                    GameObject _obj = Instantiate(EnemyList[Random.Range(0, EnemyList.Count)],
        SpawnPointList[Random.Range(0, SpawnPointList.Count)].transform.position,
        SpawnPointList[Random.Range(0, SpawnPointList.Count)].transform.rotation);
                    SpawnedEnemy.Add(_obj);
                    spawnCount++;
                    spawnTimer = 1.6f;
                }            
            }
            EventTimer -= Time.deltaTime;
            if(EventTimer < 0)
            {
                isStart= false;
                isEnd = true;
            }
        }

        foreach (GameObject obj in SpawnedEnemy)
        {
            if(obj!=null)
            {
                isAllNull = false;
                break;
            }
        }
        if(isAllNull)
        {
            isEnd = true;
        }
    }
    public void EventEndTrigger()
    {
        if(isEnd)
        {
            isStart = false;
            foreach (GameObject obj in SpawnedEnemy)
            {
                Destroy(obj);
            }
                
            player.GetComponent<PlayerController>().transform.position = new Vector3(
                player.GetComponent<PlayerController>().transform.position.x,
                player.GetComponent<PlayerController>().transform.position.y,
                0);
            player.GetComponent<PlayerController>().MoveType = 0;
            SpawnedEnemy.Clear();
            gameObject.SetActive(false);
        }
    }
}
