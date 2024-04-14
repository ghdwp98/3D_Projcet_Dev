using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.CompilerServices;


public class EscPopUpUI : MonoBehaviour
{
    // ESC 키를 누르면 팝업뜨고 

    [SerializeField]PopUpUI escPopUPUI;
   

    public void SaveData()
    {
        PlayerPrefs.SetString("LastScene",
            Manager.Scene.GetCurSceneName()); //현재씬을 저장해놓음. 
    }


   
    //씬 재로드 팝업 
    public void RestartScene() //씬 재로드 (씬 다시 시작 ) 
    {
        Manager.UI.ClearPopUpUI();
        Manager.Scene.LoadScene(Manager.Scene.GetCurSceneName());
    }
    public void ReturnGame() // 팝업 전부 끄기 --> 다시 게임화면으로 
    {       
        Manager.UI.ClearPopUpUI();
    }

    public void GmaeQuit() //게임종료
    {
        Manager.UI.ClearPopUpUI();
        Application.Quit();
    }

    public void MainScene() //메인화면으로 이동. 
    {
        Manager.UI.ClearPopUpUI();
        Manager.Scene.LoadScene("MainScene");
    }
}
