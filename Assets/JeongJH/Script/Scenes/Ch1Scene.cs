using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class Ch1Scene :BaseScene
{
    //���� ����. 

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

	public override IEnumerator LoadingRoutine()  //static �� false �� ���� ���� 
    {

        if (GameManager.saved == false)
            yield break;


        if (GameManager.isChangeScene ==false)
        {
            //���⼭ ��ġ ���� ��������? ���� Ȯ�� �� ��. 
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
        //���ξ��� �ƴҶ��� escŰ �̿밡��. 
        if (Input.GetKeyDown(KeyCode.Escape) && UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "MainScene")
        {
            Debug.Log("��Ű ��");
            Manager.UI.ShowPopUpUI(escPopUPUI); //ESC�˾� UI 
        }
    }


    private void OnTriggerEnter(Collider other) //���⼭ bool ���� true�� ���� 
	{
		if (other.gameObject.CompareTag("Player"))
		{
            GameManager.isChangeScene = true;
			Manager.Scene.LoadScene("2M");
		}
	}

}
