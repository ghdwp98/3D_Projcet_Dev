using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ch1ObjectDialog : MonoBehaviour
{
	[SerializeField] int count;//자신의 카운트를 통해 대화로그를 불러옴. 
	[SerializeField] ch1Dialog ch1Dialog;
	BoxCollider boxCollider;

	void Start()
	{
		ch1Dialog = GameObject.FindWithTag("Dialog").GetComponent<ch1Dialog>();
	}


	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			if (ch1Dialog != null){
				Debug.Log("11");
				ch1Dialog.StartTextCoroutine(count);
			}
			boxCollider = GetComponent<BoxCollider>();
			Destroy(boxCollider);
		}
	}
}

