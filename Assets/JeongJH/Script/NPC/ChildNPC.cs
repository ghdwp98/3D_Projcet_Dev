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
        [SerializeField] int m_NextGoal = 0;

        public GameObject player;
        public GameObject DialogPrefab;
        public Rigidbody rigid;
        NavMeshAgent m_Agent;

        public Transform[] goals;
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
            FindGoal();

        }


        public void FindGoal()
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            // �� goal ��ǥ�� �������� �̵�. --> ���� but ���������� ������ �ʿ� x 

            float distance = Vector3.Distance(m_Agent.transform.position, goals[m_NextGoal].position);
            if (distance < 0.5f)
            {
                //���⼭ ��ǥ������ �����Ѵٸ� -->��縦 ������ְ� ��ǥ������ �ٲ������.
                m_NextGoal = +1;
            }
            m_Agent.destination = goals[m_NextGoal].position;

        }








    }
}
