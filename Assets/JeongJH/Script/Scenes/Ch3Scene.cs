using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ch3Scene : BaseScene
{
    //3¸Ê Áý ¹Ù±ù ¾À ·Îµù. 
    [SerializeField] PopUpUI escPopUPUI;


    private void Update()
    {
        //¸ÞÀÎ¾ÀÀÌ ¾Æ´Ò¶§¸¸ escÅ° ÀÌ¿ë°¡´É. 
        if (Input.GetKeyDown(KeyCode.Escape) && UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "MainScene")
        {
            Debug.Log("°ÙÅ° µé¾î°¨");
            Manager.UI.ShowPopUpUI(escPopUPUI); //ESCÆË¾÷ UI 
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
