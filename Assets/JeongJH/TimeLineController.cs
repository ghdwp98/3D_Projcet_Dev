using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimeLineController : MonoBehaviour
{
    [SerializeField] GameObject timeLineDiretor;
    [SerializeField] float timeCount;
    [SerializeField] PlayableDirector playableDirector;


    private void Update()
    {
        timeCount += Time.deltaTime;

        if(timeCount>playableDirector.duration)
        {
            Manager.Scene.LoadScene("1MapJaehoon");
        }
           
    }



}
