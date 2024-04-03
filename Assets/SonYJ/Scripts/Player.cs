using JJH;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Progress;

public class Player : MonoBehaviour
{
	public float speed;

	public GameObject[] weapons;
	public bool[] hasWeapons;
	[SerializeField] PlayerHp playerhpmp;

	public int key; // 갖고 있는 열쇠 수
	public int maxKey; // 최대 열쇠 소지 수

	float hAxis;
	float vAxis;

	bool jDown; // Jump
	bool iDown; // Interaction
	bool dDown; // Down

	bool isJump; // 점프

	GameObject nearObject;

	Vector3 moveVec;
	Rigidbody rigid;

	private void Awake()
	{
		rigid = GetComponent<Rigidbody>();
	}

	private void Start()
	{
		playerhpmp.HP = 50;
		Debug.Log(playerhpmp.HP);
	}

	private void Update()
	{
		GetInput();
		Move();
		Turn();
		Jump();
		Down();
		Interaction();

		if (Input.GetKey(KeyCode.LeftShift))
		{
			playerhpmp.RunStaminaConsume(0.5f);
		}
	}

	void GetInput()
	{
		hAxis = Input.GetAxisRaw("Horizontal");
		vAxis = Input.GetAxisRaw("Vertical");
		jDown = Input.GetButtonDown("Jump");
		iDown = Input.GetButtonDown("Interaction");
		dDown = Input.GetButton("Down");
	}

	void Move()
	{
		moveVec = new Vector3(hAxis, 0, vAxis).normalized;

		transform.position += moveVec * speed * Time.deltaTime;
	}

	void Turn()
	{
		transform.LookAt(transform.position + moveVec);
	}

	void Jump()
	{
		if (jDown && !isJump)
		{
			rigid.AddForce(Vector3.up * 15, ForceMode.Impulse);
			isJump = true;
		}
	}

	void Interaction()
	{
		if (iDown && nearObject != null)
		{
			Debug.Log("IDOWN KEY");
			if (nearObject.tag == "RecoveryItem")
			{
				playerhpmp.HP += playerhpmp.HPRecovery;
				Debug.Log(playerhpmp.HP);
				Destroy(nearObject.gameObject);
			}
			if (nearObject.tag == "DemageItem")
			{
				playerhpmp.TakeDamage(3);
				Debug.Log(playerhpmp.HP);
				Destroy(nearObject.gameObject);

				if(playerhpmp.HP <= 0)
				{
					transform.position = CheckPoint.GetActiveCheckPointPosition();
				}
			}
		}
	}

	void Down()
	{
		BoxCollider collider = GetComponent<BoxCollider>();
		if (dDown)
		{
			collider.center = new Vector3(0, -0.1f, 0);
			collider.size = new Vector3(0.8f, 0.8f, 0.8f);
		}
		else
		{
			collider.center = new Vector3(0, 0, 0);
			collider.size = new Vector3(1, 1, 1);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Item")
		{
			Item item = other.GetComponent<Item>();
			switch (item.type)
			{
				case Item.Type.Key:
					key += item.value;
					if (key > maxKey)
						key = maxKey;
					break;
			}
			Destroy(other.gameObject);
		}
	}

	void OnDamegeLayer()
	{
		gameObject.layer = 0;
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Floor")
			isJump = false;

		if(collision.gameObject.layer == 31)
		{
			Debug.Log("충돌진입");
			gameObject.layer = 6;
			PlayerHp.Player_Action?.Invoke(10);
			Invoke("OnDamegeLayer", 1f);
			Debug.Log(playerhpmp.HP);
			Destroy(collision.gameObject);
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.tag == "DemageItem" || other.tag == "RecoveryItem")
			nearObject = other.gameObject;
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "DemageItem" || other.tag == "RecoveryItem")
			nearObject = null;
	}
}
