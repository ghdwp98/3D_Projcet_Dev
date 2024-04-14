using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MainSceneButton : MonoBehaviour
{
    string sceneName;

    private void Start()
    {
        sceneName = PlayerPrefs.GetString("LastScene");      //저장 된 씬
        Debug.Log(sceneName);
    }


    public void ch1Load()  //start버튼 누르면 저장 데이터 초기화시키기. 
    {
        PlayerPrefs.DeleteAll();

        Manager.Scene.LoadScene("1MAPJaehoon");
    }

    public void ContinueBtn()
    {
        Manager.Scene.LoadScene(sceneName); //저장된 씬을 로드함. 
    }


    public void On0ClickExit()  //quit 버튼 
    {
        Application.Quit();
    }
}
