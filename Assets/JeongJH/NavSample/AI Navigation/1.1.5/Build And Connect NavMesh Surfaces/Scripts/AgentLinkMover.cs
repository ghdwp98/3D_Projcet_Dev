using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Unity.AI.Navigation.Samples
{
    public enum OffMeshLinkMoveMethod
    {
        Teleport,
        NormalSpeed,
        Parabola,
        Curve
    }

    /// <summary>
    /// Move an agent when traversing a OffMeshLink given specific animated methods
    /// </summary>
    [RequireComponent(typeof(NavMeshAgent))]
    public class AgentLinkMover : MonoBehaviour
    {

        [Header("Spec")]
        [SerializeField] Transform viewPoint;
        [SerializeField] LayerMask targetLayerMask;
        [SerializeField] float range;
        [SerializeField] GameObject[] npcDialog;

        private int m_NextGoal = 0;
        Rigidbody rigid;

        [Header("NpcDialogue")]
        int dialogCount = 0;
        

        public OffMeshLinkMoveMethod m_Method = OffMeshLinkMoveMethod.Parabola;
        public AnimationCurve m_Curve = new AnimationCurve();
        NavMeshAgent agent;
        [SerializeField] Transform[] destinations;


        // 0번 목표를 플레이어로 해서 그냥 0번 dialog를 첫 번 만남 대사로 진행하면 될듯함. 
        // 

        IEnumerator Start()
        {
            rigid = GetComponent<Rigidbody>();
            agent = GetComponent<NavMeshAgent>();
            agent.autoTraverseOffMeshLink = false;
            while (true)
            {

                if (agent.isOnOffMeshLink) 
                {
                    if (m_Method == OffMeshLinkMoveMethod.NormalSpeed)
                        yield return StartCoroutine(NormalSpeed(agent));
                    else if (m_Method == OffMeshLinkMoveMethod.Parabola)
                        yield return StartCoroutine(Parabola(agent, 2.0f, 0.5f));
                    else if (m_Method == OffMeshLinkMoveMethod.Curve)
                        yield return StartCoroutine(Curve(agent, 0.5f));
                    agent.CompleteOffMeshLink();
                }

                yield return null;
            }
        }


        private void Update()
        {

            FindTarget();
            NextDest();

        }

        Collider[] colliders = new Collider[20];
        private void FindTarget()
        {
            int size = Physics.OverlapSphereNonAlloc(viewPoint.position, range, colliders, targetLayerMask);
            for (int i = 0; i < size; i++)
            {
                float distToTarget = Vector3.Distance(colliders[i].transform.position, viewPoint.position);
                {
                    if (distToTarget < range) //범위내에 player가 있으면 
                    {
                        agent.isStopped = false;
                        agent.SetDestination(destinations[m_NextGoal].transform.position);
                    }
                    else //플레이어가 범위 내에 있으면 대기. -->아 점프순간에 저장이 되어버려서 다시 돌아오는 상황임. 
                    {
                        agent.isStopped = true;
                    }
                }
            }
        }


        private void NextDest()
        {
            float distance = Vector3.Distance(agent.transform.position, destinations[m_NextGoal].position);
            if (distance < 2f) // 다음 위치로 이동하도록 함. 
            {
                DialogSetOn(dialogCount);

                if (m_NextGoal < destinations.Length - 1)
                {
                    m_NextGoal++;
                }
                agent.SetDestination(destinations[m_NextGoal].position);
            }

        }


        private void DialogSetOn(int count)
        {
            npcDialog[dialogCount].SetActive(true); //흠..?
            dialogCount++; // 다음 번에 다음 패널을 불러오도록 

        }




        IEnumerator NormalSpeed(NavMeshAgent agent)
        {
            OffMeshLinkData data = agent.currentOffMeshLinkData;
            Vector3 endPos = data.endPos + Vector3.up * agent.baseOffset;
            while (agent.transform.position != endPos)
            {
                agent.transform.position =
                    Vector3.MoveTowards(agent.transform.position, endPos, agent.speed * Time.deltaTime);
                yield return null;
            }
        }

        IEnumerator Parabola(NavMeshAgent agent, float height, float duration) //포물선 
        {
            OffMeshLinkData data = agent.currentOffMeshLinkData;
            Vector3 startPos = agent.transform.position;
            Vector3 endPos = data.endPos + Vector3.up * agent.baseOffset;
            float normalizedTime = 0.0f;
            while (normalizedTime < 1.0f)
            {
                float yOffset = height * 4.0f * (normalizedTime - normalizedTime * normalizedTime);
                agent.transform.position = Vector3.Lerp(startPos, endPos, normalizedTime) + yOffset * Vector3.up;
                normalizedTime += Time.deltaTime / duration;
                yield return null;
            }
        }

        IEnumerator Curve(NavMeshAgent agent, float duration)
        {
            OffMeshLinkData data = agent.currentOffMeshLinkData;
            Vector3 startPos = agent.transform.position;
            Vector3 endPos = data.endPos + Vector3.up * agent.baseOffset;
            float normalizedTime = 0.0f;
            while (normalizedTime < 1.0f)
            {
                float yOffset = m_Curve.Evaluate(normalizedTime);
                agent.transform.position = Vector3.Lerp(startPos, endPos, normalizedTime) + yOffset * Vector3.up;
                normalizedTime += Time.deltaTime / duration;
                yield return null;
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(viewPoint.position, range);
        }



    }
}