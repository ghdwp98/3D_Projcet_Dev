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
    //SerializeField] PopUpUI soundPopUPUI;
    
    
    public void SoundBtn()
    {
        //Manager.UI.ShowPopUpUI(soundPopUPUI);
    }

    public void Update()
    {
        
        //���ξ��� �ƴҶ��� escŰ �̿밡��. 
        if (Input.GetKeyDown(KeyCode.Escape)&& UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "MainScene")
        {
            Debug.Log("��Ű ��");
            Manager.UI.ShowPopUpUI(escPopUPUI); //ESC�˾� UI 
        }

    }

    

    //�� ��ε� �˾� 
    public void RestartGameScene() //�� ��ε� (�� �ٽ� ���� ) 
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnGame() // �˾� ���� ���� -->�ٽ� ���� ����. 
    {       
        Manager.UI.ClearPopUpUI();
    }

    public void GmaeQuit() //��������
    {
        Application.Quit();
    }

    public void MainReturnButton() //�˾� 1�� ���� 
    {
        Manager.UI.ClosePopUpUI(); 
    }





}
