using UnityEngine;

public class ItemGet : MonoBehaviour
{
	public void Get()
	{
		Manager.Inven.AddInven(gameObject.name);
	}
}
