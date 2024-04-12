using UnityEngine;

public class NPCInteract : MonoBehaviour
{
	[SerializeField] Item item;

	public void CheckItemApple() // ����� �ʿ��� NPC
	{
		if (Manager.Inven.FindInven(item.name))
		{
			Manager.Inven.RemoveInven(item.name);
			Manager.Inven.invenUI.PrintNPCText("NPC�� " + item.name + "�� �ް� �⻵�մϴ�.");
		}
		else
		{
			Manager.Inven.invenUI.PrintNPCText(item.name + "�� ���°� ��������? �ٽ� Ȯ�����ּ���.");
		}
	}

	public void CheckItemKey() // DoorKey�� �ʿ��� NPC
	{
		if (Manager.Inven.FindInven(item.name))
		{
			Manager.Inven.RemoveInven(item.name);
			string str = "���� " + item.name + "��(��) ����߽��ϴ�.";
			Manager.Inven.invenUI.PrintNPCText(str);
			Destroy(gameObject);
		}
		else
		{
			Manager.Inven.invenUI.PrintNPCText("���� ����ִ�. ���谡 �ʿ��غ��δ�.");
		}
	}
}
