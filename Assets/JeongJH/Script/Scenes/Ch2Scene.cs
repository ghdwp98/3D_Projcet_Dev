using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ch2Scene : BaseScene
{
    //������ƮǮ�� ... �÷��̾� üũ����Ʈ �� �ʿ��� ��ɵ� �ε� ��ƾ�� �־��ֱ�. 

    [SerializeField] GameObject player;
    [SerializeField] CharacterController controller;
    [SerializeField] PooledObject FirePrefab;
    [SerializeField] PooledObject smallFirePrefab;
    [SerializeField] int size;
    [SerializeField] int capacity;
    [SerializeField] int smallSize;
    [SerializeField] int smallCapacity;

    //�Ʊ� ��ҵ��� ���� �����Ǿ�� �ϴµ� �� ��ġ�� �����ִ°� �� ������Ʈ�� �־�ѱ�? 


    public override IEnumerator LoadingRoutine()
    {
        /*Manager.Pool.CreatePool(FirePrefab, size, capacity);
        Manager.Pool.CreatePool(smallFirePrefab, smallSize, smallCapacity);*/

        if (GameManager.saved == false)
            yield break;

        //���⼭ ��ġ ���� ��������? ���� Ȯ�� �� ��. 
        controller.enabled = false;
        player.transform.position = GameManager.playerPos + new Vector3(1, 0, 1);
        Debug.Log(GameManager.playerPos);
        controller.enabled = true;

        yield return new WaitForSecondsRealtime(0.3f);
    }

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        controller = player.GetComponent<CharacterController>();
        Manager.Pool.CreatePool(FirePrefab, size, capacity);
        Manager.Pool.CreatePool(smallFirePrefab, smallSize, smallCapacity);

        // �� �ѱ�鼭 �ϱ� �������ϱ� �ϴ� ���⼭�ϰ� ���߿� ����. 




    }

    
}
