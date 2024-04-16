using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ch3_2ObjectDialog : MonoBehaviour
{

    [SerializeField] int count;//자신의 카운트를 통해 대화로그를 불러옴. 
    [SerializeField] ch3_2Dialog ch3_2Dialog;
    SphereCollider sphereCollider;

    [SerializeField] GameObject floorDiaTrigger;

    void Start()
    {
        ch3_2Dialog = GameObject.FindWithTag("Dialog").GetComponent<ch3_2Dialog>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) //player와 트리거 되면 대사 나오고 .. 대사 나온 이후에 트리거를 꺼줘야할듯? 
        {
            if (ch3_2Dialog != null) //null이 아니면 진행. 
            {
                floorDiaTrigger.SetActive(true);
                ch3_2Dialog.StartTextCoroutine(count); //자신의 카운트로 대사 불러오기. 
            }
                
            sphereCollider = GetComponent<SphereCollider>();
            Destroy(sphereCollider); //콜라이더 파괴로 다시 대사가 나오지 않도록한다. 

        }
    }
}
