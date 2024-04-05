using System.Collections;
using UnityEngine;

public class ReturnPlayerStartPos : MonoBehaviour
{
    [SerializeField] GameObject startPos;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(ReturnCoroutine(other));

        }
    }

    IEnumerator ReturnCoroutine(Collider other)
    {
        CharacterController controller = other.GetComponent<CharacterController>();
        Rigidbody rigid = other.gameObject.GetComponent<Rigidbody>();
        rigid.isKinematic = false;
        controller.enabled = false;
        other.transform.position = startPos.transform.position + (Vector3.up * 4);
        yield return new WaitForSeconds(1f); // �̷� ��� ���ߴ� �κе��� �׳� �뷯�� ������ �����ָ� ��. 
        controller.enabled = true;
        rigid.isKinematic = true;

    }


}
