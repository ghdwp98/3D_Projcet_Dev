using Cinemachine;
using System.Collections;
using UnityEngine;

public class FireSpawner : MonoBehaviour
{
    //파이어 생성. 
    [SerializeField] CinemachineVirtualCamera vCam;
    /*[SerializeField] int size;
    [SerializeField] int capacity;
    [SerializeField] int smallSize;
    [SerializeField] int smallCapacity;*/
    //[SerializeField] float range;
    [SerializeField] bool isFirst = true;
    
    [SerializeField] LayerMask playerLayer; //의미 없는듯? 

    SphereCollider sphereCollider;
    BoxCollider boxCollider;

    [SerializeField]
    GameObject[] spawnPoint; // 자식으로 스폰 포인트 가지고 거기서 생성해주기 or 하나의 큰 산불 만들기. 
    [SerializeField]
    GameObject[] smallSapwnPoint;

    [SerializeField] PooledObject FirePrefab; //불꽃 이펙트
    [SerializeField] PooledObject smallFirePrefab;
   


    private void Start()
    {
        sphereCollider=GetComponent<SphereCollider>();
        sphereCollider.enabled = false;
        boxCollider=GetComponent<BoxCollider>();
        boxCollider.enabled = true; //박스는 켜주고 이걸로 최초 트리거 체크. 
        isFirst = true;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && isFirst == true)
        {

            //코루틴 시작.--> 플레이어 방향으로 추가 생성 or 이동 하는 코루틴. 
            // 카메라 이동 및 플레이어 움직임 방지. 
            CharacterController characterController = other.gameObject.GetComponent<CharacterController>();
            if (characterController != null)
            {
                sphereCollider.enabled = true; //더 넓은 범위의 스피어 콜라이더를 켜주기. 
                boxCollider.enabled = false; //박스는꺼주기. 
                characterController.enabled = false;
                StartCoroutine(CutScene(other));
            }

        }
    }

   // 트리거로 들어오는거를 체크하고. --> 오버랩은 더 크게 만들어놓는다.
   // 트리거로 들어온 길을 막아서 다시 들어온길로 못들어오게 길을 막는 불을 생성해서 플레이어를 추적
   // 이후 오버랩을 나가면 한 10초 쯤 후에 불을 삭제해주기. (코루틴 10초 ) 

    IEnumerator CutScene(Collider other)
    {
        if (isFirst == true)
        {
            isFirst = false;
            CharacterController characterController = other.gameObject.GetComponent<CharacterController>();
            if (characterController != null)
            {
                characterController.enabled = false;
            }
            vCam.Priority = 11;
            for (int i = 0; i < spawnPoint.Length; i++) //플레이어를 쫓아오는 큰 불꽃. 
            {
                Manager.Pool.GetPool(FirePrefab, spawnPoint[i].transform.position, Quaternion.identity);
            }
            for (int i = 0; i < smallSapwnPoint.Length; i++) //고정된 작은 불꽃 생성. 
            {
                Manager.Pool.GetPool(smallFirePrefab, smallSapwnPoint[i].transform.position, Quaternion.identity);
            }

            yield return new WaitForSeconds(1f); //3초간 플레이어 이동 방지 + 컷씬진행. 
            vCam.Priority = 9;
            yield return new WaitForSeconds(1f);
            characterController.enabled = true;
            isFirst = false;
        }

    }

    




}