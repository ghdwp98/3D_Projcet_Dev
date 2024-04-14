using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace JJH
{
    public class MainScene : BaseScene
    {
        // 컨티뉴 버튼 비활성화 작업
        public GameObject continueBtn;


        private void Start()
        {
            //저장값 반환 -->LastScene에 저장된 문자 값을 반환한다. 

            string sceneName = PlayerPrefs.GetString("LastScene");      //저장 된 씬
            if (sceneName == "")
            {
                continueBtn.GetComponent<Button>().interactable = false; //비활성
            }
            else
            {
                continueBtn.GetComponent<Button>().interactable = true; //활성
            }

        }

        public override IEnumerator LoadingRoutine()
        {
            yield return null; 
        }
        
        //main씬은 저장할 필요가 없지용. 

        

    }

}