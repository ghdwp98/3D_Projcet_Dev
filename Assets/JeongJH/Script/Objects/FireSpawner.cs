using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpawner : MonoBehaviour
{
    //파이어 생성. 
    [SerializeField] CinemachineVirtualCamera vCam;
    [SerializeField] float minSpeed;
    [SerializeField] float maxSpeed;
    

    [SerializeField] PooledObject FirePrefab; //불꽃 이펙트
    private void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //코루틴 시작.--> 플레이어 방향으로 추가 생성 or 이동 하는 코루틴. 
            // 카메라 이동 및 플레이어 움직임 방지. 
            CharacterController characterController = other.gameObject.GetComponent<CharacterController>();
            if(characterController != null)
            {
                characterController.enabled = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            //코루틴 스탑 
        }
    }


    IEnumerator CutScene(Collider ohter)
    {
        

        yield return new WaitForSeconds(1f);
    }



}