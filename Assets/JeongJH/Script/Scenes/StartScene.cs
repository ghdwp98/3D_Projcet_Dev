using System.Collections;
using UnityEngine;

public class StartScene : BaseScene
{
    public override IEnumerator LoadingRoutine()
    {
        yield return null;
    }


    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Manager.Scene.LoadScene("BaseScene");
        }

    }
}



