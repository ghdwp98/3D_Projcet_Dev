using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;

namespace JJH
{
    public class Robbery : MonoBehaviour
    {
        // 이거 어차피 2층 에서만 하는거니까 intro에서 플레이어 이동방지 + 등장하는 타임라인 실행해주고
        // 사망하면 어차피 그냥 사망하고 이벤트 진행해주면됨. 

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
        float damage = 1000f; //원펀맨. + 닿으면 바로 사망이므로 넉백도 필요없음. 
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

        private void Awake() //이거 스포너에서 생성해줄거니까 그거 생각하면서 작성. 
        {

            
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            rigid = GetComponent<Rigidbody>();
            stateMachine = new StateMachine<State>();

            stateMachine.AddState(State.Intro, new IntroState(this)); //진짜 시작은 인트로로 --> 인트로에서 타임라인진행. 
            stateMachine.AddState(State.Patrol, new PatrolState(this));
            stateMachine.AddState(State.Trace, new TraceState(this));
            stateMachine.AddState(State.Die, new DieState(this));
            stateMachine.AddState(State.Wander, new WanderState(this));

            agent.updateRotation = false; //NavMeshAgent에서 회전을 업데이트 하지 않도록 설정 (수동 업데이트 진행.)
            stateMachine.Start(State.Intro); //인트로에서 타임라인 진행 후 

        }

        private void Start()
        {
            playerObj = GameObject.FindWithTag("Player");
            agent.enabled = false;
        }

        private void Update()
        {
            stateMachine.Update();

            if (startTimer) //true면 시작. 
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

        private void OnTriggerEnter(Collider other) // 트리거 시 데미지 주는 이벤트 발생 시킴. 
        {
            if (Extension.Contain(targetLayerMask, other.gameObject.layer)) //플레이어 레이어와 트리거 되면 
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
                // 인트로에서 타임라인 및 시네머신 조정 진행 후 patrol로 전환시작. 
                animator.SetBool("isWalking", true);
                roberry.timeLineDirector.SetActive(true);
                roberry.startTimer = false;
                roberry.countDownText.gameObject.SetActive(false);
                playableDirector = roberry.timeLineDirector.gameObject.GetComponent<PlayableDirector>();
                //timeCount = playableDirector.duration; //double 형식?
                
            }

            public void OnTimeLineFinished()
            {
                roberry.playerCamera.MoveToTopOfPrioritySubqueue();

                ChangeState(State.Patrol); //패트롤로 전환하기. 
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
                roberry.startTimer = true;      //여기서 타이머 켜주기.           
                animator.SetBool("isWalking", true);
                animator.SetBool("isRunning", false);
                agent.enabled = true;
                

            }
            public override void Update() //agent의 컴포넌트 speed로 순찰도는 속도를 조절해줄 수 있음. 
            {
                float distance = Vector3.Distance(agent.transform.position, patrol[m_NextGoal].position);
                if (distance < 1f)
                {
                    Debug.Log("1f이내로 목적지에 접근함");
                    m_NextGoal = m_NextGoal != patrol.Length - 1 ? m_NextGoal + 1 : 0; //이 부분 나중에 확인.
                }

                Debug.Log(m_NextGoal);
                agent.SetDestination(patrol[m_NextGoal].position);

                Vector3 to = new Vector3(agent.destination.x, 0, agent.destination.z);
                Vector3 from = new Vector3(roberry.transform.position.x, 0, roberry.transform.position.z);
                roberry.transform.rotation = Quaternion.LookRotation(to - from);

                FindTarget(); //여기서 플레이어 발견하면 추적상태로 전환. 
            }
            private void FindTarget()
            {
                int size = Physics.OverlapSphereNonAlloc(roberry.transform.position, range, colliders, targetLayerMask);
                if (size > 0)
                {
                    Debug.Log($"{size} , 현재 발견한 target의 갯수 ");
                    isFindOn = true;
                }
            }
            public override void Transition()
            {
                if (isFindOn == true)
                {
                    isFindOn = false;
                    ChangeState(State.Trace); //찾으면 추적 상태로 변경시킴. 
                }

            }
        }

        private class WanderState : RobberyState //추적 중에 플레이어 놓치면 잠시 찾는 느낌으로다가.
        {
            public WanderState(Robbery robbery) : base(robbery) { }

            public float currentTime;
            public Vector3 goals;

            public override void Enter()
            {
                Debug.Log(State.Wander);
                agent.speed = patrolSpeed;
                
                //animator.SetBool("isRunning", false);
                // animator.SetBool("isWalking", true); //워킹 상태로 움직여주자.WW
                animator.SetBool("isRunning", false);
                animator.SetBool("isWalking", false);

                //idle 상태진입 가능한지 확인 

                //goals = CalculateWanderPosition(); //랜덤 계속 부르면 안되니까 enter로 생성. 
            }
            public override void Update()
            {
                currentTime += Time.deltaTime; // 5초간 대기 후 다시 목적지로 떠남. 

                /*float distance = Vector3.Distance(agent.transform.position, goals);
                if (distance < 1f) //1 정도로 해보자. 좀 빠르게 휙휙 바뀌는 느낌으로 
                {
                    goals= CalculateWanderPosition(); //새로 다시 부름 --> 이 부분 범위제한 안되면 그냥 치우고 idle하자. 
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
                float radius = 10f; //생성될 원의 크기
                int angle = 0; //선택된 각도 
                int angleMin = 0; //최소각도
                int angleMax = 360; //최대각도 


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
            // trace 상태에서 플레이어를 잃으면 몇 초간 방황 시작--> 이후 patrol 상태로 전환함. 

            public TraceState(Robbery robbery) : base(robbery) { }
            public override void Enter()
            {
                
                agent.speed = traceSpeed;
                animator.SetBool("isWalking", false);
                animator.SetBool("isRunning", true);
                agent.baseOffset = 0.2f;  //각 애니메이션에 맞춰서 baseOffset을 옮겨주기. ㅠ 
                Debug.Log(State.Trace);
            }

            public override void Update() //플레이어를 추적하기. 
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

                //agent.isStopped = true; //멈추기 
                Debug.Log(State.Die); //죽음 상태 진입 -> 여기서 여러 이벤트 진행. 
                animator.SetTrigger("isDead");

            }

            public override void Update()
            {
                count += Time.deltaTime;
                if (count > 2) //2초 후. 
                {
                    Manager.Scene.LoadScene("ending");
                    // 이벤트 관리하는 오브젝트 불러온 다음에 자기자신을 파괴한다. or 이벤트 관리 다른 방법 있으면 그거로 진행. 
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

