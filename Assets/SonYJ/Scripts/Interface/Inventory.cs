using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
	/*public List<Inventory> items;

    public string name;
    public int count = 0;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI countText;

	public void SetItem()
	{
		nameText.text = name;
		countText.text = count.ToString();
	}

	public void GetItem()
	{
		Debug.Log(name);
		Debug.Log(count);
		countText.text = (count++).ToString();
		SetItem();
	}*/

	public List<Item> items;

	[SerializeField] Transform slotParent;
	[SerializeField] Slot[] slots;

#if UNITY_EDITOR
	private void OnValidate()
	{
		slots = slotParent.GetComponentsInChildren<Slot>();
	}
#endif

	void Awake()
	{
		FreshSlot();
	}

	public void FreshSlot()
	{
		int i = 0;
		for (; i < items.Count && i < slots.Length; i++)
		{
			slots[i].item = items[i];
		}
		for (; i < slots.Length; i++)
		{
			slots[i].item = null;
		}
	}

	public void AddItem(Item _item)
	{
		if (items.Count < slots.Length)
		{
			items.Add(_item);
			FreshSlot();
		}
		else
		{
			print("½½·ÔÀÌ °¡µæ Â÷ ÀÖ½À´Ï´Ù.");
		}
	}
}
