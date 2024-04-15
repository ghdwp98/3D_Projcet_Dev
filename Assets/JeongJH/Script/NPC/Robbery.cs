using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;

namespace JJH
{
    public class Robbery : MonoBehaviour
    {
        // �̰� ������ 2�� ������ �ϴ°Ŵϱ� intro���� �÷��̾� �̵����� + �����ϴ� Ÿ�Ӷ��� �������ְ�
        // ����ϸ� ������ �׳� ����ϰ� �̺�Ʈ �������ָ��. 

        NavMeshAgent agent;
        Rigidbody rigid;
        Animator animator;
        GameObject playerObj;
        public Transform[] patrol;
        private int m_NextGoal = 0;
        bool startTimer;

        public float sec = 180f;
        public int min;
        public TextMeshProUGUI countDownText;
        [SerializeField] GameObject timeLineDirector;
        [SerializeField] CinemachineVirtualCamera playerCamera;



        [SerializeField] float patrolSpeed;
        [SerializeField] float traceSpeed;
        [SerializeField] LayerMask targetLayerMask;
        float damage = 1000f; //���ݸ�. + ������ �ٷ� ����̹Ƿ� �˹鵵 �ʿ����. 
        Collider[] colliders = new Collider[20];


        [SerializeField] float range;
        int size;
        Vector3 dirToTarget;
        float distToTarget;

        public enum State
        {
            Intro, Patrol, Trace, Wander, Die

        }
        public State stateEnum;

        private StateMachine<State> stateMachine;

        private void Awake() //�̰� �����ʿ��� �������ٰŴϱ� �װ� �����ϸ鼭 �ۼ�. 
        {

            
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            rigid = GetComponent<Rigidbody>();
            stateMachine = new StateMachine<State>();

            stateMachine.AddState(State.Intro, new IntroState(this)); //��¥ ������ ��Ʈ�η� --> ��Ʈ�ο��� Ÿ�Ӷ�������. 
            stateMachine.AddState(State.Patrol, new PatrolState(this));
            stateMachine.AddState(State.Trace, new TraceState(this));
            stateMachine.AddState(State.Die, new DieState(this));
            stateMachine.AddState(State.Wander, new WanderState(this));

            agent.updateRotation = false; //NavMeshAgent���� ȸ���� ������Ʈ ���� �ʵ��� ���� (���� ������Ʈ ����.)
            stateMachine.Start(State.Intro); //��Ʈ�ο��� Ÿ�Ӷ��� ���� �� 

        }

        private void Start()
        {
            playerObj = GameObject.FindWithTag("Player");
            agent.enabled = false;
        }

