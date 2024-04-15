using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation.Samples;
using UnityEngine;

public class NonHideNPC : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject npc;

    AgentNpc agentNpc;

    private void Awake() //이 부분 너무 느리면 나중에 그냥 인스펙터창에서 넣어두기. 
    {
        agentNpc = npc.gameObject.GetComponent<AgentNpc>(); //npc의 스크립트 가져오기. 

    }

    private void OnTriggerEnter(Collider other) //ENTER 되었을 때 NPC의 상태를 확인해서. 
    {

        if (agentNpc.isHide == true)
        {
            Debug.Log("npc is non hiding.. ");

            agentNpc.transform.SetParent(null);
            agentNpc.isHide = false;
            gameObject.SetActive(false);     //자기 자신 꺼보기. 

        }
    }




}

