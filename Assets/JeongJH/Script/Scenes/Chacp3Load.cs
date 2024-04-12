using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chacp3Load : BaseScene
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player")) //플레이어가 닿으면 3챕터 씬 로드... 
        {
            Manager.Scene.LoadScene("3M");
        }
    }



    public override IEnumerator LoadingRoutine()
    {
        yield return null; // 2->3챕터로 넘어감.
    }

   
    
}
