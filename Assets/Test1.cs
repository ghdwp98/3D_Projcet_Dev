using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Test1 : BaseScene
{
	public override IEnumerator LoadingRoutine()
	{
		yield return null;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
			Manager.Scene.LoadScene("3Map2");
		// ㅠㅠ 수정필요함..
	}


}
