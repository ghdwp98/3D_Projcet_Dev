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

    

    //트리거 + 넉배구현 

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer==LayerMask.NameToLayer("PPP")) //임시로 ppp 이용. 
        {
            PlayerHp.Player_Action(damage);
            Vector3 direction = other.transform.position - transform.position;
                
            Rigidbody playerRigid=other.GetComponent<Rigidbody>();
            playerRigid.velocity = Vector3.zero;
            playerRigid.velocity = direction * KnockBackPower;
        }
    }

   
    



}
