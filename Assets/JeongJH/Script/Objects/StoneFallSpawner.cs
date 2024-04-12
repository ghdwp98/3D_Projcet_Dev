using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class StoneFallSpawner : MonoBehaviour
{
    //�ణ �������� ���������´ٰ� ��������. 


    [SerializeField] PooledObject StonePrefab;
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

    
    // ���� ����Ʈ�� �ϳ� �޾��ְ� �װ��� y�� �� + x�� �� ���� �����ϴ°� ������. 


    IEnumerator SpawnRock()  //z���� �������� �������ֱ�. 
    {
        if(coroutineTime==false) 
        {
            coroutineTime = true;
            float rand = Random.Range(spawnPointXY.transform.position.z-10, spawnPointXY.transform.position.z+10);
            Vector3 zPos = new Vector3(spawnPointXY.transform.position.x, spawnPointXY.transform.position.y
                , rand); //z�� ����. 

            Manager.Pool.GetPool(StonePrefab, zPos, Quaternion.identity);
            yield return new WaitForSeconds(0.7f);            
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
