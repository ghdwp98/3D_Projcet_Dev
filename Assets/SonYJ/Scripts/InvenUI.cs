using TMPro;
using UnityEngine;

public class InvenUI : MonoBehaviour
{
	[SerializeField] TextMeshProUGUI[] itemText;
	[SerializeField] TextMeshProUGUI explainText;
	[SerializeField] TextMeshProUGUI NPCText;
	int count = 0;
	int i = 0;

	public void PrintNameText()
	{
		count = Manager.Inven.items.Count;
		Debug.Log(count);
		i = 0;

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
}
