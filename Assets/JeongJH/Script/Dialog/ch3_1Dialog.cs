using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ch3_1Dialog : MonoBehaviour  // 3챕터 오두막 대사 .
    // 여기서 다 가지고 있고 trigger 되는 오브젝트에서 대사를 불러오는 방식을 실행해볼까?
    // count를 이용하면 되지 않을지? 


{
    public DialogSystem[] dialogSystems;
    public GameObject panel;
    public bool isRoutine;
    public int dialogCount;

    private void Start()
    {
        StartCoroutine(DialogSetOn(0)); //0번 다이얼로그는 start 되자마자 진행됨. 

    }

    public void StartTextCoroutine(int count) //매개변수 int로 몇번 째 대화를 가져올지 구분. 
    {
        StartCoroutine(DialogSetOn(count));  //흠.. 스태틱으로 못만드나? 
    }

    private IEnumerator DialogSetOn(int count)
    {
        isRoutine = true;
        //count를 통해 배열 접근 
        panel.SetActive(true);
        dialogSystems[count].gameObject.SetActive(true);
        Time.timeScale = 0f;
        Debug.Log("시간" + Time.timeScale);
        yield return new WaitUntil(() => dialogSystems[count].UpdateDialog());
        Time.timeScale = 1f;
        Debug.Log("시간" + Time.timeScale);
        //여기 밑 부분을 마지막 트리거대사가 자동으로 실행되고 못돌아오고 있거든요
        dialogSystems[count].gameObject.SetActive(false); //어차피 count로 하니까 대사 다시 안나올듯 ?          
        dialogCount++; // 다음 번에 다음 패널을 불러오도록 
        panel.SetActive(false);
        isRoutine = false;

    }
}

