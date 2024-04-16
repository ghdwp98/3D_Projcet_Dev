using TMPro;
using UnityEngine;

public class InvenUI : MonoBehaviour
{
	[SerializeField] TextMeshProUGUI[] itemText;
	[SerializeField] TextMeshProUGUI explainText;
	[SerializeField] TextMeshProUGUI NPCText;

	public void PrintNameText()
	{
		int count = Manager.Inven.items.Count;

		for (int i = 0; i < itemText.Length; i++)
		{
			if (Manager.Inven.items.Count > i)
			{
				itemText[i].text = Manager.Inven.items[i];
			}
			else
			{
				itemText[i].text = "";
			}
		}
	}

	public void PrintExplainText(string str)
	{
		explainText.text = str;
	}

	public void PrintNPCText(string str)
	{
		NPCText.text = str;
	}

	public void CleanText()
	{
		for (int i = 0; i < itemText.Length; i++)
		{
			itemText[i].text = "";
			explainText.text = "";
			NPCText.text = "";
		}
	}
}
