using System.Collections;
using TMPro;
using UnityEngine;

public class LimitHidingObjectParent : MonoBehaviour
{
    BoxCollider boxCollider; //�ݶ��̴� -->��ȣ�ۿ�Ǹ� �������ְ� trigger ����� �ٽ� ����. 
    Material material;
    GameObject playerObj;

    [SerializeField] LayerMask playerLayer;
    [SerializeField] float coolTime;
    float sumTime;
    float textTime;
    bool runCoro;
    [SerializeField] Transform returnPos;
    [SerializeField] TextMeshProUGUI text;

    public float count; // �ѾƳ��� 
    public bool available; // �ٽ� �̿��� �� ���� ��� ������ �ð�. 
    bool isHiding;

    // �Ѱܳ��� �����ð� �̿�Ұ� --> ���������� text����ֱ� (�̿�Ұ� �ؽ�Ʈ )


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

        if (isHiding == true) //�����ִ� ���¶�� 
        {
            count += Time.deltaTime;
            if (count > 5f)
            {
                CharacterController characterController = playerObj.GetComponent<CharacterController>();
                characterController.enabled = false;

                //�ڽ� ������Ʈ�� ��ġ�� �Ѱܳ��� �ϱ�. 
                playerObj.transform.position = returnPos.transform.position;

                characterController.enabled = true;
                isHiding = false;
                available = false; //�Ѱܳ��� �����ð� �̿����� ���ϵ��� �ϱ�. 
                count = 0; //ī��Ʈ �ʱ�ȭ . 
            }
        }

        if (available == false) // �ð� ���� �� false�� �ٲ��ֱ�. 
        {
            sumTime += Time.deltaTime;  //��Ÿ�Ӱ� ���� ��������.
            textTime += Time.deltaTime; // �ڷ�ƾ�� ����� ��������. 

            if (sumTime > coolTime) //���ص� ��Ÿ���� ������ �ٽ� �̿밡���ϵ���. 
            {
                available = true;
                sumTime = 0;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.CompareTag("Player") && Input.GetKeyDown(KeyCode.X)
            && available == true) //comparetag�� �ξ� ��������. 
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
                isHiding = true;


            }
        }

        else if (other.gameObject.CompareTag("Player") && Input.GetKeyDown(KeyCode.X)
            && available == false) //�̿�Ұ����ϴٴ� ������ ��������. 
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
        if (other.gameObject.CompareTag("Player")) //comparetag�� �ξ� ��������.
        {
            boxCollider.enabled = true;
            material.color = new Color32(255, 255, 255, 255);
            other.gameObject.layer = 9; //�ٽ� �÷��̾��� ���̾�� ����. 
            isHiding = false; // �ٽ� �ȼ��� ���·� 
            count = 0; //ī��Ʈ �ʱ�ȭ (���½ð� �ʱ�ȭ)
        }
    }

    IEnumerator TextOnCoroutine(float textTime)
    {
        runCoro = true;
        text.enabled = true;
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
        text.text = $"���� �̿� �� �� �����ϴ�. �����ð� :{(int)(coolTime - textTime)}";

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
