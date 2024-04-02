using JJH;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.ShaderKeywordFilter;
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

    [SerializeField] GameObject lightningPrefab;
    [SerializeField] GameObject dangerZonePrefab;
    [SerializeField] LayerMask targetLayer;
    [SerializeField] GameObject playerPos;
    [SerializeField] float range;
    [SerializeField] int size;
    [SerializeField] bool isTrigger;
    [SerializeField] int circleRnage;
    [SerializeField] bool on;

    //배열을 한 3개 만들어서 그 중에서 이제 랜덤 선택해서 
    Vector3 []lightningZone=new Vector3[3];

    Vector3 createPos;

    private void Start()
    {
        
    }

    private void Update()
    {

        if(isTrigger)
        {
            StartCoroutine(LightningCoroutine());

        }
        
    }

    IEnumerator LightningCoroutine()
    {
        if(on==false)
        {
            on = true;
            for(int i=0;i<lightningZone.Length;i++)
            {
                lightningZone[i] = Random.insideUnitSphere * circleRnage;               
                createPos = playerPos.transform.position + lightningZone[i];
                createPos.y = playerPos.transform.position.y; //플레이어의 y 위치와 똑같이?
                GameObject instance= Instantiate(dangerZonePrefab, createPos, dangerZonePrefab.transform.rotation);
                yield return new WaitForSeconds(1f);
                Instantiate(lightningPrefab,createPos, Quaternion.identity);
                yield return new WaitForSeconds(1f);
                Destroy(instance);
                OverLapCheck();
                
                yield return new WaitForSeconds(2f);

            }
            yield return new WaitForSeconds(2f);
            on = false;
        }       
    }

    public void OverLapCheck() //여기 안에 있으면 데미지를 받아야 하는데.
    {
        Collider[] colliders = new Collider[20];
        size=Physics.OverlapSphereNonAlloc(createPos,range,colliders,targetLayer);
        for(int i=0;i<size;i++)
        {
            if (Extension.Contain(targetLayer, colliders[i].gameObject.layer)) //플레이어가 있다면 
            {
                Debug.Log("플레이어 데미지");
                PlayerHp.Player_Action?.Invoke(1000);

            }
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (Extension.Contain(targetLayer, other.gameObject.layer))
        {
            Debug.Log("트리거온");
            isTrigger = true;
            
        }

    }

    private void OnTriggerExit(Collider other) 
    {
        if (Extension.Contain(targetLayer, other.gameObject.layer))
        {
            
            isTrigger = false;
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(createPos, range);
    }


}
