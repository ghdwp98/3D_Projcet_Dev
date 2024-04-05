using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaserNavMesh : MonoBehaviour
{
    Animator animator;
    Rigidbody rigid;
    NavMeshAgent agent;


    private void Start()
    {
        animator=GetComponent<Animator>();
        rigid=GetComponent<Rigidbody>();
        agent=GetComponent<NavMeshAgent>();

    }

    private void Update()
    {
        





    }
}
