using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ch2Scene : BaseScene
{
    //오브젝트풀링 ... 플레이어 체크포인트 등 필요한 기능들 로딩 루틴에 넣어주기. 

    [SerializeField] GameObject player;
    [SerializeField] GameObject NPC;  //내비메쉬때문에 tranform 이동이 안되는듯하다. 
    [SerializeField] CharacterController controller;
    [SerializeField] PooledObject FirePrefab;
    [SerializeField] PooledObject smallFirePrefab;
    [SerializeField] int size;
    [SerializeField] int capacity;
    [SerializeField] int smallSize;
    [SerializeField] int smallCapacity;
	[SerializeField] AudioClip bgmClip;

	[SerializeField] PopUpUI escPopUPUI;

	//아기 산불들은 들어가면 생성되어야 하는데 그 위치를 정해주는건 빈 오브젝트를 넣어둘까? 

	private void Start()
	{
		Manager.Sound.PlayBGM(bgmClip);
	}


	public override IEnumerator LoadingRoutine()
    {

        /*Manager.Pool.CreatePool(FirePrefab, size, capacity);
        Manager.Pool.CreatePool(smallFirePrefab, smallSize, smallCapacity);*/

        if (GameManager.saved == false)
            yield break;

        //여기서 위치 저장 가능한지? 순서 확인 할 것. 
        controller.enabled = false;
        player.transform.position = GameManager.playerPos + new Vector3(1, 0, 1);
        NavMeshAgent navMeshAgent=NPC.GetComponent<NavMeshAgent>(); //npc의 네비매쉬를 가져와서
        if (navMeshAgent != null)
        {
            navMeshAgent.Warp(GameManager.playerPos);
        }
        Debug.Log(NPC.transform.position);

        Debug.Log(GameManager.playerPos);
        controller.enabled = true;

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) //플레이어가 닿으면 3챕터 씬 로드... 
        {
            Manager.Scene.LoadScene("3M");
        }
    }

    private void Awake()
    {
       
        Manager.Pool.CreatePool(FirePrefab, size, capacity);
        Manager.Pool.CreatePool(smallFirePrefab, smallSize, smallCapacity);

        // 씬 넘기면서 하기 귀찮으니까 일단 여기서하고 나중에 변경. 

    }

    
}
