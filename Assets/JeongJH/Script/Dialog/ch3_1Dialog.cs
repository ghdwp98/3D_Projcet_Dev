using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ch3_1Dialog : MonoBehaviour  // 3é�� ���θ� ��� .
    // ���⼭ �� ������ �ְ� trigger �Ǵ� ������Ʈ���� ��縦 �ҷ����� ����� �����غ���?
    // count�� �̿��ϸ� ���� ������? 


{
    public DialogSystem[] dialogSystems;
    public GameObject panel;
    public bool isRoutine;
    public int dialogCount;

    private void Start()
    {
        StartCoroutine(DialogSetOn(0)); //0�� ���̾�α״� start ���ڸ��� �����. 

    }

    public void StartTextCoroutine(int count) //�Ű����� int�� ��� ° ��ȭ�� �������� ����. 
    {
        StartCoroutine(DialogSetOn(count));  //��.. ����ƽ���� �����峪? 
    }

    private IEnumerator DialogSetOn(int count)
    {
        isRoutine = true;
        //count�� ���� �迭 ���� 
        panel.SetActive(true);
        dialogSystems[count].gameObject.SetActive(true);
        Time.timeScale = 0f;
        Debug.Log("�ð�" + Time.timeScale);
        yield return new WaitUntil(() => dialogSystems[count].UpdateDialog());
        Time.timeScale = 1f;
        Debug.Log("�ð�" + Time.timeScale);
        //���� �� �κ��� ������ Ʈ���Ŵ�簡 �ڵ����� ����ǰ� �����ƿ��� �ְŵ��
        dialogSystems[count].gameObject.SetActive(false); //������ count�� �ϴϱ� ��� �ٽ� �ȳ��õ� ?          
        dialogCount++; // ���� ���� ���� �г��� �ҷ������� 
        panel.SetActive(false);
        isRoutine = false;

    }
}

