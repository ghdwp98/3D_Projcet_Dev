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


    public override IEnumerator LoadingRoutine()
    {

        if (GameManager.saved == false)
            yield break;

        //���⼭ ��ġ ���� ��������? ���� Ȯ�� �� ��. 
        controller.enabled = false;
        player.transform.position = GameManager.playerPos + new Vector3(1, 0, 1);
        Debug.Log(GameManager.playerPos);
        controller.enabled = true;
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
		if (other.gameObject.CompareTag("Player"))
		{
			Manager.Scene.LoadScene("2M");
		}
	}

}
