using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakingPanel : MonoBehaviour
{
    //플레이어와 접촉하면 낙하시작 하는 나무판자. 
    // 충돌 안될 시 player 하단에 trigger 설치해서 trigger 판단하면 됨. 
    
    Rigidbody rigid;
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        rigid.useGravity = false;
    }

    void Update()
    {
        
    }


    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("콜라이더 충돌");
        if(other.gameObject.CompareTag("GroundChecking")) //플레이어 발 밑에 달려있는 트리거의 태그. 
        {
            Debug.Log("진입");
            rigid.useGravity = true;
            Destroy(gameObject, 5f);
        }
    }
}
