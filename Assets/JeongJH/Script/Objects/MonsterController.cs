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
        [SerializeField] Transform startPos; //�̰� �׳� �� ������Ʈ �־ �ұ� �����. 

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
            Idle, Trace, Return, Attack //���� �ް� �װ� �ϴ°� ���� �����߿� �߰��ϸ� ���� 
        }
        public State stateEnum;

        private float preAngle;
        private float cosAngle;

        //target ���̾ player�� �ּ� �÷��̾��� ���̾ �ٲ�� (������ ) �����ð� ����ϴٰ� return�ϱ�. 
        // �ڸ� ���� ������ angle�� ������ �ʱ� ������ �ɸ��� ����. 

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
                //������ NAVMESH �̿��� �� ������Ʈ�� ����. 
            }

            public override void Enter()
            {
                isTargetOn = false; //������ �ٽ� idle�� ���ƿ��� false �Ǵϱ� �ٽ� �ٷ� Ʈ�����Ǿȵ�. 
            }
            public override void Update()
            {
                Debug.Log("idel ����");
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
                //���� �ִϸ��̼� ��� �� �÷��̾� ������ �̵� �ʿ�. 
            }
            public override void Enter()
            {

            }
            public override void Update()
            {
                FindTarget();
                Debug.Log("��������");
            }
            public override void Transition()
            {
                if(monster.isTriggerOn==true) //Ʈ���Ű� enter ��� exit�� false �ϱ� ���� ���� �� exit �Ǹ� �ٽ� trace��
                {
                    ChangeState(State.Attack);
                }

                //�÷��̾ �Ұ� ������� �ð��� ������ return���� ��ȭ --> ���� ��ġ�� ���ư��� �۾�. 

                if(size==0)
                {
                    Debug.Log("size 0 ");
                    ChangeState(State.Return); //�̰� �׳� overlap�� ���� ũ�� ����? 
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
                //���� �ִϸ��̼� ��� 
            }
            public override void Update()
            {
                Debug.Log("���ݻ���");
            }

            public override void Transition()
            {
                /*if (animator.GetCurrentAnimatorStateInfo(0).IsName(monster.attack) == true)
                {
                    if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime>=1f) //�ִϸ��̼� ����. 
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
                //�ȴ� �ִϸ��̼� ���. 
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

                    ChangeState(State.Trace); //�ٽ� �þ� ���ο� ������. 

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
