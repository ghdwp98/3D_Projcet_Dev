using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ch3Scene : BaseScene
{
    //3�� �� �ٱ� �� �ε�. 
    [SerializeField] PopUpUI escPopUPUI;


    private void Update()
    {
        //���ξ��� �ƴҶ��� escŰ �̿밡��. 
        if (Input.GetKeyDown(KeyCode.Escape) && UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "MainScene")
        {
            Debug.Log("��Ű ��");
            Manager.UI.ShowPopUpUI(escPopUPUI); //ESC�˾� UI 
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            Manager.Scene.LoadScene("3M2");
        
    }


    public override IEnumerator LoadingRoutine()
    {
        

        yield return null;
    }
}
