using System.Collections;
using UnityEngine;

namespace JJH
{
    public class MainScene : BaseScene
    {

        private void Update()
        {
            if (Input.anyKeyDown)
            {
                Manager.Scene.LoadScene("TutorialScene");
            }
        }
        public override IEnumerator LoadingRoutine()
        {
            yield return new WaitForSeconds(1f);
        }


        

    }

}