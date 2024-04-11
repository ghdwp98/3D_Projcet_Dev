using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class StoneFallSpawner : MonoBehaviour
{
    //약간 절벽에서 굴러내려온다고 생각하자. 


    [SerializeField] PooledObject StonePrefab;
    [SerializeField] GameObject player;
    [SerializeField] bool isTrigger;
    [SerializeField] bool coroutineTime;
    [SerializeField] int size = 10;
    [SerializeField] int capacity = 10;
    [SerializeField] CinemachineVirtualCamera impluseCam;
    [SerializeField] GameObject spawnPointXY;
    

    private void Awake()
    {
        Manager.Pool.CreatePool(StonePrefab, size, capacity);
       
    }


    private void Update()
    {
        if(isTrigger)
        {
            StartCoroutine(SpawnRock());
        }
    }

    
    // 스폰 포인트를 하나 달아주고 그거의 y축 값 + x축 값 에서 생성하는게 나을듯. 


    IEnumerator SpawnRock()  //z축을 랜덤으로 설정해주기. 
    {
        if(coroutineTime==false) 
        {
            coroutineTime = true;
            float rand = Random.Range(transform.position.x-5, transform.position.x+5);
            Vector3 xPos = new Vector3(rand, transform.position.y + 5, transform.position.z);

            //Manager.Pool.GetPool(StonePrefab,xPos,transform.rotation);
            Manager.Pool.GetPool(StonePrefab, spawnPointXY.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(1f);            
            coroutineTime = false;
        }


        
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            impluseCam.Priority = 11;
            impluseCam.LookAt = other.transform;
            isTrigger = true;
        }
         
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            impluseCam.Priority = 9;
            isTrigger = false;
        }

            
    }
}
