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
        //���⼭ ������ �ʱ�ȭ ���ְ�. 
    }

    

    //Ʈ���� + �˹豸�� 

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer==LayerMask.NameToLayer("PPP")) //�ӽ÷� ppp �̿�. 
        {
            PlayerHp.Player_Action(damage);
            Vector3 direction = other.transform.position - transform.position;
                
            Rigidbody playerRigid=other.GetComponent<Rigidbody>();
            playerRigid.velocity = Vector3.zero;
            playerRigid.velocity = direction * KnockBackPower;
        }
    }

   
    



}
