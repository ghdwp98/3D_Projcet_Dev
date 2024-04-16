using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public static bool saved;
    public static Vector3 playerPos;

    public static bool isSceneChange;  //이거를 씬 전환하는 데에서는 true로 만들어서 true가 아닐 때만 씬로딩 중에 playerpos위치를 저장하도록 한다.
    // 씬이 아예 변환될 때는 원래 위치에서 나와야 하기 때문이다.


    public static int NpcDialogCount; // 대사 숫자 저장. 
    public static int staticNextGoal; //목적지 숫자 저장 

    public static int ch3_1_count; //오두막 바깥 
    public static int ch1_count;
    public static int ch2_count;
    public static int ch3_2_count; //1층 
    public static int ch3_3_count; //2층



    //스태틱 변수로 재로딩 시에 다시 씬 start 대사가 나오지 않도록 조정 . 



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
