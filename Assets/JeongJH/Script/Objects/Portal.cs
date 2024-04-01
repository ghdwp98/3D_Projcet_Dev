using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    //Ʈ���� �� player ��ġ �̵� . 

    [SerializeField]LayerMask playerMask;
    [SerializeField] Vector3 newPosition; //���ο� ��ġ ���� ���ֱ�. 
    

    private void OnTriggerEnter(Collider other)
    {
        if(Extension.Contain(playerMask,other.gameObject.layer))
        {
            other.gameObject.transform.position = newPosition;
        }
    }



}
