using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.CompilerServices;
using TMPro;


public class EscPopUpUI : MonoBehaviour
{
    // ESC Ű�� ������ �˾��߰� 

    [SerializeField]PopUpUI escPopUPUI;
    [SerializeField] TextMeshProUGUI text;



    private void Start()
    {
        text.enabled = false;
    }

    public void SaveData()
    {
        PlayerPrefs.SetString("LastScene",
            Manager.Scene.GetCurSceneName()); //������� �����س���. 

        StartCoroutine(OnText());

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


    IEnumerator OnText()
    {
        text.enabled=true;
        yield return new WaitForSecondsRealtime(1f);
        text.enabled = false;
        Manager.UI.ClearPopUpUI();
    }
}
