using JJH;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class BeesAttack : MonoBehaviour
{
    // 상태머신 이용하는데 얘는 조금 다르게
    // 다시 원위치로 돌아가는게 아니고 
    // 빈 게임 오브젝트를 이용해서
    // 중간에 플레이어를 놓치면 잠깐 위치에서 대기하다가 
    // 그 빈 게임 오브젝트 위치로 이동을 시작하고 (시야 밖)
    // 그 빈 게임 오브젝트랑 트리거 되면 유닛 삭제해주기.
    // 필요한 애니메이션은 idle(날아다니는 애니메이션 적용)  , trace , attack , return (사라지는 위치로 이동)

    


}
