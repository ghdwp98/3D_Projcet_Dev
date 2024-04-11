using UnityEngine;

public class HidingObjectTransParent : MonoBehaviour
{
    BoxCollider boxCollider; //콜라이더 -->상호작용되면 삭제해주고 trigger 벗어나면 다시 생김. 
    Material material;

    [SerializeField] LayerMask playerLayer;


    //오브젝트 타입 결정은 나중에해주자... 리팩토링이 가능하다면.

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        material = GetComponent<MeshRenderer>().material;
        material.color = new Color32(255, 255, 255, 255);

    }

    private void Update()
    {

    }

    private void OnTriggerStay(Collider other) //제대로 들어가지 못하는 이유가? 
    {
        if (other.gameObject.CompareTag("Player") && Input.GetKeyDown(KeyCode.X)) //comparetag가 훨씬 성능좋음. 
        {     
            boxCollider.enabled = false;
            CharacterController characterController = other.gameObject.GetComponent<CharacterController>();
            if (characterController != null)
            {
                characterController.enabled = false;
                other.gameObject.transform.position = transform.position;
                characterController.enabled = true;
                material.color = new Color32(255, 255, 255, 150);
                other.gameObject.layer = 28; //hide 레이어로 변경해보기. 

            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) //comparetag가 훨씬 성능좋음.
        {
            boxCollider.enabled = true;
            material.color = new Color32(255, 255, 255, 255);
            other.gameObject.layer = 9; // 아 이게 문제가 뭐냐면 트리거가 tag가 player인데... 이게 
            // 지금 플레이어로 바뀌어서 그럼. --> 바닥에 있어서 exit이 되어버림. 

        }
    }
}
