using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using static UnityEditor.Experimental.GraphView.Port;

public class BasingScene : BaseScene
{
    [SerializeField] GameObject player;
    [SerializeField] CharacterController controller;
    [SerializeField] PooledObject FirePrefab;
    [SerializeField] PooledObject smallFirePrefab;
    [SerializeField] int size;
    [SerializeField] int capacity;
    [SerializeField] int smallSize;
    [SerializeField] int smallCapacity;



    private void Awake()
    {
        Manager.Pool.CreatePool(FirePrefab, size, capacity); //시작 할 때 숫자만큼 생성해주고. 
        Manager.Pool.CreatePool(smallFirePrefab, smallSize, smallCapacity);
    }

    public override IEnumerator LoadingRoutine()
    {
        /*Manager.Pool.CreatePool(FirePrefab, size, capacity); //시작 할 때 숫자만큼 생성해주고. 
        Manager.Pool.CreatePool(smallFirePrefab, smallSize, smallCapacity);*/ //나중에 진짜 사용. 

        if (GameManager.saved == false)
            yield break;

        //여기서 위치 저장 가능한지? 순서 확인 할 것. 
        controller.enabled = false;
        player.transform.position = GameManager.playerPos + new Vector3(1, 0, 1);
        Debug.Log(GameManager.playerPos);
        controller.enabled = true;
        yield return null;

    }

    
}
