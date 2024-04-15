using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public static bool saved;
    public static Vector3 playerPos;

    public static int NpcDialogCount; // 대사 숫자 저장. 
    public static int staticNextGoal; //목적지 숫자 저장 


    //대화 로그를 저장해줘야하는데 



    // 이거 챕터2 로딩루틴에서 
    // npc의 위치와 npc의 대사 순서 기억해두고 -->이거 체크포인트에서 저장해야함. 체크포인트의 순간에 
    // 대사 순서와 + 그 위치(위치는 체크포인트 위치로 같이 이동시켜도 될것 같긴함. )
    // 로딩 하면 




    public void Test()
    {
        Debug.Log(GetInstanceID());
    }
}
