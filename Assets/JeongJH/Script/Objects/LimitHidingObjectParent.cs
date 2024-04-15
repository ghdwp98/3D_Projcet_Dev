using System.Collections;
using TMPro;
using UnityEngine;

public class LimitHidingObjectParent : MonoBehaviour
{
    BoxCollider boxCollider; //�ݶ��̴� -->��ȣ�ۿ�Ǹ� �������ְ� trigger ����� �ٽ� ����. 
    Material material;
    GameObject playerObj;

    [SerializeField] LayerMask playerLayer;
    float[] coolTime;
    float []sumTime;
    float[] textTime;
    bool runCoro;
    [SerializeField] Transform returnPos;
    [SerializeField] TextMeshProUGUI text;

    //�� ���� ������Ʈ���� �ڽ��� ��ȣ�� ����.
    [SerializeField] int id; 


    private float count; // �ѾƳ��� 
    bool[] available; // �ٽ� �̿��� �� ���� ��� ������ �ð�. 

    bool[] isHiding; //bool �迭�� ������ ������Ʈ���� ��Ȳ�� ���� 

    // �Ѱܳ��� �����ð� �̿�Ұ� --> ���������� text����ֱ� (�̿�Ұ� �ؽ�Ʈ )

    //�� �̰� .. �� ���� ������ �����ϴϱ� ���̳ʽ��� ���͹����� �׷��� .. 


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

        if (isHiding[id] == true) //�����ִ� ���¶�� 
        {
            count += Time.deltaTime;
            if (count > 9f) //9�� ���� 
            {
                CharacterController characterController = playerObj.GetComponent<CharacterController>();
                characterController.enabled = false;

                StartCoroutine(KickTextOn());
                //�ڽ� ������Ʈ�� ��ġ�� �Ѱܳ��� �ϱ�. 
                playerObj.transform.position = returnPos.transform.position;

                characterController.enabled = true;
                isHiding[id] = false;
                available[id] = false; //�Ѱܳ��� �����ð� �̿����� ���ϵ��� �ϱ�. 
                count = 0; //ī��Ʈ �ʱ�ȭ . 
            }
        }

        if (available[id] == false) // �ð� ���� �� false�� �ٲ��ֱ�. 
        {
            sumTime[id] += Time.deltaTime;  //��Ÿ�Ӱ� ���� ��������.

            textTime[id] += Time.deltaTime; // �ڷ�ƾ�� ����� ��������. 

            if (sumTime[id] > coolTime[id]) //���ص� ��Ÿ���� ������ �ٽ� �̿밡���ϵ���. 
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
            && available[id] == true) //comparetag�� �ξ� ��������. 
        {
            boxCollider.enabled = false;
            CharacterController characterController = other.gameObject.GetComponent<CharacterController>();
            if (characterController != null)
            {
                characterController.enabled = false;
                other.gameObject.transform.position = transform.position;
                characterController.enabled = true;
                material.color = new Color32(255, 255, 255, 150);
                other.gameObject.layer = 28; //hide ���̾�� �����غ���. 
                isHiding[id] = true;

            }
        }

        else if (other.gameObject.CompareTag("Player") && Input.GetKeyDown(KeyCode.X)
            && available[id] == false) //�̿�Ұ����ϴٴ� ������ ��������. 
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
        if (other.gameObject.CompareTag("Player")) //comparetag�� �ξ� ��������.
        {
            boxCollider.enabled = true;
            material.color = new Color32(255, 255, 255, 255);
            other.gameObject.layer = 10; //�ٽ� �÷��̾��� ���̾�� ����. 
            isHiding[id] = false; // �ٽ� �ȼ��� ���·� 
            count = 0; //ī��Ʈ �ʱ�ȭ (���½ð� �ʱ�ȭ)
        }
    }

    IEnumerator TextOnCoroutine(float textTime)
    {
        runCoro = true;
        text.enabled = true;
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
        text.text = $"���� �̿� �� �� �����ϴ�. �����ð� :{(int)(coolTime[id] - textTime)}";

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
        text.text = "�ʹ� ���� �����־���."; 

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
