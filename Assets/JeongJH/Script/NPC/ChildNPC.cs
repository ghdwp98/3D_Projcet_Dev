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
        [SerializeField] int m_NextGoal = 0;

        public GameObject player;
        public GameObject DialogPrefab;
        public Rigidbody rigid;
        NavMeshAgent m_Agent;

        public Transform[] goals;
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
            FindGoal();

        }


        public void FindGoal()
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            // 각 goal 목표를 기준으로 이동. --> 루프 but 실제게임은 루프는 필요 x 

            float distance = Vector3.Distance(m_Agent.transform.position, goals[m_NextGoal].position);
            if (distance < 0.5f)
            {
                //여기서 목표지점에 도착한다면 -->대사를 출력해주고 목표지점을 바꿔줘야함.
                m_NextGoal = +1;
            }
            m_Agent.destination = goals[m_NextGoal].position;

        }








    }
}
