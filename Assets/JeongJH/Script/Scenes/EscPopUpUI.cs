using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class EscPopUpUI : MonoBehaviour
{
    // ESC Å°¸¦ ´©¸£¸é ÆË¾÷¶ß°í 

    [SerializeField]PopUpUI escPopUPUI;
    [SerializeField] PopUpUI soundPopUPUI;
    
    
    public void SoundBtn()
    {
        Manager.UI.ShowPopUpUI(soundPopUPUI);
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
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






}
