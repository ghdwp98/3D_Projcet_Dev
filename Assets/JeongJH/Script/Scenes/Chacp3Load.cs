using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chacp3Load : BaseScene
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player")) //�÷��̾ ������ 3é�� �� �ε�... 
        {
            //���� �� �̸� Ȯ������ ����.. 
        }
    }



    public override IEnumerator LoadingRoutine()
    {
        yield return null; // 2->3é�ͷ� �Ѿ.
    }

   
    
}
