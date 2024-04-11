using Cinemachine;
using System.Collections;
using UnityEngine;

public class LimitHideObject : MonoBehaviour
{

    [SerializeField] CinemachineVirtualCamera Vcam;
    MeshRenderer mesh;
    CharacterController characterController;
    GameObject player;
    bool isHide;

    private void Start()
    {

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
                player.layer = 9; // 다시 기본 player레이어로  
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
        yield return new WaitForSecondsRealtime(0.25f);
        isHide = true;
    }












}
