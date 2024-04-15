using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;
using UnityEngine.Events;

public class InteractAdapter : MonoBehaviour, IInteractable
{
	public UnityEvent<PlayerController> OnInteracted;


    public void Interact(PlayerController player)
	{
		OnInteracted?.Invoke(player);
	}
}
