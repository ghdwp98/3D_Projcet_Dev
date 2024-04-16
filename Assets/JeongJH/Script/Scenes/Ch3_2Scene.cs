using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ch3_2Scene : BaseScene
{
    [SerializeField] PopUpUI escPopUPUI;

    private void Update()
    {
        //메인씬이 아닐때만 esc키 이용가능. 
        if (Input.GetKeyDown(KeyCode.Escape) && UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "MainScene")
        {
            Debug.Log("겟키 들어감");
            Manager.UI.ShowPopUpUI(escPopUPUI); //ESC팝업 UI 
        }
    }

    public override IEnumerator LoadingRoutine()
    {
        Debug.Log("3-2층 로딩루틴 여기서 진행할 내용은 존재하는지?");
        GameManager.isSceneChange = false;  //false로 풀어줘야 하는지? 

        yield return null;
    }
}
