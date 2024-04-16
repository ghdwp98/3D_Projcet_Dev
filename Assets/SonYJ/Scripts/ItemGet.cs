using JJH;
using UnityEngine;

public class ItemGet : MonoBehaviour
{
	[SerializeField] PlayerHp playerhpmp;

	public void Get()
	{
		Manager.Inven.AddInven(gameObject.name);
		if(gameObject.name == "DoorKey")
			gameObject.SetActive(false);
	}

	public void GetHP()
	{
		playerhpmp.HP += playerhpmp.HPRecovery;
		Destroy(gameObject);
	}
	public void LoseHP()
	{
		playerhpmp.TakeDamage(10);
		Destroy(gameObject);
	}

}
