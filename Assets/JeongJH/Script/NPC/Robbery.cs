using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace JJH
{
    public class Robbery : MonoBehaviour
    {
        // 기본적으로 spawner에서 생성시키자. why? player가 2층에 도달했을 때 생성되어야 하기 때문에

        // 공격할 때만 damage 콜라이더를 발동 시켜줘야 하는가??

        // 시야 말고 overlap 범위 내에 들어오면 바로 추적하자. --> 발 소리 라고 하면 됨. 

        // 플레이어가 숨으면 살짝 왔다갔다 3~4초 하다가 다시 목적지 순회로 상태변경 해줘야함. 
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
        float damage = 1000f; //원펀맨. + 닿으면 바로 사망이므로 넉백도 필요없음. 
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

        private void Awake() //이거 스포너에서 생성해줄거니까 그거 생각하면서 작성. 
        {

            stateMachine = new StateMachine<State>();
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            rigid = GetComponent<Rigidbody>();
            
            stateMachine.AddState(State.Intro, new IntroState(this)); //진짜 시작은 인트로로 --> 인트로에서 타임라인진행. 
            stateMachine.AddState(State.Patrol, new PatrolState(this));
            stateMachine.AddState(State.Trace, new TraceState(this));
            stateMachine.AddState(State.Die, new DieState(this));
            stateMachine.AddState(State.Wander, new WanderState(this));

            stateMachine.Start(State.Patrol); //임시로 시작을 패트롤 상태로 두기. 


        }

        private void Update()
        {
            stateMachine.Update();

            if(startTimer) //true면 시작. 
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
                // 시네머신 조정 + 타임라인 실시 등의 컷신 진행. 
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

            public override void Enter() //순환배열 
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
                    m_NextGoal = m_NextGoal != patrol.Length-1 ? m_NextGoal + 1 : 0; //이 부분 나중에 확인.
                }

                
                agent.destination=patrol[m_NextGoal].position;
                //agent.enabled = false;  -->추적 중지하고 내비메쉬 컴포넌트 비활성화 -->추적중에는 비활성화해주자. 
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
            // trace 상태에서 플레이어를 잃으면 몇 초간 방황 시작--> 이후 patrol 상태로 전환함. 

            public TraceState(Robbery robbery) : base(robbery) { }
            public override void Enter()
            {
                animator.SetTrigger("isRunning");
                agent.baseOffset = 0.2f;  //각 애니메이션에 맞춰서 baseOffset을 옮겨주기. ㅠ 
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
                Debug.Log(State.Die); //죽음 상태 진입 -> 여기서 여러 이벤트 진행. 
                animator.SetTrigger("isDead");
                
            }

            public override void Update()
            {
                count += Time.deltaTime;
                if(count>2) //2초 후. 
                {
                    
                    // 이벤트 관리하는 오브젝트 불러온 다음에 자기자신을 파괴한다. or 이벤트 관리 다른 방법 있으면 그거로 진행. 
                    Destroy(roberry.gameObject);
                }
            }


        }

       

    }
}

