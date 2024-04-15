using JJH;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    //����� ��� + �÷��̾� �˹� +������ + player��ġ�� �̵�. 
    // �����̴� fire ++ samll ���� fire�� �ٸ� ��ũ��Ʈ�� �������ֱ�. 
    // ++ ������ ����� ���̻� ���� �ʾƾ� �ϴµ� ��� ó��������? 

    AudioSource audioSource;
    GameObject player;
    Vector3 targetPos;
    bool isTriggerOn;
    bool isPoolOn;
    [SerializeField] float minMoveSpeed; // �̵� �ӵ�. ���߿� �������ֱ�. 
    [SerializeField] float maxMoveSpeed;
    [SerializeField] PooledObject pooledObject;
    [SerializeField]LayerMask playerLayer;
    [SerializeField] float KnockBackPower; //�˹� ���� ���ϰ� ���ֱ�. 
    [SerializeField] float damage;
    
    
    
    
    private void OnEnable()
    {
        isPoolOn = false;
        isTriggerOn = false;
        audioSource = GetComponent<AudioSource>();
        player = GameObject.FindWithTag("Player");

    }

    private void FixedUpdate()
    {
        if (player != null && isTriggerOn == false) //null�� �ƴ϶��. �÷��̾� ��ġ�� �̵��ؾ���. 
        {
            targetPos = player.transform.position;
            float speed = Random.Range(minMoveSpeed, maxMoveSpeed); //�������� ���ǵ带 �����ϰ� ����. 
            Vector3 direction=(targetPos-transform.position).normalized; //���� ����. 
            transform.Translate(direction*Time.deltaTime*speed);
        }
    }


    IEnumerator ControllerCoroutine(Collider other) //�˹� �� �̵��Ұ��� �ϵ��� �ϱ�. 
    {
        if(isPoolOn==false)
        {
            Vector3 direction = other.transform.position - transform.position;
            CharacterController characterController = other.GetComponent<CharacterController>();
            if (characterController != null)
            {
                isPoolOn=true;
                characterController.enabled = false;
                Rigidbody playerRigid = other.GetComponent<Rigidbody>();
                playerRigid.isKinematic = false;
                playerRigid.velocity = Vector3.zero;
                playerRigid.velocity = direction * KnockBackPower;
                yield return new WaitForSeconds(0.7f);
                playerRigid.isKinematic = true;
                characterController.enabled = true;
            }
        }
       
    }
    //Ʈ���� + �˹豸�� 

    private void OnTriggerEnter(Collider other)
    {
        if (Extension.Contain(playerLayer,other.gameObject.layer))  //�÷��̾���. ������ �ֱ�. 
        {
            PlayerHp.Player_Action(damage);
            StartCoroutine(ControllerCoroutine(other));


        }
    }



    private void OnTriggerExit(Collider other)
    {
        

        if (other.gameObject.CompareTag("Fire"))
        {
            StartCoroutine(Delay());
            Debug.Log("fire���� exit");
            isTriggerOn = true; //�̵��� �������. 

            CharacterController characterController = other.GetComponent<CharacterController>();
            if (characterController != null)
            {
                Rigidbody playerRigid = other.GetComponent<Rigidbody>();  
                playerRigid.isKinematic = true;
                characterController.enabled = true;
            }



            pooledObject.Release();


        }
    }

    //�ڷ�ƾ���� ��� ��� �����ֱ�.

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1f);
    }
   


}
