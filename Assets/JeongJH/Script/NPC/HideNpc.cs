using Unity.AI.Navigation.Samples;
using UnityEngine;

public class HideNpc : MonoBehaviour
{
    //�̰Ÿ� npc�� trigger �Ǹ� player�� �ڽ����� �����.. 
    // npc�� bool ������ ���� ���� �ִ� ���¿����� --> �ڽ��� �������ְ� �޽� �����. 
    // �����ִ� ���°� �ƴ� �� trigger �Ǹ�--> �ڽ����� ��������� 

    //npc�� bool�� ���� �ڽ��� �� ���¶�� --> �ϴ� navmesh �̵��� ����ΰ�
    // �ڽ��� ���� �� ���¶�� �ٽ� navmesh ���ֱ�. -->�ϴ� npc�� return���� ó���غ��� �ȵǸ� �� ��� ���

    [SerializeField] GameObject player;
    [SerializeField] GameObject npc;

    AgentNpc agentNpc;

    private void Awake() //�� �κ� �ʹ� ������ ���߿� �׳� �ν�����â���� �־�α�. 
    {
        agentNpc = npc.gameObject.GetComponent<AgentNpc>(); //npc�� ��ũ��Ʈ ��������. 

    }

    private void OnTriggerEnter(Collider other) //ENTER �Ǿ��� �� NPC�� ���¸� Ȯ���ؼ�. 
    {

        if(agentNpc.isHide==false)
        {
           

            agentNpc.transform.SetParent(player.gameObject.transform, true);
            agentNpc.isHide = true;

        }
    }
}
