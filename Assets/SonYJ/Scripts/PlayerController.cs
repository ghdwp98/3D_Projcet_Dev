using JJH;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	[Header("Component")]
	[SerializeField] CharacterController controller;

	[Header("Spec")]
	[SerializeField] float moveSpeed;
	[SerializeField] float jumpSpeed; // ���� Y �ӵ�
	[SerializeField] float ySpeed; // ���� �̵� Y �ӵ�
	[SerializeField] bool groundChecker; // ���� �پ��ִ��� Ȯ��
	Vector3 moveDir;
	Vector3 selfDir;

	[SerializeField] PlayerHp playerhpmp;
	GameObject nearObject;

	[Header("Interact")]
	[SerializeField] Collider[] colliders = new Collider[20];
	[SerializeField] float range;

	private void Start()
	{
		
	}

	private void Update()
	{
		Move();
		Fall();

	}

	private void OnMove(InputValue value)
	{
		Vector3 inputDir = value.Get<Vector2>();
		string temp = Manager.Scene.GetCurSceneName();
		Debug.Log(temp);
		if(temp == "1MapJaehoon")
		{
			moveDir.x = (-1) * inputDir.x;
			moveDir.z = (-1) * inputDir.y;
		}
		else if(temp == "3M")
		{
			moveDir.x = (-1) * inputDir.y;
			moveDir.z = inputDir.x;
		}
	}

	private void Move()
	{
		/*Vector3 targetPos = transform.position + moveDir;
		Vector3 framePos = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
		Vector3 frameDir = framePos - transform.position;*/
		if (moveDir != Vector3.zero)
			transform.rotation = Quaternion.LookRotation(moveDir, Vector3.up); // ȸ��
		// controller.Move(transform.TransformDirection(moveDir.normalized) * moveSpeed * Time.deltaTime); // �̵�

		/*float x = Input.GetAxisRaw("Horizontal");
		float z = Input.GetAxisRaw("Vertical");
		moveDir = new Vector3(x, 0, z);
		//controller.Move(moveDir * moveSpeed * Time.deltaTime);
		// transform.Translate(moveDir * moveSpeed * Time.deltaTime);
		// transform.position += moveDir * moveSpeed * Time.deltaTime;
		controller.Move(transform.forward * z * moveSpeed * Time.deltaTime);*/

		/*var dir = Vector3.forward;
		controller.Move(transform.position + transform.TransformDirection(-dir) * (moveSpeed * Time.deltaTime));*/

		controller.Move(moveDir * moveSpeed * Time.deltaTime);
	}

	private void OnJump(InputValue value)
	{
		if (groundChecker) // ���� ��ư ������(OnJump), ��Ʈ�ѷ� isGrounded�� true�� ��
			Jump();
	}

	private void Jump()
	{
		// ���� ��ư ������ controller.isGrounded�� true�� ��
		// �߷°����� ��� - �Ǵ� ySpeed ���� ���ϴ� jumpSpeed�� ����
		ySpeed = jumpSpeed;
		groundChecker = false;
	} // c

	private void Fall()
	{
		// ySpeed : Character Controller�� ���� �߷��� Time.deltaTime�� �ð��� ���� ������ ��� ���� ���� ���� �߷� y ���� ���ؼ� ������� �ӵ� ����
		ySpeed += Physics.gravity.y * Time.deltaTime;
		if (/*controller.isGrounded*/ groundChecker && ySpeed < 0)
			ySpeed = 0;
		controller.Move(Vector3.up * ySpeed * Time.deltaTime);
	}

	private void OnDown(InputValue value)
	{
		Down();
		if (Input.GetKeyUp(KeyCode.Z))
			UnDown();
	}

	private void Down()
	{
		controller.radius = 0.3f;
		controller.height = 0.5f;
	} // z

	private void UnDown()
	{
		controller.radius = 0.6f;
		controller.height = 1f;
	}

	private void OnInteraction(InputValue value) // x
	{
		Interaction();
	}

	private void Interaction()
	{
		// ������ ȹ��
		int sizeGet = Physics.OverlapSphereNonAlloc(transform.position, range, colliders);

		for(int i = 0; i < sizeGet; i++)
		{
			IInteractable target = colliders[i].GetComponent<IInteractable>();

			target?.Interact(this);
		}

		/*
		for (int i = 0; i < sizeGet; i++)
		{
			IGetable getable = collidersGet[i].GetComponent<IGetable>();
			//Item curItem = gameObject.GetComponent<Item>();
			string str = collidersGet[i].name;
			if (getable != null)
			{
				getable.Get(this);
				Manager.Inven.AddInven(str);
				break;
			}
		}

		// NPC �ܼ� ��ȭ
		int sizeInter = Physics.OverlapSphereNonAlloc(transform.position, range, collidersInter); // �÷��̾� ��ġ���� ������ŭ, �浹ü���� ��ȯ�ؼ� ��ȣ�ۿ�
		for (int i = 0; i < sizeInter; i++)
		{
			IInteractable interactable = collidersInter[i].GetComponent<IInteractable>();
			if (interactable != null)
			{
				interactable.Interact(this);
				break;
			}
		}

		if(nearObject != null)
		{
			playerhpmp.HP += playerhpmp.HPRecovery;
			Destroy(nearObject.gameObject);
		}
		*/
		// NPC, ������Ʈ�� ȹ�� ������ ����

	}

	private void OnControllerColliderHit(ControllerColliderHit hit)
	{
		if (hit.collider.gameObject.layer == 6) // ���� ����� �� groundChecker true, �ƴҶ� false
			groundChecker = true;
		else
			groundChecker = false;

		if (hit.collider.gameObject.layer == 31) // Damage
		{
			playerhpmp.TakeDamage(5);
			gameObject.layer = 7; // DamageMusi
			Invoke("OnDamageLayer", 1f);
			Destroy(hit.gameObject);

			if (playerhpmp.HP <= 0)
				transform.position = CheckPoint.GetActiveCheckPointPosition();
			if (playerhpmp.HP >= 100)
				playerhpmp.HP = 100;
		}

		if (hit.collider.gameObject.layer == 8) // Recovery
		{
			nearObject = hit.gameObject;
			Debug.Log(nearObject.name);
		}

		/*if(hit.collider.gameObject.layer == 9) // Item
		{
			inventory.name = hit.collider.gameObject.name;
			inventory.count = hit.collider.gameObject.GetComponent<Inventory>().count++;
			Debug.Log(inventory.name);
			Debug.Log(inventory.count);
			inventory.SetItem();
		}*/
	}

	void OnDamageLayer()
	{
		gameObject.layer = 0; // Default
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, range);
	}
}
