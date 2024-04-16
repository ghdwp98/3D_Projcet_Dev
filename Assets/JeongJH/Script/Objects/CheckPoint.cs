using Cinemachine;
using System.Collections;
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
    Material outLine;
    Renderer renderer;
    List<Material> materialList=new List<Material>();



    //�� �Լ� ��� ��������. 
    public static Vector3 GetActiveCheckPointPosition() //�ܺο��� �ҷ����� ���� -->������� ����Ǽ� �ֽź���
    {
        Vector3 result = new Vector3(0, 0, 0); //defalut �������̶� �̰� 0 0 0 �̸� ���� �� �ҷ����°ɷ� �۾��ص��ɵ�. 

        if (checkPointList != null)
        {

            foreach (GameObject cp in checkPointList)
            {

                if (cp.GetComponent<CheckPoint>().Activated) //Ʈ���� ����Ǵ°� �ƴѰ�?
                {
                    
                    //result = cp.transform.position + new Vector3(1, 0, 1); //�� ��ġ�� �ʵ��� 
                    result = GameManager.playerPos + new Vector3(1, 0, 1);
                    break;
                }
            }
        }

        return result; //cp�� ���ؼ� ��Ҹ� �����ϴ� �� ������? 
    }

    private void ActivateCheckPoint()
    {
        
        Activated = true;
        GameManager.playerPos = transform.position;
        GameManager.saved = true;
        Debug.Log(GameManager.saved); //ture �Ǵ°� �´µ�??????? 
        Debug.Log(GameManager.playerPos);
        /*foreach (GameObject cp in checkPointList) //�� �Լ� �θ��� true�� �ٲ�µ�? 
        {
            cp.GetComponent<CheckPoint>().Activated = false;
            //�Ƹ� �� �κ��� �߰� �ϵ�? 
            GameManager.playerPos= cp.transform.position;
            GameManager.saved = true;

            //cp.GetComponent<Animator>().SetBool("Active", false); -->�ִϸ��̼��� �ִٸ� 
        }*/

        //������ ��Ƽ�� �� üũ����Ʈ�� Ȱ��ȭ ��Ŵ.
        //Activated = true;
        // thisAnimator.SetBool("Active",true);

    }

    private void Start() //�ƿ����� ���� �����غ���. 
    {
        //thisAnimator=GetComponent<Animator>();
        checkPointList = GameObject.FindGameObjectsWithTag("CheckPoint").ToList();
    }

    private void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player" && instance == null)
        {
            StartCoroutine(cameraRoutine());
            ActivateCheckPoint(); //Ű ���� �׳� Ʈ���� �ݰ� �ȿ� ���� �ٷ� üũ����Ʈ �ߵ�. 
        }        
        
    }

    private void OnTriggerExit(Collider other)
    {
        
        
    }

    Coroutine routine;
    IEnumerator cameraRoutine()
    {
        virtualCamera.Priority = 11;
        instance = Instantiate(checkPrefab, pointer.transform.position, Quaternion.identity); //�Ӹ� ���� �ߴ� ������.
        yield return new WaitForSeconds(1f);
        virtualCamera.Priority = 9;
        Destroy(instance);
        
    }


}
