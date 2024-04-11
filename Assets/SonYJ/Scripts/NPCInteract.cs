using UnityEngine;

public class NPCInteract : MonoBehaviour
{
	// ����� �ʿ��� NPC
	[SerializeField] Item item;
	[SerializeField] InvenUI invenUI;
	bool PlayerCheck;

	private void Start()
	{

	}
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.X))
		{
			CheckItem();
		}
	}

	public void CheckItem()
	{
		if (Manager.Inven.FindInven(item.name) && PlayerCheck)
		{
			Manager.Inven.RemoveInven(item.name);
			Debug.Log("NPC�� ����� �ް� �⻵�մϴ�.");
		}
		else
		{
			Debug.Log("�������� �����ϴ�.");
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.layer == 10) // Player
		{
			PlayerCheck = true;
			Debug.Log(PlayerCheck);
		}
	}

	private void OnCollisionExit(Collision collision)
	{
		if (collision.gameObject.layer == 10)
		{
			PlayerCheck = false;
		}
	}
}
