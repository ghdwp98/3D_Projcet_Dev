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
    Vector3[] lightningZone = new Vector3[3];

    Vector3 []createPos=new Vector3[3];

    private void Start()
    {
        playerPos = GameObject.FindWithTag("Player");
        /*Manager.Pool.CreatePool(lightningPrefab, size, capacity);
        Manager.Pool.CreatePool(dangerZonePrefab, size, capacity);*/
    }

    private void Update()
    {
        if (isTrigger)
        {

            StartCoroutine(LightningCoroutine());

            //StartCoroutine(OverCoroutine());

            //이거 코루틴 제대로 한 번에 3개 정도씩만 플레이어 주변에 떨어지게 하는 방법을 생각해보자. 


        }

    }

    IEnumerator LightningCoroutine()
    {
        if (on == false)
        {
            on = true;
            Collider[] colliders = new Collider[20];
            for (count = 0; count < 3; count++)
            {           
                lightningZone[count] = Random.insideUnitSphere * circleRnage;
                createPos[count] = playerPos.transform.position + lightningZone[count];
                createPos[count].y = playerPos.transform.position.y; //플레이어의 y 위치와 똑같이?
            }
           
            for(int i=0;i<3;i++)
            {
                Manager.Pool.GetPool(dangerZonePrefab, createPos[i], dangerZonePrefab.transform.rotation);
            }           
            yield return new WaitForSeconds(1f);
            for(int i=0;i<3;i++) //여기가 실제 데미지를 줘야하는 부분이지용. 
            {
                Manager.Pool.GetPool(lightningPrefab, createPos[i], lightningPrefab.transform.rotation);               
            }    
            yield return new WaitForSeconds(1f);

            //이 부분 기즈모가 제대로 안그려지는데 이유가 뭐지?? 어째서 안그려지는거지??
            for(int i=0;i<3;i++)
            {
                int size = Physics.OverlapSphereNonAlloc(createPos[i], range, colliders, targetLayer);
                for(int j=0;j<size;j++)
                {
                    if (Extension.Contain(targetLayer, colliders[i].gameObject.layer)) //플레이어라면. 
                    {
                        if(PlayerHp.Player_Action!=null)
                        {
                            PlayerHp.Player_Action(1000f);
                        }
                    }
                               
                }
            }
            yield return new WaitForSeconds(0.5f);
            on = false;

        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (Extension.Contain(targetLayer, other.gameObject.layer))
        {
            isTrigger = true;

        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (Extension.Contain(targetLayer, other.gameObject.layer))
        {
            //일단 나가면 벼락 안치고 어쨋든 1자 진행이니까 다시 들어가면 다시 번개 치고. 
            isTrigger = false;
        }
    }

   


}
