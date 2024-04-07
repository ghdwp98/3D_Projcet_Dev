using System.Collections;
using TMPro;
using UnityEngine;

public class NPC : MonoBehaviour
{
	[SerializeField] TextMeshProUGUI text1;
	public void Talk()
	{
		Debug.Log("??");
		StartCoroutine(TextBlink());

	}
	IEnumerator TextBlink()
	{
		text1.gameObject.SetActive(true);
		yield return new WaitForSeconds(5f);
		text1.text = "123new Text";
		text1.gameObject.SetActive(false);
		yield return new WaitForSeconds(0.1f);
		text1.gameObject.SetActive(true);
		yield return new WaitForSeconds(0.07f);
		text1.gameObject.SetActive(false);
		yield return new WaitForSeconds(0.05f);
		text1.gameObject.SetActive(true);
		yield return new WaitForSeconds(0.09f);
		text1.gameObject.SetActive(false);
		yield return new WaitForSeconds(0.1f);
		text1.text = "New Text";
		text1.gameObject.SetActive(true);

	}
}
