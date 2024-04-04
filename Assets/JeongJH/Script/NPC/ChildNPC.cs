using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace JJH
{
    public class ChildNPC : MonoBehaviour
    {
        // 1. 일단 플레이어와 만나면 플레이어의 뒤쪽으로 이동해야함 (앞에 있으면 더 이상 이동 x vector 방향을 + - 로 구분?)
        // 2. 일정 거리를 두고 따라다님. 
        // 3. 장애물 확인은 laycast 이용해서 전방으로 발사 ?? 해보기.
        // 4. 일정 구간에 들어서면 (trigger?)를 가지고 npc대사 패널 작동해서 대화 나오기. 

        [SerializeField] int speed = 10;


        public GameObject player;
        public GameObject DialogPrefab;
        public Rigidbody rigid;
        NavMeshAgent m_Agent;

        [SerializeField] List<Vector3>destPos=new List<Vector3>();
        int count;

        


        void Start()
        {
            player = GameObject.FindWithTag("Player"); //플레이어 태그 찾아서 저장. 
            rigid = GetComponent<Rigidbody>();
            m_Agent = GetComponent<NavMeshAgent>();

            count = 0;

        }

        void Update()  //여기서 이제 만약 일정범위 이내로 들어오면 그 위치에 고정되어야함. 
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            //이 부분은 trigger를 벗어나면 실행해서 다시 돌아가도록 하고


            m_Agent.destination = destPos[count]; //도착시마다 count 증가 시켜주기. 



        }


        private void OnTriggerEnter(Collider other)
        {

        }


        private void OnTriggerExit(Collider other)
        {

        }

        private void Jump() //y축 증가시키고 + 이동해야 하는 방향 설정해줘서 넣어주기. 
        {

        }

        private void OnGround() //그라운드 체크로 점프 막 못하게 하기. 
        {

        }


    }
}
