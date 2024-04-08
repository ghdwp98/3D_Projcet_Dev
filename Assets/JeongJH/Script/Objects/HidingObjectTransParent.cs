using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingObjectTransParent : MonoBehaviour
{
    BoxCollider boxCollider; //콜라이더 -->상호작용되면 삭제해주고 trigger 벗어나면 다시 생김. 
    Material material;
    
    [SerializeField]LayerMask playerLayer;
    

    //오브젝트 타입 결정은 나중에해주자... 리팩토링이 가능하다면.

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        material= GetComponent<MeshRenderer>().material;
        material.color = new Color32(255, 255, 255, 255);
        
 
    }


    private void Update()
    {
        
    }



    private void OnTriggerEnter(Collider other) //제대로 들어가지 못하는 이유가? 
    { 
        if(other.gameObject.CompareTag("Player")) //comparetag가 훨씬 성능좋음. 
        {
            Debug.Log("플레이어트리거");
            
            if(Input.GetKeyDown(KeyCode.X))
            {
                boxCollider.enabled = false;
                CharacterController characterController = other.gameObject.GetComponent<CharacterController>();
                if(characterController != null)
                {
                    characterController.enabled = false;
                    other.gameObject.transform.position = transform.position;
                    characterController.enabled = true;                    
                    material.color = new Color32(255, 255, 255, 150);
                    other.gameObject.layer = 0;
                   
                }
                
                
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("트리거탈출");
        if (other.gameObject.CompareTag("Player")) //comparetag가 훨씬 성능좋음.
        {
            boxCollider.enabled = true;
            material.color = new Color32(255, 255, 255, 255);
            other.gameObject.layer = 30; //이 부분 수정 필요. layer가 1번으로 바뀜... 원래 레이어를 유지할 수 있도록 해줘야함. 

        }
    }
}
