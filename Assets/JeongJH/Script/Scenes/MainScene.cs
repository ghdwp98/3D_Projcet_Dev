using System.Collections;
using UnityEngine;

namespace JJH
{
    public class MainScene : BaseScene
    {

        private void Update()
        {
          
        }
        public override IEnumerator LoadingRoutine()
        {
            yield return new WaitForSeconds(1f);
        }


        

    }

}