using JJH;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	[Header("Component")]
	[SerializeField] CharacterController controller;

	[Header("Spec")]
	[SerializeField] float moveSpeed;
	[SerializeField] float jumpSpeed; // 지정 Y 속도
	[SerializeField] float ySpeed; // 실제 이동 Y 속도
	[SerializeField] bool groundChecker; // 땅에 붙어있는지 확인
	Vector3 moveDir;

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
		moveDir.x = inputDir.x;
		moveDir.z = inputDir.y;
	}

	private void Move()
	{

		if (moveDir != Vector3.zero)
			transform.rotation = Quaternion.LookRotation(moveDir, Vector3.up); // 회전
		controller.Move(moveDir * moveSpeed * Time.deltaTime); // 이동
	}

	private void OnJump(InputValue value)
	{
		if (groundChecker) // 점프 버튼 눌리고(OnJump), 컨트롤러 isGrounded가 true일 때
			Jump();
	}

	private void Jump()
	{
		// 점프 버튼 눌리고 controller.isGrounded가 true일 때
		// 중력값으로 계속 - 되던 ySpeed 값을 원하는 jumpSpeed로 변경
		ySpeed = jumpSpeed;
		groundChecker = false;
	} // c

	private void Fall()
	{
		// ySpeed : Character Controller에 없는 중력을 Time.deltaTime로 시간이 지날 때마다 계속 더한 값에 물리 중력 y 값을 곱해서 얻어지는 속도 변수
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
		// 아이템 획득
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

		// NPC 단순 대화
		int sizeInter = Physics.OverlapSphereNonAlloc(transform.position, range, collidersInter); // 플레이어 위치부터 범위만큼, 충돌체들을 반환해서 상호작용
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
		// NPC, 오브젝트에 획득 아이템 전달

	}

	private void OnControllerColliderHit(ControllerColliderHit hit)
	{
		if (hit.collider.gameObject.layer == 6) // 땅에 닿았을 때 groundChecker true, 아닐때 false
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
