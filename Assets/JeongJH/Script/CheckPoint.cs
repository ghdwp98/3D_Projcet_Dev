using Cinemachine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] GameObject pointer;
    [SerializeField] GameObject checkPrefab; //�ӽ÷� circle prefab �̿���. 
    [SerializeField] CinemachineVirtualCamera virtualCamera;
    //private Animator thisAnimator; -->�ִϸ��̼��� �ִٸ�. 

    GameObject instance;
    public bool Activated = false;
    public static List<GameObject> checkPointList;


    public static Vector3 GetActiveCheckPointPosition() //�ܺο��� �ҷ����� ���� -->������� ����Ǽ� �ֽź���
    {
        Vector3 result = new Vector3(0, 0, 0); //defalut �������̶� �̰� 0 0 0 �̸� ���� �� �ҷ����°ɷ� �۾��ص��ɵ�. 

        if(checkPointList!=null)
        {
            
            foreach(GameObject cp in checkPointList)
            {
                
                if (cp.GetComponent<CheckPoint>().Activated) //Ʈ���� ����Ǵ°� �ƴѰ�?
                {
                    Debug.Log("�Լ� ������");
                    result = cp.transform.position + new Vector3(1, 0, 1); //�� ��ġ�� �ʵ��� 
                    break; 
                }
            }
        }
        return result; //cp�� ���ؼ� ��Ҹ� �����ϴ� �� ������? 
    }

    private void ActivateCheckPoint()
    {
        foreach(GameObject cp in checkPointList) //�� �Լ� �θ��� true�� �ٲ�µ�? 
        {
            Debug.Log("üũ����Ʈ ����");
            cp.GetComponent<CheckPoint>().Activated = false;
            //cp.GetComponent<Animator>().SetBool("Active", false); -->�ִϸ��̼��� �ִٸ� 
        }
        
        //������ ��Ƽ�� �� üũ����Ʈ�� Ȱ��ȭ ��Ŵ.
        Activated = true;
        // thisAnimator.SetBool("Active",true);
        
    }

    private void Start()
    {
        //thisAnimator=GetComponent<Animator>();
        checkPointList = GameObject.FindGameObjectsWithTag("CheckPoint").ToList();
    }

    private void Update()
    {
       
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.tag == "Player" && instance == null)
        {
            instance = Instantiate(checkPrefab, pointer.transform.position, Quaternion.identity);
        }
        virtualCamera.Priority = 11; //�����ϸ� �ó׸ӽ� �켱���� �������� ī�޶�����

        if (Input.GetKeyDown(KeyCode.X)) //���⼭ Ű ������ �۾��ϸ� �ɵ� stay�� ��� �ǳ�. 
        {
            ActivateCheckPoint();
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Destroy(instance);
        virtualCamera.Priority = 9;
    }
}
