using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Progress;

public class Player : MonoBehaviour
{
	public float speed;

	public GameObject[] weapons;
	public bool[] hasWeapons;

	float hp;

	public int key; // ���� �ִ� ���� ��
	public int maxKey; // �ִ� ���� ���� ��

	float hAxis;
	float vAxis;

	bool jDown; // Jump
	bool iDown; // Interaction
	bool dDown; // Down

	bool isJump; // ����

	GameObject nearObject;

	Vector3 moveVec;
	Rigidbody rigid;

	private void Awake()
	{
		rigid = GetComponent<Rigidbody>();
	}

	private void Start()
	{

	}

	private void Update()
	{
		GetInput();
		Move();
		Turn();
		Jump();
		Down();
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
		if (iDown && nearObject != null && !isJump)
		{
			if (nearObject.tag == "Weapon")
			{
				Item item = nearObject.GetComponent<Item>();
				int weaponIndex = item.value;
				hasWeapons[weaponIndex] = true;

				Destroy(nearObject);
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

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Floor")
			isJump = false;
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.tag == "Weapon")
			nearObject = other.gameObject;
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "Weapon")
			nearObject = null;
	}
}
