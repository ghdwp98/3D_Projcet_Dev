using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IVItem : MonoBehaviour
{
    public string itemName;
    public int itemCount;

	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.layer == 6) // Player
		{

		}
	}
}
