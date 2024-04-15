using System.Collections;
using TMPro;
using UnityEngine;

public class LimitHidingObjectParent : MonoBehaviour
{
    BoxCollider boxCollider; //콜라이더 -->상호작용되면 삭제해주고 trigger 벗어나면 다시 생김. 
    Material material;
    GameObject playerObj;

    [SerializeField] LayerMask playerLayer;
    float[] coolTime;
    float []sumTime;
    float[] textTime;
    bool runCoro;
    [SerializeField] Transform returnPos;
    [SerializeField] TextMeshProUGUI text;

    //각 숨는 오브젝트들이 자신의 번호를 가짐.
    [SerializeField] int id; 


    private float count; // 쫓아내기 
    bool[] available; // 다시 이용할 때 까지 대기 가능한 시간. 

    bool[] isHiding; //bool 배열로 각각의 오브젝트들의 상황을 제어 

    // 쫓겨나면 일정시간 이용불가 --> 프리팹으로 text띄워주기 (이용불가 텍스트 )

    //아 이게 .. 다 같은 변수를 공유하니까 마이너스가 나와버리고 그러네 .. 


    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        material = GetComponent<MeshRenderer>().material;
        material.color = new Color32(255, 255, 255, 255);
        isHiding = new bool[] { false, false, false, false };
        available = new bool[] {true,true, true, true };
        count = 0;
        sumTime = new float[] { 0, 0, 0, 0 };
        textTime = new float[] { 0, 0, 0, 0 };
        coolTime = new float[] { 30, 30, 30, 30, };

        returnPos = transform.GetChild(0);
        text.enabled = false;
        playerObj = GameObject.FindWithTag("Player");

    }

    private void Update()
    {

        if (isHiding[id] == true) //숨어있는 상태라면 
        {
            count += Time.deltaTime;
            if (count > 9f) //9초 이후 
            {
                CharacterController characterController = playerObj.GetComponent<CharacterController>();
                characterController.enabled = false;

                StartCoroutine(KickTextOn());
                //자식 오브젝트의 위치로 쫓겨나게 하기. 
                playerObj.transform.position = returnPos.transform.position;

                characterController.enabled = true;
                isHiding[id] = false;
                available[id] = false; //쫓겨나면 일정시간 이용하지 못하도록 하기. 
                count = 0; //카운트 초기화 . 
            }
        }

        if (available[id] == false) // 시간 누적 후 false로 바꿔주기. 
        {
            sumTime[id] += Time.deltaTime;  //쿨타임과 비교할 누적숫자.

            textTime[id] += Time.deltaTime; // 코루틴에 띄워줄 누적숫자. 

            if (sumTime[id] > coolTime[id]) //정해둔 쿨타임이 지나면 다시 이용가능하도록. 
            {
                available[id] = true;
                sumTime[id] = 0f;
                textTime[id] = 0f;
                

            }
        }
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.CompareTag("Player") && Input.GetKeyDown(KeyCode.X)
            && available[id] == true) //comparetag가 훨씬 성능좋음. 
        {
            boxCollider.enabled = false;
            CharacterController characterController = other.gameObject.GetComponent<CharacterController>();
            if (characterController != null)
            {
                characterController.enabled = false;
                other.gameObject.transform.position = transform.position;
                characterController.enabled = true;
                material.color = new Color32(255, 255, 255, 150);
                other.gameObject.layer = 28; //hide 레이어로 변경해보기. 
                isHiding[id] = true;

            }
        }

        else if (other.gameObject.CompareTag("Player") && Input.GetKeyDown(KeyCode.X)
            && available[id] == false) //이용불가능하다는 문구를 띄워줘야함. 
        {
            if (runCoro == true)
            {
                return;
            }
            else if (runCoro == false)
            {
                StartCoroutine(TextOnCoroutine(textTime[id]));
            }

        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) //comparetag가 훨씬 성능좋음.
        {
            boxCollider.enabled = true;
            material.color = new Color32(255, 255, 255, 255);
            other.gameObject.layer = 10; //다시 플레이어의 레이어로 변경. 
            isHiding[id] = false; // 다시 안숨은 상태로 
            count = 0; //카운트 초기화 (숨는시간 초기화)
        }
    }

    IEnumerator TextOnCoroutine(float textTime)
    {
        runCoro = true;
        text.enabled = true;
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
        text.text = $"아직 이용 할 수 없습니다. 남은시간 :{(int)(coolTime[id] - textTime)}";

        yield return new WaitForSeconds(1f);
        while (text.color.a > 0.01f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b,
                text.color.a - (Time.deltaTime / 1.5f));
            yield return null;
        }
        text.enabled = false;
        runCoro = false;
        yield return null;

    }

    IEnumerator KickTextOn()
    {
        runCoro = true;
        text.enabled = true;
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
        text.text = "너무 오래 숨어있었다."; 

        yield return new WaitForSeconds(1f);
        while (text.color.a > 0.01f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b,
                text.color.a - (Time.deltaTime / 1.5f));
            yield return null;
        }
        text.enabled = false;
        runCoro = false;
        yield return null;
    }


}
