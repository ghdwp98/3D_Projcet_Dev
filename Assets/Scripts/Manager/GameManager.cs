using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public static bool saved;
    public static Vector3 playerPos;

    public static int NpcDialogCount; // ��� ���� ����. 
    public static int staticNextGoal; //������ ���� ���� 

    public static int ch3_1_count; //���θ� �ٱ� 
    public static int ch1_count;
    public static int ch2_count;
    public static int ch3_2_count; //1�� 
    public static int ch3_3_count; //2��

    //����ƽ ������ ��ε� �ÿ� �ٽ� �� start ��簡 ������ �ʵ��� ���� . 



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
