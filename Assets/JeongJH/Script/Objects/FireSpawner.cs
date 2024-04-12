using Cinemachine;
using System.Collections;
using UnityEngine;

public class FireSpawner : MonoBehaviour
{
    //���̾� ����. 
    [SerializeField] CinemachineVirtualCamera vCam;
    /*[SerializeField] int size;
    [SerializeField] int capacity;
    [SerializeField] int smallSize;
    [SerializeField] int smallCapacity;*/
    //[SerializeField] float range;
    [SerializeField] bool isFirst = true;
    
    [SerializeField] LayerMask playerLayer; //�ǹ� ���µ�? 

    SphereCollider sphereCollider;
    BoxCollider boxCollider;

    [SerializeField]
    GameObject[] spawnPoint; // �ڽ����� ���� ����Ʈ ������ �ű⼭ �������ֱ� or �ϳ��� ū ��� �����. 
    [SerializeField]
    GameObject[] smallSapwnPoint;

    [SerializeField] PooledObject FirePrefab; //�Ҳ� ����Ʈ
    [SerializeField] PooledObject smallFirePrefab;
   


    private void Start()
    {
        sphereCollider=GetComponent<SphereCollider>();
        sphereCollider.enabled = false;
        boxCollider=GetComponent<BoxCollider>();
        boxCollider.enabled = true; //�ڽ��� ���ְ� �̰ɷ� ���� Ʈ���� üũ. 
        isFirst = true;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && isFirst == true)
        {

            //�ڷ�ƾ ����.--> �÷��̾� �������� �߰� ���� or �̵� �ϴ� �ڷ�ƾ. 
            // ī�޶� �̵� �� �÷��̾� ������ ����. 
            CharacterController characterController = other.gameObject.GetComponent<CharacterController>();
            if (characterController != null)
            {
                sphereCollider.enabled = true; //�� ���� ������ ���Ǿ� �ݶ��̴��� ���ֱ�. 
                boxCollider.enabled = false; //�ڽ��²��ֱ�. 
                characterController.enabled = false;
                StartCoroutine(CutScene(other));
            }

        }
    }

   // Ʈ���ŷ� �����°Ÿ� üũ�ϰ�. --> �������� �� ũ�� �������´�.
   // Ʈ���ŷ� ���� ���� ���Ƽ� �ٽ� ���±�� �������� ���� ���� ���� �����ؼ� �÷��̾ ����
   // ���� �������� ������ �� 10�� �� �Ŀ� ���� �������ֱ�. (�ڷ�ƾ 10�� ) 

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
            for (int i = 0; i < spawnPoint.Length; i++) //�÷��̾ �Ѿƿ��� ū �Ҳ�. 
            {
                Manager.Pool.GetPool(FirePrefab, spawnPoint[i].transform.position, Quaternion.identity);
            }
            for (int i = 0; i < smallSapwnPoint.Length; i++) //������ ���� �Ҳ� ����. 
            {
                Manager.Pool.GetPool(smallFirePrefab, smallSapwnPoint[i].transform.position, Quaternion.identity);
            }

            yield return new WaitForSeconds(1f); //3�ʰ� �÷��̾� �̵� ���� + �ƾ�����. 
            vCam.Priority = 9;
            yield return new WaitForSeconds(1f);
            characterController.enabled = true;
            isFirst = false;
        }

    }

    




}