using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Next2ch : BaseScene
{
	public override IEnumerator LoadingRoutine()
	{
		yield return null;
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.CompareTag("Player"))
		{
			Manager.Scene.LoadScene("2M");
		}
	}


}
