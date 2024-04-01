using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace JJH
{
    public class MonsterController : MonoBehaviour
    {
        [SerializeField] Transform viewPoint;
        [SerializeField] LayerMask targetLayerMask;
        [SerializeField] LayerMask obstacleLayerMask;
        [SerializeField] float range;
        [SerializeField, Range(0, 360)] float angle;

        [SerializeField] bool onGround;
        [SerializeField] LayerMask groundLayer;
        [SerializeField] float groundCheckDistance = 0.1f;
        [SerializeField] float moveSpeed;
        [SerializeField] AnimationClip attackClip;
        [SerializeField] Transform startPos; //이거 그냥 빈 오브젝트 넣어서 할까 고민중. 

        string attack;
        int size = 20;
        Vector3 dirToTarget;
        float distToTarget;


        bool isTriggerOn;
        Rigidbody rigid;
        CapsuleCollider capsuleCollider;
        Animator animator;


        public enum State
        {
            Idle, Trace, Return, Attack //공격 받고 죽고 하는거 없이 순찰중에 발견하면 추적 
        }
        public State stateEnum;

        private float preAngle;
        private float cosAngle;

        //target 레이어를 player로 둬서 플레이어의 레이어가 바뀌면 (숨으면 ) 일정시간 대기하다가 return하기. 
        // 뒤를 보고 있으면 angle에 들어오지 않기 때문에 걸리지 않음. 

        private StateMachine<State> stateMachine;

        private void Awake()
        {
            rigid = GetComponent<Rigidbody>();
            capsuleCollider = GetComponent<CapsuleCollider>();
            animator = GetComponent<Animator>();


            stateMachine = new StateMachine<State>();
            attackClip=GetComponent<AnimationClip>();

            //attack=attackClip.name;

            stateMachine.AddState(State.Idle, new IdleState(this));
            stateMachine.AddState(State.Trace, new TraceState(this));
            stateMachine.AddState(State.Return, new ReturnState(this));
            stateMachine.AddState(State.Attack, new AttackState(this));

            stateMachine.Start(State.Idle);


        }


        private float CosAngle
        {
            get
            {
                if (preAngle == angle)
                    return cosAngle;

                preAngle = angle;
                cosAngle = Mathf.Cos(angle * 0.5f * Mathf.Deg2Rad);
                return cosAngle;
            }
        }

        private class MonsterState : BaseState<State>
        {
            protected MonsterController monster;

            protected Transform transform => monster.transform;
            protected Rigidbody rigid => monster.rigid;
            protected Animator animator => monster.animator;
            protected LayerMask targetLayerMask => monster.targetLayerMask;
            protected Transform viewPoint => monster.viewPoint;
            protected LayerMask obstacleLayerMask => monster.obstacleLayerMask;

            protected float moveSpeed => monster.moveSpeed;

            protected int size { get { return monster.size; } set { monster.size = value; } }
            protected Vector3 dirToTarget { get { return monster.dirToTarget; } set { monster.dirToTarget = value; } }
            protected float distToTarget { get { return monster.distToTarget; } set { monster.distToTarget = value; } }
            protected float range => monster.range;

            protected float CosAngle => monster.CosAngle;

            public MonsterState(MonsterController monster)
            {
                this.monster = monster;
            }
        }

        private class IdleState : MonsterState
        {
            bool isTargetOn = false;
            public IdleState(MonsterController monster) : base(monster)
            {
                //순찰은 NAVMESH 이용한 빈 오브젝트로 하자. 
            }

            public override void Enter()
            {
                isTargetOn = false; //어차피 다시 idle로 돌아오면 false 되니까 다시 바로 트랜지션안됨. 
            }
            public override void Update()
            {
                Debug.Log("idel 상태");
                FindTarget();
            }

            public override void Transition()
            {
                if (isTargetOn == true)
                {
                    ChangeState(State.Trace);
                }
            }
            Collider[] colliders = new Collider[20];
            private void FindTarget()
            {
                size = Physics.OverlapSphereNonAlloc(viewPoint.position, range, colliders, targetLayerMask);
                for (int i = 0; i < size; i++)
                {
                    dirToTarget = (colliders[i].transform.position - viewPoint.position).normalized;

                    if (Vector3.Dot(viewPoint.forward, dirToTarget) < CosAngle)
                        continue;

                    distToTarget = Vector3.Distance(colliders[i].transform.position, viewPoint.position);
                    if (Physics.Raycast(viewPoint.position, dirToTarget, distToTarget, obstacleLayerMask))
                        continue;

                    isTargetOn = true;

                    Debug.DrawRay(viewPoint.position, dirToTarget * distToTarget, Color.red);
                    return;
                }
            }


        }
        private class TraceState : MonsterState
        {
            public TraceState(MonsterController monster) : base(monster)
            {
                //추적 애니메이션 재생 및 플레이어 쪽으로 이동 필요. 
            }
            public override void Enter()
            {

            }
            public override void Update()
            {
                FindTarget();
                Debug.Log("추적상태");
            }
            public override void Transition()
            {
                if(monster.isTriggerOn==true) //트리거가 enter 라면 exit는 false 니까 공격 실행 중 exit 되면 다시 trace로
                {
                    ChangeState(State.Attack);
                }

                //플레이어를 잃고 어느정도 시간이 지나면 return으로 변화 --> 원래 위치로 돌아가는 작업. 

                if(size==0)
                {
                    Debug.Log("size 0 ");
                    ChangeState(State.Return); //이거 그냥 overlap을 조금 크게 잡죠? 
                }
            }

            Collider[] colliders = new Collider[20];
            private void FindTarget()
            {
                size = Physics.OverlapSphereNonAlloc(viewPoint.position, range, colliders, targetLayerMask);
                for (int i = 0; i < size; i++)
                {
                    dirToTarget = (colliders[i].transform.position - viewPoint.position).normalized;

                    if (Vector3.Dot(viewPoint.forward, dirToTarget) < CosAngle)
                        continue;

                    distToTarget = Vector3.Distance(colliders[i].transform.position, viewPoint.position);
                    if (Physics.Raycast(viewPoint.position, dirToTarget, distToTarget, obstacleLayerMask))
                        continue;

                    transform.LookAt(colliders[i].transform.position);
                    transform.position = Vector3.MoveTowards(transform.position, colliders[i].transform.position,
                        moveSpeed * Time.deltaTime);



                    Debug.DrawRay(viewPoint.position, dirToTarget * distToTarget, Color.red);
                    return;
                }
            }
        }

        
        private class AttackState : MonsterState
        {
            public AttackState(MonsterController monster) : base(monster)
            {

            }

            public override void Enter()
            {
                //공격 애니메이션 재생 
            }
            public override void Update()
            {
                Debug.Log("공격상태");
            }

            public override void Transition()
            {
                /*if (animator.GetCurrentAnimatorStateInfo(0).IsName(monster.attack) == true)
                {
                    if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime>=1f) //애니메이션 종료. 
                    {
                        ChangeState(State.Trace);
                    }

                }*/
            }
        }
        private class ReturnState : MonsterState
        {
            public ReturnState(MonsterController monster) : base(monster)
            {
                
            }
            public override void Enter()
            {
                //걷는 애니메이션 재생. 
            }

            public override void Update()
            {
                FindTarget();

                transform.position = Vector3.MoveTowards(transform.position, monster.startPos.position,
                         moveSpeed * Time.deltaTime);
            }

            public override void Transition()
            {
                if (Vector3.Distance(monster.startPos.position, transform.position) < 0.1f)
                {
                    ChangeState(State.Idle);
                }
            }

            Collider[] colliders = new Collider[20];
            private void FindTarget()
            {
                size = Physics.OverlapSphereNonAlloc(viewPoint.position, range, colliders, targetLayerMask);
                for (int i = 0; i < size; i++)
                {
                    dirToTarget = (colliders[i].transform.position - viewPoint.position).normalized;

                    if (Vector3.Dot(viewPoint.forward, dirToTarget) < CosAngle)
                        continue;

                    distToTarget = Vector3.Distance(colliders[i].transform.position, viewPoint.position);
                    if (Physics.Raycast(viewPoint.position, dirToTarget, distToTarget, obstacleLayerMask))
                        continue;

                    ChangeState(State.Trace); //다시 시야 내부에 들어오면. 

                    Debug.DrawRay(viewPoint.position, dirToTarget * distToTarget, Color.red);
                    return;
                }
            }

        }

        private void Update()
        {
            stateMachine.Update();

        }

        private void FixedUpdate()
        {
            onGround = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);
            stateMachine.FixedUpdate();
            
        }


        private void OnTriggerEnter(Collider other)
        {
            if (Extension.Contain(targetLayerMask,other.gameObject.layer))
            {
                isTriggerOn = true;
            }
            
        }
        private void OnTriggerExit(Collider other)
        {
            if (Extension.Contain(targetLayerMask, other.gameObject.layer))
            {
                isTriggerOn = false;
            }
        }



        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(viewPoint.position, range);

            Vector3 rightDir = Quaternion.Euler(0, angle * 0.5f, 0) * viewPoint.forward;
            Vector3 leftDir = Quaternion.Euler(0, angle * -0.5f, 0) * viewPoint.forward;

            Debug.DrawRay(viewPoint.position, rightDir * range, Color.cyan);
            Debug.DrawRay(viewPoint.position, leftDir * range, Color.cyan);
        }
    }
}
