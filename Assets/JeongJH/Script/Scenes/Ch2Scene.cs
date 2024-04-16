using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ch2Scene : BaseScene
{
    //������ƮǮ�� ... �÷��̾� üũ����Ʈ �� �ʿ��� ��ɵ� �ε� ��ƾ�� �־��ֱ�. 

    [SerializeField] GameObject player;
    [SerializeField] GameObject NPC;  //����޽������� tranform �̵��� �ȵǴµ��ϴ�. 
    [SerializeField] CharacterController controller;
    [SerializeField] PooledObject FirePrefab;
    [SerializeField] PooledObject smallFirePrefab;
    [SerializeField] int size;
    [SerializeField] int capacity;
    [SerializeField] int smallSize;
    [SerializeField] int smallCapacity;

	[SerializeField] PopUpUI escPopUPUI;

	//�Ʊ� ��ҵ��� ���� �����Ǿ�� �ϴµ� �� ��ġ�� �����ִ°� �� ������Ʈ�� �־�ѱ�? 

	public override IEnumerator LoadingRoutine()
    {

        /*Manager.Pool.CreatePool(FirePrefab, size, capacity);
        Manager.Pool.CreatePool(smallFirePrefab, smallSize, smallCapacity);*/

        if (GameManager.saved == false)
            yield break;


        if (GameManager.isChangeScene == false)
        {
            //���⼭ ��ġ ���� ��������? ���� Ȯ�� �� ��. 
            controller.enabled = false;
            player.transform.position = GameManager.playerPos + new Vector3(1, 0, 1);
            NavMeshAgent navMeshAgent = NPC.GetComponent<NavMeshAgent>(); //npc�� �׺�Ž��� �����ͼ�
            if (navMeshAgent != null)
            {
                navMeshAgent.Warp(GameManager.playerPos);
            }
            Debug.Log(NPC.transform.position);

            Debug.Log(GameManager.playerPos);
            controller.enabled = true;
            yield return null;
        }

        GameManager.isChangeScene = false;
        yield return null;



    }


    private void Update()
    {
        //���ξ��� �ƴҶ��� escŰ �̿밡��. 
        if (Input.GetKeyDown(KeyCode.Escape) && UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "MainScene")
        {
            Debug.Log("��Ű ��");
            Manager.UI.ShowPopUpUI(escPopUPUI); //ESC�˾� UI 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) //�÷��̾ ������ 3é�� �� �ε�... 
        {
            GameManager.isChangeScene = true;
            Manager.Scene.LoadScene("3M");
        }
    }

    private void Awake()
    {
       
        Manager.Pool.CreatePool(FirePrefab, size, capacity);
        Manager.Pool.CreatePool(smallFirePrefab, smallSize, smallCapacity);

        // �� �ѱ�鼭 �ϱ� �������ϱ� �ϴ� ���⼭�ϰ� ���߿� ����. 

    }

    
}
