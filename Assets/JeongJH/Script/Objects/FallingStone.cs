using JJH;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingStone : MonoBehaviour
{
    //언덕에서 굴리는 느낌으로다가
    PooledObject pooledObject; //Auto Release 있으면 안써도 될걸

    Rigidbody rigid;

    


    private void OnEnable()  //spawner에서 생성하고 player 위의 위치로 이동시키자. 
    {
        //있으면 애니메이션 등 재생하고
        rigid= GetComponent<Rigidbody>();
        


    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            PlayerHp.Player_Action(10); //10정도 데미지 주죵 . 
        }
    }

}
