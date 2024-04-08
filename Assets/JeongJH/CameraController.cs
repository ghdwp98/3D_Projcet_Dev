using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    public void Start()
    {
        offset= transform.position;
    }


    private void Update()
    {
        transform.position=target.position+offset;
    }





}
