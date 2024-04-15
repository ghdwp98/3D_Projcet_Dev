using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation.Samples;
using UnityEngine;

public class NonHideNPC : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject npc;

    AgentNpc agentNpc;

    private void Awake() //�� �κ� �ʹ� ������ ���߿� �׳� �ν�����â���� �־�α�. 
    {
        agentNpc = npc.gameObject.GetComponent<AgentNpc>(); //npc�� ��ũ��Ʈ ��������. 

    }

    private void OnTriggerEnter(Collider other) //ENTER �Ǿ��� �� NPC�� ���¸� Ȯ���ؼ�. 
    {

        if (agentNpc.isHide == true)
        {
            Debug.Log("npc is non hiding.. ");

            agentNpc.transform.SetParent(null);
            agentNpc.isHide = false;
            gameObject.SetActive(false);     //�ڱ� �ڽ� ������. 

        }
    }




}

