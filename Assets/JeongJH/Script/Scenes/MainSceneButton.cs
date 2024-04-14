using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MainSceneButton : MonoBehaviour
{
    string sceneName;

    private void Start()
    {
        sceneName = PlayerPrefs.GetString("LastScene");      //���� �� ��
        Debug.Log(sceneName);
    }


    public void ch1Load()  //start��ư ������ ���� ������ �ʱ�ȭ��Ű��. 
    {
        PlayerPrefs.DeleteAll();

        Manager.Scene.LoadScene("1MAPJaehoon");
    }

    public void ContinueBtn()
    {
        Manager.Scene.LoadScene(sceneName); //����� ���� �ε���. 
    }


    public void On0ClickExit()  //quit ��ư 
    {
        Application.Quit();
    }
}
