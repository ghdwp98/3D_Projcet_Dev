using Cinemachine;
using System.Collections;
using UnityEngine;

public class HideObject : MonoBehaviour
{
    //충돌체 가지고 있고.
    // 상호작용 키를 누르면 충돌체 사라지고 트리거?로 변하고
    //트리거 내에 player가 있으면 player의 layer를 변경해주고. --> 찾을 수 없도록 
    // 플레이어의 위치를 tranform.positino으로 강제 이동시켜서 플레이어와 정확히 일치하도록하고
    // 카메라가 플레이어를 보지 못하도록 이단 카메라 설치 

    [SerializeField] BoxCollider boxCollier;
    [SerializeField] CinemachineVirtualCamera Vcam;
    MeshRenderer mesh;
    CharacterController characterController;
    GameObject player;
    int originLayer;
    bool isHide;

    private void Start()
    {
        boxCollier = GetComponent<BoxCollider>();

    }

    private void Update()
    {
        if (isHide)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                Debug.Log("탈출");
                characterController.enabled = true;
                mesh.enabled = true;
                isHide = false;
                player.layer = 30; //그냥 직접 바꿔주는 수밖에 없는것 같은데?? 
                // 이게 여러번 눌려서 down인데도 여러번 눌려서 layer가 0으로 저장되버려. 
            }


        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.X) && isHide == false)
            {
                characterController = other.gameObject.GetComponent<CharacterController>();
                if (characterController != null)
                {
                    characterController.enabled = false;
                }
                mesh = other.gameObject.GetComponent<MeshRenderer>();
                if (mesh != null)
                {
                    mesh.enabled = false;
                }
                player= other.gameObject;
                other.gameObject.layer = 0; //글로벌매트릭스에서 damage/monster 등과 효과없어지는 레이어로변경. 
                StartCoroutine(HideRoutine());

            }
        }
    }

    IEnumerator HideRoutine()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        isHide = true;
    }












}
