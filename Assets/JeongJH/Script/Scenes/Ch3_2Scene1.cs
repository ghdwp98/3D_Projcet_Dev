using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ch3_2Scene : BaseScene
{
    //3�� 2��  �� �ε�. 


    public override IEnumerator LoadingRoutine()
    {
        PlayerPrefs.SetString("LastScene",
            Manager.Scene.GetCurSceneName());
        yield return null;
    }
}
