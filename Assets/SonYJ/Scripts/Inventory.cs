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

	// 아이템 추가
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

	// 아이템 사용
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

	// 인벤토리 내의 모든 아이템 제거
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

	// 인벤토리 내의 모든 아이템 출력
	public void PrintInven()
	{
		Debug.Log("인벤토리 아이템 목록:");
		foreach (var i in items)
		{
			Debug.Log("- " + i);
		}
	}

}
