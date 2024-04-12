using UnityEngine;

public class NPCInteract : MonoBehaviour
{
	// ����� �ʿ��� NPC
	[SerializeField] Item item;

	public void CheckItem()
	{
		if (Manager.Inven.FindInven(item.name))
		{
			Debug.Log(item.name);
			Manager.Inven.RemoveInven(item.name);
			Manager.Inven.invenUI.PrintNPCText("NPC�� " + item.name + "�� �ް� �⻵�մϴ�.");
			//Debug.Log("NPC�� ����� �ް� �⻵�մϴ�.");
		}
		else
		{
			Manager.Inven.invenUI.PrintNPCText(item.name + "�� ���°� ��������? �ٽ� Ȯ�����ּ���.");
			//Debug.Log("�������� �����ϴ�.");
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
