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
                Debug.Log("Ż��");
                characterController.enabled = true;
                mesh.enabled = true;
                isHide = false;
                player.layer = 9; // �ٽ� �⺻ player���̾��  
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
                other.gameObject.layer = 0; //�۷ι���Ʈ�������� damage/monster ��� ȿ���������� ���̾�κ���. 
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
