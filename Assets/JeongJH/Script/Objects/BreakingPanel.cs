using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakingPanel : MonoBehaviour
{
    //�÷��̾�� �����ϸ� ���Ͻ��� �ϴ� ��������. 
    // �浹 �ȵ� �� player �ϴܿ� trigger ��ġ�ؼ� trigger �Ǵ��ϸ� ��. 
    
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
        Debug.Log("�ݶ��̴� �浹");
        if(other.gameObject.CompareTag("GroundChecking")) //�÷��̾� �� �ؿ� �޷��ִ� Ʈ������ �±�. 
        {
            Debug.Log("����");
            rigid.useGravity = true;
            Destroy(gameObject, 5f);
        }
    }
}
