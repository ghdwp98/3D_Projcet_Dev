using System.Dynamic;
using UnityEngine;
using UnityEngine.Events;

public class InteractAdapter : MonoBehaviour, IInteractable, IGetable
{
	public UnityEvent<PlayerController> OnInteracted;
	public UnityEvent<PlayerController> OnGeted;
	public UnityEvent<PlayerController> OnUsed;

	public void Interact(PlayerController player)
	{
		OnInteracted?.Invoke(player);
	}
	public void Get(PlayerController player)
	{
		OnGeted?.Invoke(player);
	}

	public void Use(PlayerController player)
	{
		OnUsed?.Invoke(player);
	}
}
