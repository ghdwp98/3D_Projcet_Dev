using JJH;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingStone : MonoBehaviour
{
    //������� ������ �������δٰ�
    PooledObject pooledObject; //Auto Release ������ �Ƚᵵ �ɰ�

    Rigidbody rigid;

    


    private void OnEnable()  //spawner���� �����ϰ� player ���� ��ġ�� �̵���Ű��. 
    {
        //������ �ִϸ��̼� �� ����ϰ�
        rigid= GetComponent<Rigidbody>();
        


    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            PlayerHp.Player_Action(10); //10���� ������ ���� . 
        }
    }

}
