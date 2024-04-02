using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneFallSpawner : MonoBehaviour
{
    //�ణ �������� ���������´ٰ� ��������. 


    [SerializeField] PooledObject StonePrefab;
    [SerializeField] bool isTrigger;
    [SerializeField] bool coroutineTime;
    [SerializeField] int size = 10;
    [SerializeField] int capacity = 10;

    private void Start()
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


    IEnumerator SpawnRock()
    {
        if(coroutineTime==false) 
        {
            Debug.Log("�ڷ�ƾ����");
            coroutineTime = true;
            float rand = Random.Range(transform.position.x-5, transform.position.x+5);
            Vector3 xPos = new Vector3(rand, transform.position.y + 5, transform.position.z);

      

            Manager.Pool.GetPool(StonePrefab,xPos,transform.rotation);
            yield return new WaitForSeconds(2f);            
            coroutineTime = false;
        }


        
    }

    private void OnTriggerStay(Collider other)
    {
        
        isTrigger= true;    
    }

    private void OnTriggerExit(Collider other)
    {
        isTrigger = false;
    }
}
