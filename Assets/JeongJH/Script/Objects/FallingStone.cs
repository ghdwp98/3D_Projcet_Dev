using JJH;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingStone : MonoBehaviour
{
    //언덕에서 굴리는 느낌으로다가
    [SerializeField]PooledObject pooledObject; //Auto Release 5초. 
    [SerializeField] LayerMask playerLayer;
    [SerializeField] float KnockBackPower;
    Rigidbody rigid;

    


    private void OnEnable()  //spawner에서 생성하고 player 위의 위치로 이동시키자. 
    {
        //여러가지 있으면 재생해주고. 
        rigid= GetComponent<Rigidbody>();
        KnockBackPower = 5f;


    }

    private void OnCollisionEnter(Collision collision) //무적일 때는 공격안받아야 하니까 레이어로 체크하자. 
    {
        if (Extension.Contain(playerLayer,collision.gameObject.layer))
        {
            PlayerHp.Player_Action(10); //10정도 데미지 주죵 .
            StartCoroutine(ControllerCoroutine(collision));

        }
    }

    IEnumerator ControllerCoroutine(Collision collision)
    {
        PlayerHp.Player_Action(10); //10정도 데미지 주죵 .
        Vector3 direction = collision.gameObject.transform.position - transform.position;
        CharacterController characterController = collision.gameObject.GetComponent<CharacterController>();
        if (characterController != null)
        {
            characterController.enabled = false;
            Rigidbody playerRigid = collision.gameObject.GetComponent<Rigidbody>();
            playerRigid.isKinematic = false;
            playerRigid.velocity = Vector3.zero;
            playerRigid.velocity = direction * KnockBackPower;

            //반복기 없이 바로 자제 다시 잡을 수 있게 해보기 . 
            yield return new WaitForSeconds(0.5f);
            playerRigid.isKinematic = true;
            characterController.enabled = true;
        }
    }




}
