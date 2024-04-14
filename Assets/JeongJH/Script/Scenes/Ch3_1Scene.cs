using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ch3_1Scene : BaseScene
{
    //3¸Ê 1Ãþ ¾À ·Îµù. 


    public override IEnumerator LoadingRoutine()
    {
        PlayerPrefs.SetString("LastScene",
            Manager.Scene.GetCurSceneName());
        yield return null;
    }
}
