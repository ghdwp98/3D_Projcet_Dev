using UnityEngine;

public class NPCInteract : MonoBehaviour
{
	// 사과가 필요한 NPC
	[SerializeField] Item item;

	public void CheckItem()
	{
		if (Manager.Inven.FindInven(item.name))
		{
			Debug.Log(item.name);
			Manager.Inven.RemoveInven(item.name);
			Manager.Inven.invenUI.PrintNPCText("NPC가 " + item.name + "을 받고 기뻐합니다.");
			//Debug.Log("NPC가 사과를 받고 기뻐합니다.");
		}
		else
		{
			Manager.Inven.invenUI.PrintNPCText(item.name + "가 없는거 같은데요? 다시 확인해주세요.");
			//Debug.Log("아이템이 없습니다.");
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.layer == 10) // Player
		{
		}
	}

	private void OnCollisionExit(Collision collision)
	{
		if (collision.gameObject.layer == 10)
		{

		}
	}
}
