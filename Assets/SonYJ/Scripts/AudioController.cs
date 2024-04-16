using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
	[SerializeField] AudioClip bgmClip1;
	[SerializeField] AudioClip bgmClip2;

	private void Start()
	{
		Manager.Sound.PlayBGM(bgmClip1);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Fire"))
		{
			Manager.Sound.StopBGM();
			Manager.Sound.PlayBGM(bgmClip2);
		}

		if (other.gameObject.CompareTag("Save"))
		{
			Manager.Sound.StopBGM();
			Manager.Sound.PlayBGM(bgmClip1);
		}
	}
}
