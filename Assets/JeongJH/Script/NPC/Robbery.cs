using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace JJH
{
    public class Robbery : MonoBehaviour
    {
        // �⺻������ spawner���� ������Ű��. why? player�� 2���� �������� �� �����Ǿ�� �ϱ� ������

        // ������ ���� damage �ݶ��̴��� �ߵ� ������� �ϴ°�??

        // �þ� ���� overlap ���� ���� ������ �ٷ� ��������. --> �� �Ҹ� ��� �ϸ� ��. 

        // �÷��̾ ������ ��¦ �Դٰ��� 3~4�� �ϴٰ� �ٽ� ������ ��ȸ�� ���º��� �������. 
        NavMeshAgent agent;
        Rigidbody rigid;
        Animator animator;
        public Transform[] patrol;
        private int m_NextGoal = 0;
        bool startTimer;

        public float sec = 180f;
        public int min;
        public TextMeshProUGUI countDownText;



        [SerializeField] float moveSpeed;
        [SerializeField] LayerMask targetLayerMask;
        float damage = 1000f; //���ݸ�. + ������ �ٷ� ����̹Ƿ� �˹鵵 �ʿ����. 
        Collider[] colliders = new Collider[20];


        [SerializeField] float range;
        int size;
        Vector3 dirToTarget;
        float distToTarget;

        public enum State
        {
            Intro, Patrol, Trace, Wander,Die 

        }
        public State stateEnum;

        private StateMachine<State> stateMachine;

        private void Awake() //�̰� �����ʿ��� �������ٰŴϱ� �װ� �����ϸ鼭 �ۼ�. 
        {

            stateMachine = new StateMachine<State>();
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            rigid = GetComponent<Rigidbody>();
            
            stateMachine.AddState(State.Intro, new IntroState(this)); //��¥ ������ ��Ʈ�η� --> ��Ʈ�ο��� Ÿ�Ӷ�������. 
            stateMachine.AddState(State.Patrol, new PatrolState(this));
            stateMachine.AddState(State.Trace, new TraceState(this));
            stateMachine.AddState(State.Die, new DieState(this));
            stateMachine.AddState(State.Wander, new WanderState(this));

            stateMachine.Start(State.Patrol); //�ӽ÷� ������ ��Ʈ�� ���·� �α�. 


        }

        private void Update()
        {
            stateMachine.Update();

            if(startTimer) //true�� ����. 
            {
                sec -= Time.deltaTime;
                if (sec>0)
                {                  
                    min = (int)(sec / 60);
                    countDownText.text = string.Format("{0:D2}:{1:D2}", min, (int)(sec % 60));
                }
                else if(sec<=0)
                {                   
                    countDownText.gameObject.SetActive(false); 
                    stateMachine.ChangeState(State.Die); 
                }
            }
            
        }

        private void FixedUpdate()
        {
            stateMachine.FixedUpdate();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(gameObject.transform.position, range);
        }


        private class RobberyState : BaseState<State>
        {
            protected Robbery roberry;

            protected Rigidbody rigid => roberry.rigid;

            protected Animator animator => roberry.animator;
            protected LayerMask targetLayerMask => roberry.targetLayerMask;
            protected float moveSpeed => roberry.moveSpeed;
            protected int size { get { return roberry.size; } set{ roberry.size = value; }}
            protected Transform[] patrol => roberry.patrol;
            protected NavMeshAgent agent => roberry.agent;
            protected float range => roberry.range;
            protected int m_NextGoal { get { return roberry.m_NextGoal; } set { roberry.m_NextGoal = value; } }
            protected Collider[] colliders => roberry.colliders;


            public RobberyState(Robbery robbery)
            {
                this.roberry = robbery;
            }

        }
        private class IntroState : RobberyState
        {
            public IntroState(Robbery robbery) : base(robbery) { }

            public override void Enter()
            {
                // �ó׸ӽ� ���� + Ÿ�Ӷ��� �ǽ� ���� �ƽ� ����. 
            }

            public override void Update()
            {

            }

            public override void Transition()
            {

            }
        }

        private class PatrolState : RobberyState
        {
            public PatrolState(Robbery robbery) : base(robbery) { }

            public override void Enter() //��ȯ�迭 
            {
                roberry.startTimer = true;
                Debug.Log(State.Patrol);
                //animator.SetTrigger("isWalking");
                FindTarget();
                animator.SetTrigger("isRunning");
            }

            public override void Update()
            {
                float distance = Vector3.Distance(agent.transform.position, patrol[m_NextGoal].position);
                if (distance < 0.5f)
                {
                    m_NextGoal = m_NextGoal != patrol.Length-1 ? m_NextGoal + 1 : 0; //�� �κ� ���߿� Ȯ��.
                }

                
                agent.destination=patrol[m_NextGoal].position;
                //agent.enabled = false;  -->���� �����ϰ� ����޽� ������Ʈ ��Ȱ��ȭ -->�����߿��� ��Ȱ��ȭ������. 
            }


            private void FindTarget()
            {
                
            }
            

            public override void Transition()
            {
                
            }

        }

        private class WanderState : RobberyState
        {
            public WanderState(Robbery robbery) : base(robbery) { }

            public override void Enter()
            {

            }
            public override void Update()
            {

            }

            public override void Transition()
            {

            }
        }


        private class TraceState : RobberyState
        {
            // trace ���¿��� �÷��̾ ������ �� �ʰ� ��Ȳ ����--> ���� patrol ���·� ��ȯ��. 

            public TraceState(Robbery robbery) : base(robbery) { }
            public override void Enter()
            {
                animator.SetTrigger("isRunning");
                agent.baseOffset = 0.2f;  //�� �ִϸ��̼ǿ� ���缭 baseOffset�� �Ű��ֱ�. �� 
            }

            public override void Update()
            {

            }

            public override void Transition()
            {

            }


        }

        private class DieState : RobberyState
        {
            public DieState(Robbery robbery) : base(robbery) { }

            float count = 0;
            public override void Enter()
            {
                agent.isStopped = true;
                Debug.Log(State.Die); //���� ���� ���� -> ���⼭ ���� �̺�Ʈ ����. 
                animator.SetTrigger("isDead");
                
            }

            public override void Update()
            {
                count += Time.deltaTime;
                if(count>2) //2�� ��. 
                {
                    
                    // �̺�Ʈ �����ϴ� ������Ʈ �ҷ��� ������ �ڱ��ڽ��� �ı��Ѵ�. or �̺�Ʈ ���� �ٸ� ��� ������ �װŷ� ����. 
                    Destroy(roberry.gameObject);
                }
            }


        }

       

    }
}

