using Cinemachine;
using System.Collections;
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
    Material outLine;
    Renderer renderer;
    List<Material> materialList=new List<Material>();



    //이 함수 사용 자제하자. 
    public static Vector3 GetActiveCheckPointPosition() //외부에서 불러오는 거임 -->순서대로 저장되서 최신부터
    {
        Vector3 result = new Vector3(0, 0, 0); //defalut 포지션이라서 이거 0 0 0 이면 최초 씬 불러오는걸로 작업해도될듯. 

        if (checkPointList != null)
        {

            foreach (GameObject cp in checkPointList)
            {

                if (cp.GetComponent<CheckPoint>().Activated) //트루라면 저장되는거 아닌가?
                {
                    
                    //result = cp.transform.position + new Vector3(1, 0, 1); //딱 겹치지 않도록 
                    result = GameManager.playerPos + new Vector3(1, 0, 1);
                    break;
                }
            }
        }

        return result; //cp를 통해서 장소를 저장하는 것 같은데? 
    }

    private void ActivateCheckPoint()
    {
        
        Activated = true;
        GameManager.playerPos = transform.position;
        GameManager.saved = true;
        Debug.Log(GameManager.saved); //ture 되는거 맞는데??????? 
        Debug.Log(GameManager.playerPos);
        /*foreach (GameObject cp in checkPointList) //이 함수 부르면 true로 바뀌는데? 
        {
            cp.GetComponent<CheckPoint>().Activated = false;
            //아마 밑 부분은 추가 일듯? 
            GameManager.playerPos= cp.transform.position;
            GameManager.saved = true;

            //cp.GetComponent<Animator>().SetBool("Active", false); -->애니메이션이 있다면 
        }*/

        //현재의 액티브 된 체크포인트를 활성화 시킴.
        //Activated = true;
        // thisAnimator.SetBool("Active",true);

    }

    private void Start() //아웃라인 적용 실행해보기. 
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
            ActivateCheckPoint(); //키 없이 그냥 트리거 반경 안에 들어가면 바로 체크포인트 발동. 
        }        
        
    }

    private void OnTriggerExit(Collider other)
    {
        
        
    }

    Coroutine routine;
    IEnumerator cameraRoutine()
    {
        virtualCamera.Priority = 11;
        instance = Instantiate(checkPrefab, pointer.transform.position, Quaternion.identity); //머리 위에 뜨는 프리팹.
        yield return new WaitForSeconds(1f);
        virtualCamera.Priority = 9;
        Destroy(instance);
        
    }


}
