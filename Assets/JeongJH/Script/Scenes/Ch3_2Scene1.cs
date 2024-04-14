using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ch3_2Scene : BaseScene
{
    //3¸Ê 2Ãþ  ¾À ·Îµù. 


    public override IEnumerator LoadingRoutine()
    {
        PlayerPrefs.SetString("LastScene",
            Manager.Scene.GetCurSceneName());
        yield return null;
    }
}
