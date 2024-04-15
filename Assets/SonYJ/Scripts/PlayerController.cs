using JJH;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	[Header("Component")]
	[SerializeField] CharacterController controller;
	[SerializeField] Animator ani;

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
		CleanInven();
	}

	private void CleanInven()
	{
		if (Input.GetKeyDown(KeyCode.Q))
		{
			Manager.Inven.ClearInven();
		}
	}
	private void OnMove(InputValue value)
	{
		Vector3 inputDir = value.Get<Vector2>();
		if(inputDir != Vector3.zero)
		{
			ani.SetBool("run", true);
		}
		else
		{
			ani.SetBool("run", false);
		}
		string temp = Manager.Scene.GetCurSceneName();
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
		if(temp == "3M2")
		{
			moveDir.x = inputDir.x;
			moveDir.z = inputDir.y;
		}
	}

	private void Move()
	{
		if (moveDir != Vector3.zero)
			transform.rotation = Quaternion.LookRotation(moveDir, Vector3.up); // 회전
		controller.Move(moveDir * moveSpeed * Time.deltaTime);
	}

	private void OnJump(InputValue value)
	{
		if (groundChecker) // 점프 버튼 눌리고(OnJump), 컨트롤러 isGrounded가 true일 때
			Jump();
	}

	private void Jump() // c
	{
		// 점프 버튼 눌리고 controller.isGrounded가 true일 때
		// 중력값으로 계속 - 되던 ySpeed 값을 원하는 jumpSpeed로 변경
		ani.SetTrigger("jump");
		ySpeed = jumpSpeed;
		groundChecker = false;
		
	}

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

	private void Down() // z
	{
		controller.radius = 0.6f;
		controller.height = 2.0f;
	}

	private void UnDown()
	{
		controller.radius = 0.5f;
		controller.height = 2.7f;
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
			if(target != null)
			{
				target?.Interact(this);
				break; // 제일 가까운 하나 상호작용하면 break로 멈추기, 여러개 할 필요 X
			}
		}
		// NPC, 오브젝트에 획득 아이템 전달

	}

	private void OnControllerColliderHit(ControllerColliderHit hit)
	{
		if (hit.collider.gameObject.layer == 6) // 땅에 닿았을 때 groundChecker true, 아닐때 false
			groundChecker = true;
		else
			groundChecker = false;

		/*if (hit.collider.gameObject.layer == 31) // Damage
		{
			gameObject.layer = 7; // DamageMusi
			Invoke("OnDamageLayer", 1f);

			if (playerhpmp.HP <= 0)
				transform.position = CheckPoint.GetActiveCheckPointPosition();
		}*/
	}

	/*void OnDamageLayer()
	{
		gameObject.layer = 0; // Default
	}*/

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, range);
	}
}
