using JJH;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class DefalutDamageObjs : MonoBehaviour
{
    public float damage;
    public float KnockBackPower;

    
    void Start()
    {
        //여기서 데미지 초기화 해주고. 
    }

    
    IEnumerator ControllerCoroutine(Collider other)
    {
        Vector3 direction = other.transform.position - transform.position;
        CharacterController characterController = other.GetComponent<CharacterController>();
        if (characterController != null)
        {
            characterController.enabled = false;
            Rigidbody playerRigid = other.GetComponent<Rigidbody>();
            playerRigid.isKinematic= false;
            playerRigid.velocity = Vector3.zero;
            playerRigid.velocity = direction * KnockBackPower;
            yield return new WaitForSeconds(0.7f);
            playerRigid.isKinematic = true;
            characterController.enabled = true;
        }
    }
    //트리거 + 넉배구현 

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer==LayerMask.NameToLayer("Player")) //플레이어라면. 데미지 주기. 
        {
            PlayerHp.Player_Action(damage);
            StartCoroutine(ControllerCoroutine(other));

        }
    }

    
    



}
