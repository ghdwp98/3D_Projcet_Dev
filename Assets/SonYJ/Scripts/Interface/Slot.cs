using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
	/* public Item item; // 획득한 아이템
     public int itemCount; // 획득한 아이템의 개수
     public Image itemImage; // 아이템의 이미지

     [SerializeField] Text text_Count; // 아이템의 개수 
     [SerializeField] GameObject go_CountImage;

     public void AddItem(Item _item, int _count = 1)
     {
         // AddItem(item);
         item = _item;
         itemCount = _count;
         //itemImage.sprite = item.itemImage;

         text_Count.text = itemCount.ToString();

     }*//*
	[SerializeField] TextMeshProUGUI itemNameText;
	[SerializeField] TextMeshProUGUI itemCountText;

	public string itemName;
	public int itemCount;
	public int size = 5;

	public void AddItem()
	{
		SetItem();

		itemCount++;

		SetItem();
	}

	public void UseItem()
	{
		SetItem();

		itemName = name;
		itemCount--;

		SetItem();
	}

	private void SetItem()
	{
		itemNameText.text = itemName;
		itemCountText.text = itemCount.ToString();

	}*/

	[SerializeField] Image image;

	private Item _item;
	public Item item
	{
		get { return _item;  }
		set
		{
			_item = value;
			if (_item != null)
			{
				image.sprite = item.itemImage;
				image.color = new Color(1, 1, 1, 1);
			}
			else
				image.color = new Color(1, 1, 1, 0);
		}
	}
}
