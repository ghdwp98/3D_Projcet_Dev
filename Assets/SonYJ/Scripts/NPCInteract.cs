using UnityEngine;

public class NPCInteract : MonoBehaviour
{
	// 사과가 필요한 NPC
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
			Debug.Log("NPC가 사과를 받고 기뻐합니다.");
		}
		else
		{
			Debug.Log("아이템이 없습니다.");
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
