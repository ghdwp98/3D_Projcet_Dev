using JJH;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedFire : MonoBehaviour
{
    //pooled object 

    [SerializeField]LayerMask playerLayer;
    [SerializeField] float KnockBackPower;
    [SerializeField] float damage;

    private void OnEnable()
    {
        
    }

    IEnumerator ControllerCoroutine(Collider other)
    {
        Vector3 direction = other.transform.position - transform.position;
        CharacterController characterController = other.GetComponent<CharacterController>();
        if (characterController != null)
        {
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
    //Ʈ���� + �˹豸�� 

    private void OnTriggerEnter(Collider other)
    {
        if (Extension.Contain(playerLayer,other.gameObject.layer)) //�ӽ÷� ppp �̿�. 
        {
            PlayerHp.Player_Action(damage);
            StartCoroutine(ControllerCoroutine(other));

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (Extension.Contain(playerLayer, other.gameObject.layer)) //�ӽ÷� ppp �̿�. 
        {
            CharacterController characterController = other.GetComponent<CharacterController>();
            if (characterController != null)
            {
                Rigidbody playerRigid = other.GetComponent<Rigidbody>();
                playerRigid.isKinematic = true;
                characterController.enabled = true;
            }
        }
    }


}
