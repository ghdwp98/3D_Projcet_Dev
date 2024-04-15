using JJH;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    //오디오 재생 + 플레이어 넉백 +데미지 + player위치로 이동. 
    // 움직이는 fire ++ samll 고정 fire는 다른 스크립트로 제어해주기. 
    // ++ 구역을 벗어나면 더이상 쫓지 않아야 하는데 어떻게 처리해줄지? 

    AudioSource audioSource;
    GameObject player;
    Vector3 targetPos;
    bool isTriggerOn;
    bool isPoolOn;
    [SerializeField] float minMoveSpeed; // 이동 속도. 나중에 조절해주기. 
    [SerializeField] float maxMoveSpeed;
    [SerializeField] PooledObject pooledObject;
    [SerializeField]LayerMask playerLayer;
    [SerializeField] float KnockBackPower; //넉백 조금 강하게 해주기. 
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
        if (player != null && isTriggerOn == false) //null이 아니라면. 플레이어 위치로 이동해야함. 
        {
            targetPos = player.transform.position;
            float speed = Random.Range(minMoveSpeed, maxMoveSpeed); //순간순간 스피드를 랜덤하게 변경. 
            Vector3 direction=(targetPos-transform.position).normalized; //방향 벡터. 
            transform.Translate(direction*Time.deltaTime*speed);
        }
    }


    IEnumerator ControllerCoroutine(Collider other) //넉백 및 이동불가능 하도록 하기. 
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
    //트리거 + 넉배구현 

    private void OnTriggerEnter(Collider other)
    {
        if (Extension.Contain(playerLayer,other.gameObject.layer))  //플레이어라면. 데미지 주기. 
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
            Debug.Log("fire에서 exit");
            isTriggerOn = true; //이동을 멈춰버림. 

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

    //코루틴으로 잠시 대기 시켜주기.

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1f);
    }
   


}
