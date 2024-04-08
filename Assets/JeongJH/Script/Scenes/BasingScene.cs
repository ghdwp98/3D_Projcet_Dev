using System.Collections;
using UnityEngine;

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
    [SerializeField] PooledObject lightningPrefab;
    [SerializeField] PooledObject dangerCircle;

    bool routine;


    private void Awake()
    {


    }

    public override IEnumerator LoadingRoutine()
    {
        Manager.Pool.CreatePool(FirePrefab, size, capacity);
        Manager.Pool.CreatePool(smallFirePrefab, smallSize, smallCapacity);
        Manager.Pool.CreatePool(lightningPrefab, size, capacity);
        Manager.Pool.CreatePool(dangerCircle, size, capacity);

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
