using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace JJH
{
    public class ChildNPC : MonoBehaviour
    {
        // 1. �ϴ� �÷��̾�� ������ �÷��̾��� �������� �̵��ؾ��� (�տ� ������ �� �̻� �̵� x vector ������ + - �� ����?)
        // 2. ���� �Ÿ��� �ΰ� ����ٴ�. 
        // 3. ��ֹ� Ȯ���� laycast �̿��ؼ� �������� �߻� ?? �غ���.
        // 4. ���� ������ ���� (trigger?)�� ������ npc��� �г� �۵��ؼ� ��ȭ ������. 

        [SerializeField] int speed = 10;


        public GameObject player;
        public GameObject DialogPrefab;
        public Rigidbody rigid;
        NavMeshAgent m_Agent;

        [SerializeField] List<Vector3>destPos=new List<Vector3>();
        int count;

        


        void Start()
        {
            player = GameObject.FindWithTag("Player"); //�÷��̾� �±� ã�Ƽ� ����. 
            rigid = GetComponent<Rigidbody>();
            m_Agent = GetComponent<NavMeshAgent>();

            count = 0;

        }

        void Update()  //���⼭ ���� ���� �������� �̳��� ������ �� ��ġ�� �����Ǿ����. 
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            //�� �κ��� trigger�� ����� �����ؼ� �ٽ� ���ư����� �ϰ�


            m_Agent.destination = destPos[count]; //�����ø��� count ���� �����ֱ�. 



        }


        private void OnTriggerEnter(Collider other)
        {

        }


        private void OnTriggerExit(Collider other)
        {

        }

        private void Jump() //y�� ������Ű�� + �̵��ؾ� �ϴ� ���� �������༭ �־��ֱ�. 
        {

        }

        private void OnGround() //�׶��� üũ�� ���� �� ���ϰ� �ϱ�. 
        {

        }


    }
}
