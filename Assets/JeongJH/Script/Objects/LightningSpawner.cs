using JJH;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;

public class LightningSpawner : MonoBehaviour
{
    //라이트닝 스포너 생성
    // 라이트닝 생성 전 붉은원으로 위험범위표시
    // 이후 라이트닝 -->맞으면 죽음. 
    // event 이용해서 바로 ondeath를 불러온다면??? 그러면 될듯한대.


    // trigger로 들어오고 나가고를 판단하고
    // 오버랩 스피어로 player 주변을 감지해서 그 주변에 원 생성 
    //2초 정도 후 생성 

    [SerializeField] PooledObject lightningPrefab;
    [SerializeField] PooledObject dangerZonePrefab;
    [SerializeField] LayerMask targetLayer;
    [SerializeField] GameObject playerPos;
    [SerializeField] float range;
    [SerializeField] int size;
    [SerializeField] bool isTrigger;
    [SerializeField] int circleRnage;
    [SerializeField] bool on;
    [SerializeField] int capacity;
    [SerializeField] int count;

    //배열을 한 3개 만들어서 그 중에서 이제 랜덤 선택해서 
    Vector3[] lightningZone = new Vector3[5];

    Vector3 []createPos=new Vector3[5];

    private void Awake()
    {
        playerPos = GameObject.FindWithTag("Player");
        Manager.Pool.CreatePool(lightningPrefab, size, capacity);
        Manager.Pool.CreatePool(dangerZonePrefab, size, capacity);

        //임시로 나중에 로딩루틴에서 해주기 --> 메인화면 --> 1챕터 
    }

    Collider[] colliders = new Collider[20];
    IEnumerator LightningCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);

            for (count = 0; count < lightningZone.Length; count++)
            {           
                lightningZone[count] = Random.insideUnitSphere * circleRnage;
                createPos[count] = playerPos.transform.position + lightningZone[count];
                createPos[count].y = playerPos.transform.position.y; //플레이어의 y 위치와 똑같이?
            }
           
            for(int i=0;i<lightningZone.Length;i++)
            {
                Manager.Pool.GetPool(dangerZonePrefab, createPos[i], dangerZonePrefab.transform.rotation);
            }           
            yield return new WaitForSeconds(1f);
            for(int i=0;i< lightningZone.Length; i++) //여기가 실제 데미지를 줘야하는 부분이지용. 
            {
                Manager.Pool.GetPool(lightningPrefab, createPos[i], lightningPrefab.transform.rotation);               
            }    
            yield return new WaitForSeconds(1f);

            
            for(int i=0;i< lightningZone.Length; i++)
            {
                int size = Physics.OverlapSphereNonAlloc(createPos[i], range, colliders, targetLayer);
                for(int j=0;j<size;j++)
                {
                    if (colliders[j].gameObject.CompareTag("Player")) //플레이어라면. 
                    {
                        if(PlayerHp.Player_Action!=null&&gameObject!=null)
                        {
                            PlayerHp.Player_Action(1000f);
                        }
                    }
                               
                }
            }
            yield return new WaitForSeconds(0.4f);
        }
    }


    Coroutine spawnRoutine = null;
    private void OnTriggerEnter(Collider other)
    {
        if (Extension.Contain(targetLayer, other.gameObject.layer))
        {
            if (spawnRoutine != null)
                return;

            Debug.Log("Trigger Enter");
            spawnRoutine = StartCoroutine(LightningCoroutine());
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (Extension.Contain(targetLayer, other.gameObject.layer))
        {
            if (spawnRoutine == null)
                return;

            Debug.Log("Trigger Exit");
            //일단 나가면 벼락 안치고 어쨋든 1자 진행이니까 다시 들어가면 다시 번개 치고.
            StopCoroutine(spawnRoutine);
            spawnRoutine = null;
        }
    }

    private void OnEnable()
    {
        Debug.Log("라이트닝 활성화");
    }

    private void OnDisable()
    {
        Debug.Log("라이트닝 비활성화");
    }
}
