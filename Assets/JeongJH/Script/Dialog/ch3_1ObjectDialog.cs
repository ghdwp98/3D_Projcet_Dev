using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ch3_1ObjectDialog : MonoBehaviour
{
    [SerializeField] int count;//�ڽ��� ī��Ʈ�� ���� ��ȭ�α׸� �ҷ���. 
    [SerializeField] ch3_1Dialog ch3_1Dialog;
    SphereCollider sphereCollider;
    
    void Start()
    {
        ch3_1Dialog = GameObject.FindWithTag("Dialog").GetComponent<ch3_1Dialog>();
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player")) //player�� Ʈ���� �Ǹ� ��� ������ .. ��� ���� ���Ŀ� Ʈ���Ÿ� ������ҵ�? 
        {
            if(ch3_1Dialog != null) //null�� �ƴϸ� ����. 
            ch3_1Dialog.StartTextCoroutine(count); //�ڽ��� ī��Ʈ�� ��� �ҷ�����. 
            sphereCollider=GetComponent<SphereCollider>();
            Destroy(sphereCollider); //�ݶ��̴� �ı��� �ٽ� ��簡 ������ �ʵ����Ѵ�. 

        }
    }


}
