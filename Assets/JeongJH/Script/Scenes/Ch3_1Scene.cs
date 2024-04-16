using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ch3_1Scene : BaseScene
{

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


    private void OnTriggerEnter(Collider other)  //Ʈ���ŷ� �� ��ȯ�Ѵٸ� 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.isSceneChange = true;
            Debug.Log("3_1�� ->3_2�� é�ͺ���" + GameManager.isSceneChange);
            Manager.Scene.LoadScene("3M3");
        }
            

    }



    public override IEnumerator LoadingRoutine()
    {
        Debug.Log("3��_1�� ���� �ε���ƾ " + GameManager.isSceneChange);
        GameManager.isSceneChange = false;
        Debug.Log("3��_1�� ���� �ε���ƾ false ���� ���������� " + GameManager.isSceneChange);
        yield return null;
    }
}
