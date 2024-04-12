using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Unity.AI.Navigation.Samples
{
    public enum Method
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
    public class AgentNpc : MonoBehaviour
    {

        [Header("Spec")]
        [SerializeField] Transform viewPoint;
        [SerializeField] LayerMask targetLayerMask;
        [SerializeField] float range;

        public bool isHide; //숨어있는 상태 true 아닌상태 false


        private int m_NextGoal = 0;
        Rigidbody rigid;

        [Header("NpcDialogue")]
        int dialogCount = 0;

        [SerializeField]
        DialogSystem[] dialogSystems;

        [SerializeField] GameObject panel;
        MeshRenderer meshRenderer;


        public Method m_Method = Method.Parabola;
        public AnimationCurve m_Curve = new AnimationCurve();
        NavMeshAgent agent;
        [SerializeField] Transform[] destinations;


        // 0번 목표를 플레이어로 해서 그냥 0번 dialog를 첫 번 만남 대사로 진행하면 될듯함. 
        // + 특정 이벤트 지역들에 들어가면 잠시 사라졌다가 다시 나와야 하는데 
        // 차라리 들어갈 때 마다 active fasle 해주고 다른 npc를 미리 준비해 두는게 나을 수 도 있음. 



        IEnumerator Start()
        {
            isHide = false; //일단 false로 초기화. 
            rigid = GetComponent<Rigidbody>();
            agent = GetComponent<NavMeshAgent>();
            meshRenderer= GetComponent<MeshRenderer>();
            agent.autoTraverseOffMeshLink = false;
            while (true)
            {

                if (agent.isOnOffMeshLink)
                {
                    if (m_Method == Method.NormalSpeed)
                        yield return StartCoroutine(NormalSpeed(agent));
                    else if (m_Method == Method.Parabola)
                        yield return StartCoroutine(Parabola(agent, 2.0f, 0.5f));
                    else if (m_Method == Method.Curve)
                        yield return StartCoroutine(Curve(agent, 0.5f));
                    agent.CompleteOffMeshLink();
                }

                yield return null;
            }
        }


        private void Update()
        {
            if(isHide==false)
            {
                agent.isStopped = false;
                // 투명 해제 
                meshRenderer.enabled = true;
                FindTarget();
                NextDest();
            }
            else //숨어있는 상태라면. (hide 가 true) 
            {
                meshRenderer.enabled = false;
                agent.isStopped = true; //stop이 안된다?? 
                agent.transform.localPosition = new Vector3(0, 0, 0);
            }

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
                    else //플레이어가 범위 내에 없으면  대기. -->아 점프순간에 저장이 되어버려서 다시 돌아오는 상황임. 
                    {
                        agent.isStopped = true;
                    }
                }
            }
        }


        private void NextDest()
        {
            float distance = Vector3.Distance(agent.transform.position, destinations[m_NextGoal].position);
            if (distance < 1.5f) // 다음 위치로 이동하도록 함. 
            {
                // 특정 위치에 도달하면 대화 코루틴 스타트. 
                StartCoroutine(DialogSetOn(dialogCount));

                if (m_NextGoal < destinations.Length - 1)
                {
                    m_NextGoal++;
                }
                agent.SetDestination(destinations[m_NextGoal].position);
            }

        }


        // 이 부분 어차피 반복이니까  list형태로 관리해주자. 
        private IEnumerator DialogSetOn(int count)
        {
            //count를 통해 배열 접근 
            panel.SetActive(true);
            dialogSystems[count].gameObject.SetActive(true);
            Time.timeScale = 0f;
            yield return new WaitUntil(() => dialogSystems[count].UpdateDialog());
            Time.timeScale = 1f;
            dialogSystems[count].gameObject.SetActive(false); //어차피 count로 하니까 대사 다시 안나올듯 ? 
            if(dialogCount<dialogSystems.Length-1)
            {
                dialogCount++; // 다음 번에 다음 패널을 불러오도록 
            }        
            panel.SetActive(false);

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

        private void OnTriggerEnter(Collider other) //특정한 레이어에 닿으면 잠시 사라져야함. 
        {
            
        }

    }
}