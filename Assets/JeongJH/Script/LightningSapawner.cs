using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JJH
{
    public class LightningSapawner : MonoBehaviour
    {
        public GameObject lightningPrefab;
        public LayerMask layermask; //�÷��̾��� ���̾� 
        public bool triggerOn = false;

        void Start()
        {
            //Instantiate(lightningPrefab,transform.position,Quaternion.identity);
        }

        void Update()
        {

            if(triggerOn==true)//Ʈ���� ���Խÿ��� �ڷ�ƾ ����. 
            {
                StartCoroutine(LightningCoroutine());

            }

        }

        private void OnTriggerEnter(Collider other) //���� ���� ����.
        {
            if (Extension.Contain(layermask,other.gameObject.layer))
            {
                Debug.Log("�÷��̾� ����.");
                triggerOn = true;
            }

    }

        private void OnTriggerExit(Collider other) // ���� ���� ��. 
        {
            if (Extension.Contain(layermask, other.gameObject.layer))
            {
                Debug.Log("�÷��̾� ���");
                triggerOn= false;
            }
        }


        IEnumerator LightningCoroutine()
        {


            //�Ӱ� ������ �� ��ġ �����ֱ�.

            yield return new WaitForSeconds(3f); //��� �� ���� ���� 
            Instantiate(lightningPrefab); //���� ��ġ�� �Ӱ� ������ �� ���� �����ؾ���.



            

        }



    }
}