        private void Update()
        {
            stateMachine.Update();

            if (startTimer) //true�� ����. 
            {
                sec -= Time.deltaTime;
                if (sec > 0)
                {
                    min = (int)(sec / 60);
                    countDownText.text = string.Format("{0:D2}:{1:D2}", min, (int)(sec % 60));
                }
                else if (sec <= 0)
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

        private void OnTriggerEnter(Collider other) // Ʈ���� �� ������ �ִ� �̺�Ʈ �߻� ��Ŵ. 
        {
            if (Extension.Contain(targetLayerMask, other.gameObject.layer)) //�÷��̾� ���̾�� Ʈ���� �Ǹ� 
            {
                if (PlayerHp.Player_Action != null)
                {
                    PlayerHp.Player_Action(damage);
                }
            }
        }



        private class RobberyState : BaseState<State>
        {
            protected Robbery roberry;

            protected Rigidbody rigid => roberry.rigid;

            protected Animator animator => roberry.animator;
            protected LayerMask targetLayerMask => roberry.targetLayerMask;
            protected float patrolSpeed { get { return roberry.patrolSpeed; } set { roberry.patrolSpeed = value; } }
            protected float traceSpeed { get { return roberry.traceSpeed; } set { roberry.traceSpeed = value; } }

            protected GameObject playerObj => roberry.playerObj;
            protected int size { get { return roberry.size; } set { roberry.size = value; } }
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

            public double timeCount = 0;
            PlayableDirector playableDirector;
            public override void Enter()
            {
                Debug.Log(State.Intro);
                // ��Ʈ�ο��� Ÿ�Ӷ��� �� �ó׸ӽ� ���� ���� �� patrol�� ��ȯ����. 
                animator.SetBool("isWalking", true);
                roberry.timeLineDirector.SetActive(true);
                roberry.startTimer = false;
                roberry.countDownText.gameObject.SetActive(false);
                playableDirector = roberry.timeLineDirector.gameObject.GetComponent<PlayableDirector>();
                //timeCount = playableDirector.duration; //double ����?
                
            }

            public void OnTimeLineFinished()
            {
                roberry.playerCamera.MoveToTopOfPrioritySubqueue();

                ChangeState(State.Patrol); //��Ʈ�ѷ� ��ȯ�ϱ�. 
            }

            public override void Update()
            {
                timeCount += Time.deltaTime;

                if(timeCount> playableDirector.duration)
                {
                    OnTimeLineFinished();
                }

                

            }

            public override void Transition()
            {

            }
        }

        private class PatrolState : RobberyState
        {
            bool isFindOn;

            public PatrolState(Robbery robbery) : base(robbery) { }

            public override void Enter()
            {
                Debug.Log(State.Patrol);
                roberry.countDownText.gameObject.SetActive(true);
                agent.speed = patrolSpeed;
                roberry.startTimer = true;      //���⼭ Ÿ�̸� ���ֱ�.           
                animator.SetBool("isWalking", true);
                animator.SetBool("isRunning", false);
                agent.enabled = true;
                

            }
            public override void Update() //agent�� ������Ʈ speed�� �������� �ӵ��� �������� �� ����. 
            {
                float distance = Vector3.Distance(agent.transform.position, patrol[m_NextGoal].position);
                if (distance < 1f)
                {
                    Debug.Log("1f�̳��� �������� ������");
                    m_NextGoal = m_NextGoal != patrol.Length - 1 ? m_NextGoal + 1 : 0; //�� �κ� ���߿� Ȯ��.
                }

                Debug.Log(m_NextGoal);
                agent.SetDestination(patrol[m_NextGoal].position);

                Vector3 to = new Vector3(agent.destination.x, 0, agent.destination.z);
                Vector3 from = new Vector3(roberry.transform.position.x, 0, roberry.transform.position.z);
                roberry.transform.rotation = Quaternion.LookRotation(to - from);

                FindTarget(); //���⼭ �÷��̾� �߰��ϸ� �������·� ��ȯ. 
            }
            private void FindTarget()
            {
                int size = Physics.OverlapSphereNonAlloc(roberry.transform.position, range, colliders, targetLayerMask);
                if (size > 0)
                {
                    Debug.Log($"{size} , ���� �߰��� target�� ���� ");
                    isFindOn = true;
                }
            }
            public override void Transition()
            {
                if (isFindOn == true)
                {
                    isFindOn = false;
                    ChangeState(State.Trace); //ã���� ���� ���·� �����Ŵ. 
                }

            }
        }

        private class WanderState : RobberyState //���� �߿� �÷��̾� ��ġ�� ��� ã�� �������δٰ�.
        {
            public WanderState(Robbery robbery) : base(robbery) { }

            public float currentTime;
            public Vector3 goals;

            public override void Enter()
            {
                Debug.Log(State.Wander);
                agent.speed = patrolSpeed;
                
                //animator.SetBool("isRunning", false);
                // animator.SetBool("isWalking", true); //��ŷ ���·� ����������.WW
                animator.SetBool("isRunning", false);
                animator.SetBool("isWalking", false);

                //idle �������� �������� Ȯ�� 

                //goals = CalculateWanderPosition(); //���� ��� �θ��� �ȵǴϱ� enter�� ����. 
            }
            public override void Update()
            {
                currentTime += Time.deltaTime; // 5�ʰ� ��� �� �ٽ� �������� ����. 

                /*float distance = Vector3.Distance(agent.transform.position, goals);
                if (distance < 1f) //1 ������ �غ���. �� ������ ���� �ٲ�� �������� 
                {
                    goals= CalculateWanderPosition(); //���� �ٽ� �θ� --> �� �κ� �������� �ȵǸ� �׳� ġ��� idle����. 
                }
                agent.SetDestination(goals);

                Vector3 to = new Vector3(agent.destination.x, 0, agent.destination.z);
                Vector3 from = new Vector3(roberry.transform.position.x, 0, roberry.transform.position.z);
                roberry.transform.rotation = Quaternion.LookRotation(to - from);*/

            }
            public override void Transition()
            {
                if (currentTime > 5f)
                {
                    currentTime = 0f;
                    ChangeState(State.Patrol);
                }

            }

            private Vector3 CalculateWanderPosition()
            {
                float radius = 10f; //������ ���� ũ��
                int angle = 0; //���õ� ���� 
                int angleMin = 0; //�ּҰ���
                int angleMax = 360; //�ִ밢�� 


                angle = Random.Range(angleMin, angleMax);
                Vector3 targetPosition = roberry.transform.position+SetAngle(radius, angle);

                return targetPosition;

            }

            private Vector3 SetAngle(float radius,int angle)
            {
                Vector3 position = Vector3.zero;

                position.x=Mathf.Cos(angle)*radius;
                position.z=Mathf.Sin(angle)*radius;

                return position;
            }

        }
        private class TraceState : RobberyState
        {
            // trace ���¿��� �÷��̾ ������ �� �ʰ� ��Ȳ ����--> ���� patrol ���·� ��ȯ��. 

            public TraceState(Robbery robbery) : base(robbery) { }
            public override void Enter()
            {
                
                agent.speed = traceSpeed;
                animator.SetBool("isWalking", false);
                animator.SetBool("isRunning", true);
                agent.baseOffset = 0.2f;  //�� �ִϸ��̼ǿ� ���缭 baseOffset�� �Ű��ֱ�. �� 
                Debug.Log(State.Trace);
            }

            public override void Update() //�÷��̾ �����ϱ�. 
            {
                if (playerObj.gameObject.layer == LayerMask.NameToLayer("Player"))
                {
                    animator.SetBool("isRunning", true);
                    agent.SetDestination(playerObj.transform.position);

                    Vector3 to = new Vector3(agent.destination.x, 0, agent.destination.z);
                    Vector3 from = new Vector3(roberry.transform.position.x, 0, roberry.transform.position.z);
                    roberry.transform.rotation = Quaternion.LookRotation(to - from);

                }
            }
            public override void Transition()
            {
                if (playerObj.gameObject.layer != LayerMask.NameToLayer("Player"))
                {
                    ChangeState(State.Wander);
                }
            }
        }

        private class DieState : RobberyState
        {
            public DieState(Robbery robbery) : base(robbery) { }

            float count = 0;
            public override void Enter()
            {

                //agent.isStopped = true; //���߱� 
                Debug.Log(State.Die); //���� ���� ���� -> ���⼭ ���� �̺�Ʈ ����. 
                animator.SetTrigger("isDead");

            }

            public override void Update()
            {
                count += Time.deltaTime;
                if (count > 2) //2�� ��. 
                {
                    Manager.Scene.LoadScene("ending");
                    // �̺�Ʈ �����ϴ� ������Ʈ �ҷ��� ������ �ڱ��ڽ��� �ı��Ѵ�. or �̺�Ʈ ���� �ٸ� ��� ������ �װŷ� ����. 
                    Destroy(roberry.gameObject);
                }
            }


        }


        public void ChangePatrol()
        {
            stateMachine.ChangeState(State.Patrol);
        }



    }
}

