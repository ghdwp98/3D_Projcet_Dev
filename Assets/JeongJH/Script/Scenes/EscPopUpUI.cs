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
    //SerializeField] PopUpUI soundPopUPUI;
    
    
    public void SoundBtn()
    {
        //Manager.UI.ShowPopUpUI(soundPopUPUI);
    }

    public void Update()
    {
        
        //메인씬이 아닐때만 esc키 이용가능. 
        if (Input.GetKeyDown(KeyCode.Escape)&& UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "MainScene")
        {
            Debug.Log("겟키 들어감");
            Manager.UI.ShowPopUpUI(escPopUPUI); //ESC팝업 UI 
        }

    }

    

    //씬 재로드 팝업 
    public void RestartGameScene() //씬 재로드 (씬 다시 시작 ) 
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnGame() // 팝업 전부 끄기 -->다시 시작 느낌. 
    {       
        Manager.UI.ClearPopUpUI();
    }

    public void GmaeQuit() //게임종료
    {
        Application.Quit();
    }

    public void MainReturnButton() //팝업 1개 끄기 
    {
        Manager.UI.ClosePopUpUI(); 
    }





}
