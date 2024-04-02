using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscPopUpUI : MonoBehaviour
{
    // ESC Å°¸¦ ´©¸£¸é ÆË¾÷¶ß°í 

    [SerializeField]PopUpUI escPopUPUI;

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Manager.UI.ShowPopUpUI(escPopUPUI);
        }

    }

    public void ReturnGame()
    {
        Manager.UI.ClosePopUpUI();
    }

    public void GmaeQuit()
    {
        Application.Quit();
    }






}
