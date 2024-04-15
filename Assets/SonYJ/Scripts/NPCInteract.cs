using UnityEngine;

public class NPCInteract : MonoBehaviour
{
	[SerializeField] Item item;

	public void CheckItemApple() // 사과가 필요한 NPC
	{
		if (Manager.Inven.FindInven(item.name))
		{
			Manager.Inven.RemoveInven(item.name);
			Manager.Inven.invenUI.PrintNPCText("NPC가 " + item.name + "을 받고 기뻐합니다.");
		}
		else
		{
			Manager.Inven.invenUI.PrintNPCText(item.name + "가 없는거 같은데요? 다시 확인해주세요.");
		}
	}

	public void CheckItemKey() // DoorKey가 필요한 NPC
	{
		if (Manager.Inven.FindInven(item.name))
		{
			Manager.Inven.RemoveInven(item.name);
			string str = "문에 " + item.name + "을(를) 사용했습니다.";
			Manager.Inven.invenUI.PrintNPCText(str);
			Destroy(gameObject);
		}
		else
		{
			Manager.Inven.invenUI.PrintNPCText("문이 잠겨있다. 열쇠가 필요해보인다.");
		}
	}

	public void CheckItemFood() // 재료가 필요한 화구
	{
		if (Manager.Inven.GetInvenCount() != 0)
		{
			if (Manager.Inven.FindInven("Vegetable-a") && Manager.Inven.FindInven("Vegetable-c"))
			{
				Manager.Inven.RemoveInven("Vegetable-a");
				Manager.Inven.RemoveInven("Vegetable-c");
				string str = "화구에 Vegetable-a 과 Vegetable-c를 사용했습니다.";
				Manager.Inven.invenUI.PrintNPCText(str);
			}
			else
			{
				string str = "필요한 재료가 다 모이지 않았습니다! Vegetable-a과 Vegetable-c가 필요합니다!" +
					"인벤토리가 다 찼다면 Q버튼을 눌러서 인벤토리를 비우세요!";
				Manager.Inven.invenUI.PrintNPCText(str);
			}
		}
	}
}
