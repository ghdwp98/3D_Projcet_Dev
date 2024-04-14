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


    public override IEnumerator LoadingRoutine()
    {
        PlayerPrefs.SetString("LastScene",
            Manager.Scene.GetCurSceneName());

       


        if (GameManager.saved == false)
            yield break;

        //���⼭ ��ġ ���� ��������? ���� Ȯ�� �� ��. 
        controller.enabled = false;
        player.transform.position = GameManager.playerPos + new Vector3(1, 0, 1);
        Debug.Log(GameManager.playerPos);
        controller.enabled = true;
        yield return null;
    }


	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			Manager.Scene.LoadScene("2M");
		}
	}

}
