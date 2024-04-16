using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ch3_2ObjectDialog : MonoBehaviour
{

    [SerializeField] int count;//�ڽ��� ī��Ʈ�� ���� ��ȭ�α׸� �ҷ���. 
    [SerializeField] ch3_2Dialog ch3_2Dialog;
    SphereCollider sphereCollider;

    [SerializeField] GameObject floorDiaTrigger;

    void Start()
    {
        ch3_2Dialog = GameObject.FindWithTag("Dialog").GetComponent<ch3_2Dialog>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) //player�� Ʈ���� �Ǹ� ��� ������ .. ��� ���� ���Ŀ� Ʈ���Ÿ� ������ҵ�? 
        {
            if (ch3_2Dialog != null) //null�� �ƴϸ� ����. 
            {
                floorDiaTrigger.SetActive(true);
                ch3_2Dialog.StartTextCoroutine(count); //�ڽ��� ī��Ʈ�� ��� �ҷ�����. 
            }
                
            sphereCollider = GetComponent<SphereCollider>();
            Destroy(sphereCollider); //�ݶ��̴� �ı��� �ٽ� ��簡 ������ �ʵ����Ѵ�. 

        }
    }
}
