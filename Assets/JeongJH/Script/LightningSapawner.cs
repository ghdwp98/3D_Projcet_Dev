using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJH
{
    public class LightningSapawner : MonoBehaviour
    {
        public GameObject lightningPrefab;
        public LayerMask layermask; //플레이어의 레이어 
        public bool triggerOn = false;

        void Start()
        {
            //Instantiate(lightningPrefab,transform.position,Quaternion.identity);
        }

        void Update()
        {

            if(triggerOn==true)//트리거 진입시에만 코루틴 시작. 
            {
                StartCoroutine(LightningCoroutine());

            }

        }

        private void OnTriggerEnter(Collider other) //번개 생성 시작.
        {
            if (Extension.Contain(layermask,other.gameObject.layer))
            {
                Debug.Log("플레이어 진입.");
                triggerOn = true;
            }

    }

        private void OnTriggerExit(Collider other) // 번개 생성 끝. 
        {
            if (Extension.Contain(layermask, other.gameObject.layer))
            {
                Debug.Log("플레이어 벗어남");
                triggerOn= false;
            }
        }


        IEnumerator LightningCoroutine()
        {


            //붉게 빛나는 곳 위치 정해주기.

            yield return new WaitForSeconds(3f); //대기 후 번개 방출 
            Instantiate(lightningPrefab); //생성 위치는 붉게 빛나던 곳 으로 생성해야함.



            

        }



    }
}

