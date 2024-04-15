using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TimeLine3_2 : MonoBehaviour
{
    public PlayableDirector director;
    public TimelineAsset timelineAsset;

    //이거 강도에서 시작해주자. 
    private void OnEnable()
    {
        director.Play(timelineAsset);
    }



}
