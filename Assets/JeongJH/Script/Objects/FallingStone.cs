using JJH;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingStone : MonoBehaviour
{
    //������� ������ �������δٰ�
    PooledObject pooledObject; //Auto Release 5��. 

    Rigidbody rigid;

    


    private void OnEnable()  //spawner���� �����ϰ� player ���� ��ġ�� �̵���Ű��. 
    {
        //�������� ������ ������ְ�. 
        rigid= GetComponent<Rigidbody>();
        


    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            PlayerHp.Player_Action(10); //10���� ������ ���� .
            pooledObject.Release(); //������ �׳� ����������. 
        }
    }

}
