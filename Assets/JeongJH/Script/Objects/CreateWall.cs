using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateWall : MonoBehaviour
{
    BoxCollider boxCollider;
    SphereCollider sphereCollider;


    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        sphereCollider = GetComponent<SphereCollider>();

        boxCollider.enabled = false;
        sphereCollider.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            boxCollider.enabled = true;
        }
    }
}
