using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ch3_2Scene : BaseScene
{
    [SerializeField] PopUpUI escPopUPUI;

    private void Update()
    {
        //���ξ��� �ƴҶ��� escŰ �̿밡��. 
        if (Input.GetKeyDown(KeyCode.Escape) && UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "MainScene")
        {
            Debug.Log("��Ű ��");
            Manager.UI.ShowPopUpUI(escPopUPUI); //ESC�˾� UI 
        }
    }

    public override IEnumerator LoadingRoutine()
    {
        
        yield return null;
    }
}
