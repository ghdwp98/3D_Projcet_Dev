using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrologueScene : BaseScene

{
    public override IEnumerator LoadingRoutine()
    {
        Debug.Log("프롤로그씬으로이동");


        yield return null;  //이거 나중에 만약 추가해야 할 거 있으면 추가해주기. 
    }


    private void Update() //일단 임시로 아무키나 누르면 1챕터로 이동하도록 하기. 
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Manager.Scene.LoadScene("1MapJaehoon");
        }
    }
}
