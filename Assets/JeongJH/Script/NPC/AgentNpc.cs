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

        public bool isHide; //�����ִ� ���� true �ƴѻ��� false


        private int m_NextGoal = 0;
        Rigidbody rigid;

        [Header("NpcDialogue")]
        public int dialogCount = 0;

        [SerializeField]
        DialogSystem[] dialogSystems;

        [SerializeField] GameObject panel;
        MeshRenderer meshRenderer;


        public Method m_Method = Method.Parabola;
        public AnimationCurve m_Curve = new AnimationCurve();
        NavMeshAgent agent;
        [SerializeField] Transform[] destinations;
        [SerializeField] SkinnedMeshRenderer[] allMesh;




        bool isRoutine;
        


        // 0�� ��ǥ�� �÷��̾�� �ؼ� �׳� 0�� dialog�� ù �� ���� ���� �����ϸ� �ɵ���. 
        // + Ư�� �̺�Ʈ �����鿡 ���� ��� ������ٰ� �ٽ� ���;� �ϴµ� 
        // ���� �� �� ���� active fasle ���ְ� �ٸ� npc�� �̸� �غ��� �δ°� ���� �� �� ����. 



        IEnumerator Start()
        {
            isHide = false; //�ϴ� false�� �ʱ�ȭ. 
            rigid = GetComponent<Rigidbody>();
            agent = GetComponent<NavMeshAgent>();
            meshRenderer = GetComponent<MeshRenderer>();
            dialogCount = GameManager.NpcDialogCount;
            
           
            m_NextGoal = GameManager.staticNextGoal; //���� �����صα�. 
            isRoutine = false;
            Debug.Log("npc�� start���� �����ϴ� ī��Ʈ ����. " + GameManager.NpcDialogCount);
            Debug.Log("npc��ġ"+transform.position);

            allMesh = GetComponentsInChildren<SkinnedMeshRenderer>();

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
            if (isHide == false)
            {
                agent.isStopped = false;
                // ���� ���� 
                meshRenderer.enabled = true;
                FindTarget();
                NextDest();

                //������Ʈ �߿��� �׳� �ȱ� �ִϸ��̼Ǹ� ����? �ϴ� �־��. 

                for(int i = 0; i < allMesh.Length; i++)
                {
                    allMesh[i].enabled = true;
                }
                


            }
            else //�����ִ� ���¶��. (hide �� true) 
            {
                meshRenderer.enabled = false;
                for (int i = 0; i < allMesh.Length; i++)
                {
                    allMesh[i].enabled = true;
                }

                agent.isStopped = true; //stop�� �ȵȴ�?? 
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
                    if (distToTarget < range) //�������� player�� ������ 
                    {
                        agent.isStopped = false;
                        agent.SetDestination(destinations[m_NextGoal].transform.position);
                        
                    }
                    else //�÷��̾ ���� ���� ������  ���. -->�� ���������� ������ �Ǿ������ �ٽ� ���ƿ��� ��Ȳ��. 
                    {
                        agent.isStopped = true;
                        
                    }
                }
            }
        }


        private void NextDest()
        {
            float distance = Vector3.Distance(agent.transform.position, destinations[m_NextGoal].position);


            if(distance>2f)
            {
                Vector3 to = new Vector3(agent.destination.x, 0, agent.destination.z);
                Vector3 from = new Vector3(transform.position.x, 0, transform.position.z);
                transform.rotation = Quaternion.LookRotation(to - from);

                //�ϴ� Ŭ ���� ȸ�� ���Ѻ���? 
            }


            if (distance < 2f) // ���� ��ġ�� �̵��ϵ��� ��. 
            {
                // Ư�� ��ġ�� �����ϸ� ��ȭ �ڷ�ƾ ��ŸƮ. 
                
                if(dialogCount<dialogSystems.Length&&isRoutine==false)
                {
                    // ���⼭ idle�� �����ϰ� ��ȭ����
                    
                    StartCoroutine(DialogSetOn(dialogCount));
                }
                if (m_NextGoal < destinations.Length - 1)
                {
                    m_NextGoal++;
                    GameManager.staticNextGoal++; //����ƽ �ؽ�Ʈ�� �������Ѽ� ���� �������� ����ص־���. 
                    Debug.Log("���� ��ǥ ��ȣ:"+ GameManager.staticNextGoal);

                }
                agent.SetDestination(destinations[m_NextGoal].position);

            }

        }


        // �� �κ� ������ �ݺ��̴ϱ�  list���·� ����������. 
        private IEnumerator DialogSetOn(int count)
        {
            isRoutine = true;
            //count�� ���� �迭 ���� 
            panel.SetActive(true);
            Debug.Log("ī��Ʈ " + count);
            dialogSystems[count].gameObject.SetActive(true);
            Time.timeScale = 0f;
            Debug.Log("�ð�" + Time.timeScale);
            yield return new WaitUntil(() => dialogSystems[count].UpdateDialog());
            Time.timeScale = 1f;
            Debug.Log("�ð�" + Time.timeScale);
            //���� �� �κ��� ������ Ʈ���Ŵ�簡 �ڵ����� ����ǰ� �����ƿ��� �ְŵ��
            dialogSystems[count].gameObject.SetActive(false); //������ count�� �ϴϱ� ��� �ٽ� �ȳ��õ� ?          
            dialogCount++; // ���� ���� ���� �г��� �ҷ������� 
            GameManager.NpcDialogCount++; //�̰� �����صΰ�. 
            Debug.Log("npc�� ī��Ʈ�� ������Ű�� ����. -->" + GameManager.NpcDialogCount);
            panel.SetActive(false);
            isRoutine = false;

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

        IEnumerator Parabola(NavMeshAgent agent, float height, float duration) //������ 
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

        private void OnTriggerEnter(Collider other) //Ư���� ���̾ ������ ��� ���������. 
        {

        }

    }
}