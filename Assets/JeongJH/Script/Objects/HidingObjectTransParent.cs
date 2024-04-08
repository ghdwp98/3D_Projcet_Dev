using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingObjectTransParent : MonoBehaviour
{
    BoxCollider boxCollider; //�ݶ��̴� -->��ȣ�ۿ�Ǹ� �������ְ� trigger ����� �ٽ� ����. 
    Material material;
    
    [SerializeField]LayerMask playerLayer;
    

    //������Ʈ Ÿ�� ������ ���߿�������... �����丵�� �����ϴٸ�.

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        material= GetComponent<MeshRenderer>().material;
        material.color = new Color32(255, 255, 255, 255);
        
 
    }


    private void Update()
    {
        
    }



    private void OnTriggerEnter(Collider other) //����� ���� ���ϴ� ������? 
    { 
        if(other.gameObject.CompareTag("Player")) //comparetag�� �ξ� ��������. 
        {
            Debug.Log("�÷��̾�Ʈ����");
            
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
        Debug.Log("Ʈ����Ż��");
        if (other.gameObject.CompareTag("Player")) //comparetag�� �ξ� ��������.
        {
            boxCollider.enabled = true;
            material.color = new Color32(255, 255, 255, 255);
            other.gameObject.layer = 30; //�� �κ� ���� �ʿ�. layer�� 1������ �ٲ�... ���� ���̾ ������ �� �ֵ��� �������. 

        }
    }
}
