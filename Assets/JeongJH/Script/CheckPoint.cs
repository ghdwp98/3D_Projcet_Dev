using Cinemachine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] GameObject pointer;
    [SerializeField] GameObject checkPrefab; //임시로 circle prefab 이용함. 
    [SerializeField] CinemachineVirtualCamera virtualCamera;
    //private Animator thisAnimator; -->애니메이션이 있다면. 

    GameObject instance;
    public bool Activated = false;
    public static List<GameObject> checkPointList;


    public static Vector3 GetActiveCheckPointPosition() //외부에서 불러오는 거임 -->순서대로 저장되서 최신부터
    {
        Vector3 result = new Vector3(0, 0, 0); //defalut 포지션이라서 이거 0 0 0 이면 최초 씬 불러오는걸로 작업해도될듯. 

        if(checkPointList!=null)
        {
            
            foreach(GameObject cp in checkPointList)
            {
                
                if (cp.GetComponent<CheckPoint>().Activated) //트루라면 저장되는거 아닌가?
                {
                    Debug.Log("함수 진입함");
                    result = cp.transform.position + new Vector3(1, 0, 1); //딱 겹치지 않도록 
                    break; 
                }
            }
        }
        return result; //cp를 통해서 장소를 저장하는 것 같은데? 
    }

    private void ActivateCheckPoint()
    {
        foreach(GameObject cp in checkPointList) //이 함수 부르면 true로 바뀌는데? 
        {
            Debug.Log("체크포인트 저장");
            cp.GetComponent<CheckPoint>().Activated = false;
            //cp.GetComponent<Animator>().SetBool("Active", false); -->애니메이션이 있다면 
        }
        
        //현재의 액티브 된 체크포인트를 활성화 시킴.
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
        virtualCamera.Priority = 11; //접근하면 시네머신 우선순위 변경으로 카메라조정

        if (Input.GetKeyDown(KeyCode.X)) //여기서 키 누르는 작업하면 될듯 stay중 계속 되네. 
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
