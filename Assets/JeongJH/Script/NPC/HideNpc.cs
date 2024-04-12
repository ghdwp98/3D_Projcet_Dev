using Unity.AI.Navigation.Samples;
using UnityEngine;

public class HideNpc : MonoBehaviour
{
    //이거를 npc가 trigger 되면 player의 자식으로 만들고.. 
    // npc의 bool 변수에 따라 숨어 있는 상태였으면 --> 자식을 해제해주고 메쉬 재생성. 
    // 숨어있는 상태가 아닐 때 trigger 되면--> 자식으로 만들고숨기기 

    //npc도 bool에 따라 자식이 된 상태라면 --> 일단 navmesh 이동을 멈춰두고
    // 자식이 해제 된 상태라면 다시 navmesh 켜주기. -->일단 npc의 return으로 처리해보고 안되면 이 방법 사용

    [SerializeField] GameObject player;
    [SerializeField] GameObject npc;

    AgentNpc agentNpc;

    private void Awake() //이 부분 너무 느리면 나중에 그냥 인스펙터창에서 넣어두기. 
    {
        agentNpc = npc.gameObject.GetComponent<AgentNpc>(); //npc의 스크립트 가져오기. 

    }

    private void OnTriggerEnter(Collider other) //ENTER 되었을 때 NPC의 상태를 확인해서. 
    {

        if(agentNpc.isHide==false)
        {
           

            agentNpc.transform.SetParent(player.gameObject.transform, true);
            agentNpc.isHide = true;

        }
    }
}
