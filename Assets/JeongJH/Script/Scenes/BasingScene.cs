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
        Manager.Pool.CreatePool(FirePrefab, size, capacity); //���� �� �� ���ڸ�ŭ �������ְ�. 
        Manager.Pool.CreatePool(smallFirePrefab, smallSize, smallCapacity);
    }

    public override IEnumerator LoadingRoutine()
    {
        /*Manager.Pool.CreatePool(FirePrefab, size, capacity); //���� �� �� ���ڸ�ŭ �������ְ�. 
        Manager.Pool.CreatePool(smallFirePrefab, smallSize, smallCapacity);*/ //���߿� ��¥ ���. 

        if (GameManager.saved == false)
            yield break;

        //���⼭ ��ġ ���� ��������? ���� Ȯ�� �� ��. 
        controller.enabled = false;
        player.transform.position = GameManager.playerPos + new Vector3(1, 0, 1);
        Debug.Log(GameManager.playerPos);
        controller.enabled = true;
        yield return null;

    }

    
}
