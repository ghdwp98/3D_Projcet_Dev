using JJH;
using UnityEngine;

public class ItemGet : MonoBehaviour
{
	[SerializeField] PlayerHp playerhpmp;

	public void Get()
	{
		Manager.Inven.AddInven(gameObject.name);
	}

	public void GetHP()
	{
		Debug.Log("1");
		Debug.Log(playerhpmp.HP);
		playerhpmp.HP += playerhpmp.HPRecovery;

	}
}
