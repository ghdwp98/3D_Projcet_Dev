using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpawner : MonoBehaviour
{
    //���̾� ����. 
    [SerializeField] CinemachineVirtualCamera vCam;
    [SerializeField] float minSpeed;
    [SerializeField] float maxSpeed;
    

    [SerializeField] PooledObject FirePrefab; //�Ҳ� ����Ʈ
    private void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //�ڷ�ƾ ����.--> �÷��̾� �������� �߰� ���� or �̵� �ϴ� �ڷ�ƾ. 
            // ī�޶� �̵� �� �÷��̾� ������ ����. 
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
            //�ڷ�ƾ ��ž 
        }
    }


    IEnumerator CutScene(Collider ohter)
    {
        

        yield return new WaitForSeconds(1f);
    }



}