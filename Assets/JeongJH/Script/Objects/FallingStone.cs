using JJH;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingStone : MonoBehaviour
{
    //������� ������ �������δٰ�
    [SerializeField]PooledObject pooledObject; //Auto Release 5��. 
    [SerializeField] LayerMask playerLayer;
    [SerializeField] float KnockBackPower;
    Rigidbody rigid;

    


    private void OnEnable()  //spawner���� �����ϰ� player ���� ��ġ�� �̵���Ű��. 
    {
        //�������� ������ ������ְ�. 
        rigid= GetComponent<Rigidbody>();
        KnockBackPower = 5f;


    }

    private void OnCollisionEnter(Collision collision) //������ ���� ���ݾȹ޾ƾ� �ϴϱ� ���̾�� üũ����. 
    {
        if (Extension.Contain(playerLayer,collision.gameObject.layer))
        {
            PlayerHp.Player_Action(10); //10���� ������ ���� .
            StartCoroutine(ControllerCoroutine(collision));

        }
    }

    IEnumerator ControllerCoroutine(Collision collision)
    {
        PlayerHp.Player_Action(10); //10���� ������ ���� .
        Vector3 direction = collision.gameObject.transform.position - transform.position;
        CharacterController characterController = collision.gameObject.GetComponent<CharacterController>();
        if (characterController != null)
        {
            characterController.enabled = false;
            Rigidbody playerRigid = collision.gameObject.GetComponent<Rigidbody>();
            playerRigid.isKinematic = false;
            playerRigid.velocity = Vector3.zero;
            playerRigid.velocity = direction * KnockBackPower;

            //�ݺ��� ���� �ٷ� ���� �ٽ� ���� �� �ְ� �غ��� . 
            yield return new WaitForSeconds(0.5f);
            playerRigid.isKinematic = true;
            characterController.enabled = true;
        }
    }




}
