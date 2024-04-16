using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace JJH
{
    public class MainScene : BaseScene
    {
        // ��Ƽ�� ��ư ��Ȱ��ȭ �۾�
        public GameObject continueBtn;
        [SerializeField] AudioClip audioclip;

        private void Start()
        {
            //���尪 ��ȯ -->LastScene�� ����� ���� ���� ��ȯ�Ѵ�. 

            string sceneName = PlayerPrefs.GetString("LastScene");      //���� �� ��
            if (sceneName == "")
            {
                continueBtn.GetComponent<Button>().interactable = false; //��Ȱ��
            }
            else
            {
                continueBtn.GetComponent<Button>().interactable = true; //Ȱ��
            }

            Manager.Sound.PlayBGM(audioclip);

        }

        public override IEnumerator LoadingRoutine()
        {
            yield return null; 
        }
        
        //main���� ������ �ʿ䰡 ������. 

        

    }

}