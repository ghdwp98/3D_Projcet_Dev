using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ch3Scene : BaseScene
{
    //3�� �� �ٱ� �� �ε�. 

    


    public override IEnumerator LoadingRoutine()
    {
        PlayerPrefs.SetString("LastScene",
            Manager.Scene.GetCurSceneName());

        yield return null;
    }
}
