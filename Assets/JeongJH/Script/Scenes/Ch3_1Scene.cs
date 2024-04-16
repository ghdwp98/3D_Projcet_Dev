using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ch3_1Scene : BaseScene
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


    private void OnTriggerEnter(Collider other)  //트리거로 씬 전환한다면 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.isSceneChange = true;
            Debug.Log("3_1층 ->3_2층 챕터변경" + GameManager.isSceneChange);
            Manager.Scene.LoadScene("3M3");
        }
            

    }



    public override IEnumerator LoadingRoutine()
    {
        Debug.Log("3씬_1층 진입 로딩루틴 " + GameManager.isSceneChange);
        GameManager.isSceneChange = false;
        Debug.Log("3씬_1층 진입 로딩루틴 false 변경 성공적인지 " + GameManager.isSceneChange);
        yield return null;
    }
}
