using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public static bool saved;
    public static Vector3 playerPos;

    public static int NpcDialogCount; // ��� ���� ����. 
    public static int staticNextGoal; //������ ���� ���� 


    //��ȭ �α׸� ����������ϴµ� 



    // �̰� é��2 �ε���ƾ���� 
    // npc�� ��ġ�� npc�� ��� ���� ����صΰ� -->�̰� üũ����Ʈ���� �����ؾ���. üũ����Ʈ�� ������ 
    // ��� ������ + �� ��ġ(��ġ�� üũ����Ʈ ��ġ�� ���� �̵����ѵ� �ɰ� ������. )
    // �ε� �ϸ� 




    public void Test()
    {
        Debug.Log(GetInstanceID());
    }
}
