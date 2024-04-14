using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.CompilerServices;


public class EscPopUpUI : MonoBehaviour
{
    // ESC Ű�� ������ �˾��߰� 

    [SerializeField]PopUpUI escPopUPUI;
   

    public void SaveData()
    {
        PlayerPrefs.SetString("LastScene",
            Manager.Scene.GetCurSceneName()); //������� �����س���. 
    }


   
    //�� ��ε� �˾� 
    public void RestartScene() //�� ��ε� (�� �ٽ� ���� ) 
    {
        Manager.UI.ClearPopUpUI();
        Manager.Scene.LoadScene(Manager.Scene.GetCurSceneName());
    }
    public void ReturnGame() // �˾� ���� ���� --> �ٽ� ����ȭ������ 
    {       
        Manager.UI.ClearPopUpUI();
    }

    public void GmaeQuit() //��������
    {
        Manager.UI.ClearPopUpUI();
        Application.Quit();
    }

    public void MainScene() //����ȭ������ �̵�. 
    {
        Manager.UI.ClearPopUpUI();
        Manager.Scene.LoadScene("MainScene");
    }
}
