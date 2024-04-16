using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingScene : BaseScene
{
    float time;
    // ¿£µù ¾À ÀüÈ¯. 
    public override IEnumerator LoadingRoutine()
    {
        yield return null; 
    }

	public void Update()
	{
		if(Time.timeScale != 0)
        {
            time += Time.deltaTime;
            if(time > 5f)
            {
                Manager.Scene.LoadScene("MainScene");            }
        }
	}


}
