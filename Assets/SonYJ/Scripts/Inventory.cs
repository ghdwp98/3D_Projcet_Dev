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
			items.Add(str);
			invenUI.PrintExplainText(str + "을(를) 인벤토리에 추가했습니다.");
			invenUI.PrintNameText();
		}
		else
		{
			invenUI.PrintExplainText(str + "은/는 이미 인벤토리에 존재합니다.");
		}
	}

	// 아이템 사용
	public void RemoveInven(string str)
	{
		if (FindInven(str))
		{
			items.Remove(str);
			invenUI.PrintExplainText(str + "을(를) 인벤토리에서 제거했습니다.");
			invenUI.PrintNameText();
			invenUI.PrintNPCText(str);
		}
	}

	public bool FindInven(string str)
	{
		if (items.Contains(str))
		{
			invenUI.PrintExplainText(str + "이(가) 인벤토리에 존재합니다");
			return true;
		}
		invenUI.PrintExplainText("인벤토리에 해당 아이템이 없습니다.");
		return false;
	}

	// 인벤토리 내의 모든 아이템 제거
	public void ClearInven()
	{
		items.Clear();
		invenUI.PrintNameText();
		invenUI.PrintExplainText("인벤토리를 비웠습니다.");
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
