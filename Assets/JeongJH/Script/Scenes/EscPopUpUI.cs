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
            Manager.UI.ShowPopUpUI(escPopUPUI);
        }

    }

    public void ReturnGame()
    {       
        Manager.UI.ClearPopUpUI();
    }

    public void GmaeQuit()
    {
        Application.Quit();
    }

    public void MainReturnButton()
    {
        Manager.UI.ClosePopUpUI();
    }




}
