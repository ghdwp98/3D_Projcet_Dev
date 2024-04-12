using System.Collections;
using TMPro;
using Unity.Collections;
using UnityEngine;

public class NPCTalk : MonoBehaviour
{
	string str1 = "12";
	string str2 = "34";

	// 단순 대화 출력 NPC
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
			str1 = "안녕?";
			str2 = "안녕하세용가리";
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
			str1 = "화분 아래에는 열쇠가 없는 것 같다. 다른 곳을 찾아보자.";
		if (gameObject.name == "Box(1)")
			str1 = "이 상자에는 아무것도 없다";
		if (gameObject.name == "Box(2)")
			str1 = "히든 스토리(1)";
		if (gameObject.name == "Box(3)")
			str1 = "히든 스토리(2)";
		if (gameObject.name == "Box(4)")
			str1 = "열쇠를 찾았다!";

		Manager.Inven.invenUI.PrintNPCText(str1);

		if (gameObject.name == "Box(4)")
			gameObject.SetActive(false);

		yield return new WaitForSeconds(5f);
		Manager.Inven.invenUI.PrintNPCText("");
	}


}
