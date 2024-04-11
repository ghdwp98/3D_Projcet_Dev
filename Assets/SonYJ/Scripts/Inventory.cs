using System.Collections.Generic;
using UnityEngine;

public class Inventory : Singleton<Inventory>
{
	public InvenUI invenUI;
	public List<string> items = new List<string>();

	protected override void Awake()
	{
		base.Awake();
	}

	// ������ �߰�
	public void AddInven(string str)
	{
		if (!FindInven(str))
		{
			items.Add(str);
			invenUI.PrintExplainText(str + "��(��) �κ��丮�� �߰��߽��ϴ�.");
			invenUI.PrintNameText();
		}
		else
		{
			invenUI.PrintExplainText(str + "��/�� �̹� �κ��丮�� �����մϴ�.");
		}
	}

	// ������ ���
	public void RemoveInven(string str)
	{
		if (FindInven(str))
		{
			items.Remove(str);
			invenUI.PrintExplainText(str + "��(��) �κ��丮���� �����߽��ϴ�.");
			invenUI.PrintNameText();
			invenUI.PrintNPCText(str);
		}
	}

	public bool FindInven(string str)
	{
		if (items.Contains(str))
		{
			invenUI.PrintExplainText(str + "��(��) �κ��丮�� �����մϴ�");
			return true;
		}
		invenUI.PrintExplainText("�κ��丮�� �ش� �������� �����ϴ�.");
		return false;
	}

	// �κ��丮 ���� ��� ������ ����
	public void ClearInven()
	{
		items.Clear();
		invenUI.PrintNameText();
		invenUI.PrintExplainText("�κ��丮�� ������ϴ�.");
	}

	// �κ��丮 ���� ��� ������ ���
	public void PrintInven()
	{
		Debug.Log("�κ��丮 ������ ���:");
		foreach (var i in items)
		{
			Debug.Log("- " + i);
		}
	}

}
