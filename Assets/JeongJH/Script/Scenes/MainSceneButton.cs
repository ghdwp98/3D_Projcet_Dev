using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MainSceneButton : MonoBehaviour
{
    public void MainSceneLoad()
    {
        Manager.Scene.LoadScene("TutorialScene");
    }


    public void On0ClickExit()
    {
        Application.Quit();
    }
}
