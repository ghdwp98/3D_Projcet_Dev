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
			if (items.Count < 3)
			{
				items.Add(str);
				invenUI.PrintNameText();
			}
		}
	}

	// ������ ���
	public void RemoveInven(string str)
	{
		if (FindInven(str))
		{
			items.Remove(str);
			invenUI.PrintNameText();
			invenUI.PrintNPCText(str);
		}
	}

	public bool FindInven(string str)
	{
		if (items.Contains(str))
		{
			return true;
		}
		return false;
	}

	// �κ��丮 ���� ��� ������ ����
	public void ClearInven()
	{
		items.Clear();
		foreach (var i in items)
		{
			invenUI.PrintNameText();
		}
		invenUI.PrintNameText();
	}

	public int GetInvenCount()
	{
		return items.Count;	
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
