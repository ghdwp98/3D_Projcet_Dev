using System.Collections;
using TMPro;
using UnityEngine;

public class NPCTalk : MonoBehaviour
{
	// 단순 대화 출력 NPC
	public void Talk()
	{
		Debug.Log("Talk");
		StartCoroutine(TextBlink());
	}
	IEnumerator TextBlink()
	{
		Debug.Log(Manager.Inven.name);
		Manager.Inven.invenUI.PrintNPCText("안녕?");
		yield return new WaitForSeconds(3f);
		Manager.Inven.invenUI.PrintNPCText("안녕하세용가리");
		yield return new WaitForSeconds(0.13f);
		Manager.Inven.invenUI.PrintNPCText("");
		yield return new WaitForSeconds(0.09f);
		Manager.Inven.invenUI.PrintNPCText("안녕하세용가리");
		yield return new WaitForSeconds(0.11f);
		Manager.Inven.invenUI.PrintNPCText("");
		yield return new WaitForSeconds(0.08f);
		Manager.Inven.invenUI.PrintNPCText("안녕하세용가리");
		yield return new WaitForSeconds(0.12f);
		Manager.Inven.invenUI.PrintNPCText("");
		yield return new WaitForSeconds(0.1f);
		Manager.Inven.invenUI.PrintNPCText("안녕?");
		yield return new WaitForSeconds(3f);
		Manager.Inven.invenUI.PrintNPCText("");
	}
}
