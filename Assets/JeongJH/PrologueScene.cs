using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrologueScene : BaseScene

{
    public override IEnumerator LoadingRoutine()
    {
        Debug.Log("���ѷα׾������̵�");


        yield return null;  //�̰� ���߿� ���� �߰��ؾ� �� �� ������ �߰����ֱ�. 
    }


    private void Update() //�ϴ� �ӽ÷� �ƹ�Ű�� ������ 1é�ͷ� �̵��ϵ��� �ϱ�. 
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Manager.Scene.LoadScene("1MapJaehoon");
        }
    }
}
