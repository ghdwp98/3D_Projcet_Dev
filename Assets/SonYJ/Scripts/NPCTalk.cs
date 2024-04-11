using System.Collections;
using TMPro;
using UnityEngine;

public class NPCTalk : MonoBehaviour
{
	// �ܼ� ��ȭ ��� NPC
	public void Talk()
	{
		Debug.Log("Talk");
		StartCoroutine(TextBlink());
	}
	IEnumerator TextBlink()
	{
		Debug.Log(Manager.Inven.name);
		Manager.Inven.invenUI.PrintNPCText("�ȳ�?");
		yield return new WaitForSeconds(3f);
		Manager.Inven.invenUI.PrintNPCText("�ȳ��ϼ��밡��");
		yield return new WaitForSeconds(0.13f);
		Manager.Inven.invenUI.PrintNPCText("");
		yield return new WaitForSeconds(0.09f);
		Manager.Inven.invenUI.PrintNPCText("�ȳ��ϼ��밡��");
		yield return new WaitForSeconds(0.11f);
		Manager.Inven.invenUI.PrintNPCText("");
		yield return new WaitForSeconds(0.08f);
		Manager.Inven.invenUI.PrintNPCText("�ȳ��ϼ��밡��");
		yield return new WaitForSeconds(0.12f);
		Manager.Inven.invenUI.PrintNPCText("");
		yield return new WaitForSeconds(0.1f);
		Manager.Inven.invenUI.PrintNPCText("�ȳ�?");
		yield return new WaitForSeconds(3f);
		Manager.Inven.invenUI.PrintNPCText("");
	}
}
