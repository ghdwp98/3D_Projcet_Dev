using System.Collections;
using TMPro;
using Unity.Collections;
using UnityEngine;

public class NPCTalk : MonoBehaviour
{
	string str1 = "12";
	string str2 = "34";

	// �ܼ� ��ȭ ��� NPC
	public void Talk()
	{
		if (gameObject.name == "TalkNPC")
		{
			StartCoroutine(TextBlink());
		}
		if (gameObject.name == "FlowerPot" || gameObject.name == "Box(1)" || gameObject.name == "Box(2)" || gameObject.name == "Box(3)" || gameObject.name == "Box(4)")
		{
			StartCoroutine(Text());
		}
	}
	IEnumerator TextBlink()
	{
		if (gameObject.name == "TalkNPC")
		{
			str1 = "�ȳ�?";
			str2 = "�ȳ��ϼ��밡��";
		}
		Manager.Inven.invenUI.PrintNPCText(str1);
		yield return new WaitForSeconds(3f);
		Manager.Inven.invenUI.PrintNPCText(str2);
		yield return new WaitForSeconds(0.13f);
		Manager.Inven.invenUI.PrintNPCText("");
		yield return new WaitForSeconds(0.09f);
		Manager.Inven.invenUI.PrintNPCText(str2);
		yield return new WaitForSeconds(0.11f);
		Manager.Inven.invenUI.PrintNPCText("");
		yield return new WaitForSeconds(0.08f);
		Manager.Inven.invenUI.PrintNPCText(str2);
		yield return new WaitForSeconds(0.12f);
		Manager.Inven.invenUI.PrintNPCText("");
		yield return new WaitForSeconds(0.1f);
		Manager.Inven.invenUI.PrintNPCText(str1);
		yield return new WaitForSeconds(3f);
		Manager.Inven.invenUI.PrintNPCText("");
	}

	IEnumerator Text()
	{
		if (gameObject.name == "FlowerPot")
			str1 = "ȭ�� �Ʒ����� ���谡 ���� �� ����. �ٸ� ���� ã�ƺ���.";
		if (gameObject.name == "Box(1)")
			str1 = "�� ���ڿ��� �ƹ��͵� ����";
		if (gameObject.name == "Box(2)")
			str1 = "���� ���丮(1)";
		if (gameObject.name == "Box(3)")
			str1 = "���� ���丮(2)";
		if (gameObject.name == "Box(4)")
			str1 = "���踦 ã�Ҵ�!";

		Manager.Inven.invenUI.PrintNPCText(str1);

		if (gameObject.name == "Box(4)")
			gameObject.SetActive(false);

		yield return new WaitForSeconds(5f);
		Manager.Inven.invenUI.PrintNPCText("");
	}


}
