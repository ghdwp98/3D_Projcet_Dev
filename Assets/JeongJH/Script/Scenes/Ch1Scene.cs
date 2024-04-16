using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class Ch1Scene :BaseScene
{
    //번개 생성. 

    [SerializeField] GameObject player;
    [SerializeField] CharacterController controller;
    [SerializeField] int size;
    [SerializeField] int capacity;  
    [SerializeField] PooledObject lightningPrefab;
    [SerializeField] PooledObject dangerCircle;
    [SerializeField] PopUpUI escPopUPUI;

    [SerializeField] AudioClip bgmClip;

	private void Start()
	{
        Manager.Sound.PlayBGM(bgmClip);
	}

	public override IEnumerator LoadingRoutine()  //static 이 false 일 때만 진행 
    {

        if (GameManager.saved == false)
            yield break;


        if (GameManager.isChangeScene ==false)
        {
            //여기서 위치 저장 가능한지? 순서 확인 할 것. 
            controller.enabled = false;
            player.transform.position = GameManager.playerPos + new Vector3(1, 0, 1);
            Debug.Log(GameManager.playerPos);
            controller.enabled = true;
            yield return null;
        }

        GameManager.isChangeScene = false;
        yield return null;
    }

    private void Update()
    {
        //메인씬이 아닐때만 esc키 이용가능. 
        if (Input.GetKeyDown(KeyCode.Escape) && UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "MainScene")
        {
            Debug.Log("겟키 들어감");
            Manager.UI.ShowPopUpUI(escPopUPUI); //ESC팝업 UI 
        }
    }


    private void OnTriggerEnter(Collider other) //여기서 bool 변수 true로 변경 
	{
		if (other.gameObject.CompareTag("Player"))
		{
            GameManager.isChangeScene = true;
			Manager.Scene.LoadScene("2M");
		}
	}

}
