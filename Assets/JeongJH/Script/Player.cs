using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject panel;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("실행");
        panel.SetActive(true); //패널 다시 꺼주기?? 
    }
}
