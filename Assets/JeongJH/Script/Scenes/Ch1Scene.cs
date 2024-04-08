using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class Ch1Scene :BaseScene
{
    [SerializeField] GameObject player;
    [SerializeField] CharacterController controller;
    [SerializeField] PooledObject FirePrefab;
    [SerializeField] PooledObject smallFirePrefab;
    [SerializeField] int size;
    [SerializeField] int capacity;
    [SerializeField] int smallSize;
    [SerializeField] int smallCapacity;
    [SerializeField] PooledObject lightningPrefab;
    [SerializeField] PooledObject dangerCircle;





    public override IEnumerator LoadingRoutine()
    {
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
