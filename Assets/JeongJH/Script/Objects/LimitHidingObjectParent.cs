using System.Collections;
using TMPro;
using UnityEngine;

public class LimitHidingObjectParent : MonoBehaviour
{
    BoxCollider boxCollider; //콜라이더 -->상호작용되면 삭제해주고 trigger 벗어나면 다시 생김. 
    Material material;
    GameObject playerObj;

    [SerializeField] LayerMask playerLayer;
    [SerializeField] float coolTime;
    float sumTime;
    float textTime;
    bool runCoro;
    [SerializeField] Transform returnPos;
    [SerializeField] TextMeshProUGUI text;

    public float count; // 쫓아내기 
    public bool available; // 다시 이용할 때 까지 대기 가능한 시간. 
    bool isHiding;

    // 쫓겨나면 일정시간 이용불가 --> 프리팹으로 text띄워주기 (이용불가 텍스트 )


    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        material = GetComponent<MeshRenderer>().material;
        material.color = new Color32(255, 255, 255, 255);
        isHiding = false;
        available = true;

        returnPos = transform.GetChild(0);
        text.enabled = false;
        playerObj = GameObject.FindWithTag("Player");

    }

    private void Update()
    {

        if (isHiding == true) //숨어있는 상태라면 
        {
            count += Time.deltaTime;
            if (count > 5f)
            {
                CharacterController characterController = playerObj.GetComponent<CharacterController>();
                characterController.enabled = false;

                //자식 오브젝트의 위치로 쫓겨나게 하기. 
                playerObj.transform.position = returnPos.transform.position;

                characterController.enabled = true;
                isHiding = false;
                available = false; //쫓겨나면 일정시간 이용하지 못하도록 하기. 
                count = 0; //카운트 초기화 . 
            }
        }

        if (available == false) // 시간 누적 후 false로 바꿔주기. 
        {
            sumTime += Time.deltaTime;  //쿨타임과 비교할 누적숫자.
            textTime += Time.deltaTime; // 코루틴에 띄워줄 누적숫자. 

            if (sumTime > coolTime) //정해둔 쿨타임이 지나면 다시 이용가능하도록. 
            {
                available = true;
                sumTime = 0;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.CompareTag("Player") && Input.GetKeyDown(KeyCode.X)
            && available == true) //comparetag가 훨씬 성능좋음. 
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
                isHiding = true;


            }
        }

        else if (other.gameObject.CompareTag("Player") && Input.GetKeyDown(KeyCode.X)
            && available == false) //이용불가능하다는 문구를 띄워줘야함. 
        {
            if (runCoro == true)
            {
                return;
            }
            else if (runCoro == false)
            {
                StartCoroutine(TextOnCoroutine(textTime));
            }


        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) //comparetag가 훨씬 성능좋음.
        {
            boxCollider.enabled = true;
            material.color = new Color32(255, 255, 255, 255);
            other.gameObject.layer = 9; //다시 플레이어의 레이어로 변경. 
            isHiding = false; // 다시 안숨은 상태로 
            count = 0; //카운트 초기화 (숨는시간 초기화)
        }
    }

    IEnumerator TextOnCoroutine(float textTime)
    {
        runCoro = true;
        text.enabled = true;
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
        text.text = $"아직 이용 할 수 없습니다. 남은시간 :{(int)(coolTime - textTime)}";

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
