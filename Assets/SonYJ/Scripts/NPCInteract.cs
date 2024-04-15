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

	public void CheckItemFood() // ��ᰡ �ʿ��� ȭ��
	{
		if (Manager.Inven.GetInvenCount() != 0)
		{
			if (Manager.Inven.FindInven("Vegetable-a") && Manager.Inven.FindInven("Vegetable-c"))
			{
				Manager.Inven.RemoveInven("Vegetable-a");
				Manager.Inven.RemoveInven("Vegetable-c");
				string str = "ȭ���� Vegetable-a �� Vegetable-c�� ����߽��ϴ�.";
				Manager.Inven.invenUI.PrintNPCText(str);
			}
			else
			{
				string str = "�ʿ��� ��ᰡ �� ������ �ʾҽ��ϴ�! Vegetable-a�� Vegetable-c�� �ʿ��մϴ�!" +
					"�κ��丮�� �� á�ٸ� Q��ư�� ������ �κ��丮�� ��켼��!";
				Manager.Inven.invenUI.PrintNPCText(str);
			}
		}
	}
}
