using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject panel;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("����");
        panel.SetActive(true); //�г� �ٽ� ���ֱ�?? 
    }
}
