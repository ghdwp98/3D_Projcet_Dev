using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    //트리거 시 player 위치 이동 . 

    [SerializeField]LayerMask playerMask;
    [SerializeField] Vector3 newPosition; //새로운 위치 저장 해주기. 
    

    private void OnTriggerEnter(Collider other)
    {
        if(Extension.Contain(playerMask,other.gameObject.layer))
        {
            other.gameObject.transform.position = newPosition;
        }
    }



}
