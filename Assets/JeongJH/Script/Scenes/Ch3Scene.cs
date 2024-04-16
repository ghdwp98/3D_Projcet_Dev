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


    private void OnTriggerEnter(Collider other)  //Ʈ���� �� �� ��ȯ.
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.isSceneChange = true;
            Debug.Log("3->3_1�� é�ͺ���" + GameManager.isSceneChange);
            Manager.Scene.LoadScene("3M2");
        }
           
        
    }


    public override IEnumerator LoadingRoutine()
    {
        Debug.Log("3�� ���� �ε���ƾ " + GameManager.isSceneChange);
        GameManager.isSceneChange = false;
        Debug.Log("3�� ���� �ε���ƾ false ���� ���������� " + GameManager.isSceneChange);

        yield return null;
    }
}
