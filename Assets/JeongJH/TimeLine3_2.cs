using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TimeLine3_2 : MonoBehaviour
{
    public PlayableDirector director;
    public TimelineAsset timelineAsset;

    //�̰� �������� ����������. 
    private void OnEnable()
    {
        director.Play(timelineAsset);
    }



}
