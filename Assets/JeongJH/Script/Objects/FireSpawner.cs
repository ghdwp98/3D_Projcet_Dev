using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpawner : MonoBehaviour
{
    //산불 spawner --> 파티클 과 셰이더(가능하면) 를 이용하여 플레이어 쪽으로 이동하는 산불을 생성
    // 산불은 데미지를 가지고있음. 
    // 산불의 속도는 Random 
    // 구역을 벗어나면 더이상 산불은 쫓아오지 않음
    // 산불은 player 방향으로 쫓아옴. 

    [SerializeField] PooledObject FirePrefab;
    private void Start()
    {
        
    }






}