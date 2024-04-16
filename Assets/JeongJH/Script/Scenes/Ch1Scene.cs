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

	public override IEnumerator LoadingRoutine()  
    {

        if (GameManager.saved == false)
            yield break;

        //여기서 위치 저장 가능한지? 순서 확인 할 것. 

        if(GameManager.isSceneChange==false)
        {
            controller.enabled = false;
            player.transform.position = GameManager.playerPos + new Vector3(1, 0, 1);
            Debug.Log(GameManager.playerPos);
            controller.enabled = true;
            yield return null;
        }

        GameManager.isSceneChange = false; //루팅 진행 후 false로 변경. 
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


    private void OnTriggerEnter(Collider other) //트리거로 2맵 진입. 
	{
		if (other.gameObject.CompareTag("Player")) //2맵으로 넘어가게 된다.
		{
            GameManager.isSceneChange = true; // 이거를 잠시 트루로 두고 2m 로딩루틴에서 false일때만 playerpos를 진행하도록 하고 다시 false 로 변경 

            Debug.Log("1 -> 2 scene " + GameManager.isSceneChange);
            Manager.Scene.LoadScene("2M");
		}
	}

}
