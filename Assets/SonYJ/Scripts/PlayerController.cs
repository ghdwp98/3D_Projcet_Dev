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
	[SerializeField] float jumpSpeed; // ���� Y �ӵ�
	[SerializeField] float ySpeed; // ���� �̵� Y �ӵ�
	[SerializeField] bool groundChecker; // ���� �پ��ִ��� Ȯ��
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
			transform.rotation = Quaternion.LookRotation(moveDir, Vector3.up); // ȸ��
		controller.Move(moveDir * moveSpeed * Time.deltaTime);
	}

	private void OnJump(InputValue value)
	{
		if (groundChecker) // ���� ��ư ������(OnJump), ��Ʈ�ѷ� isGrounded�� true�� ��
			Jump();
	}

	private void Jump() // c
	{
		// ���� ��ư ������ controller.isGrounded�� true�� ��
		// �߷°����� ��� - �Ǵ� ySpeed ���� ���ϴ� jumpSpeed�� ����
		ani.SetTrigger("jump");
		ySpeed = jumpSpeed;
		groundChecker = false;
		
	}

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
		// ������ ȹ��
		int sizeGet = Physics.OverlapSphereNonAlloc(transform.position, range, colliders);

		for(int i = 0; i < sizeGet; i++)
		{
			IInteractable target = colliders[i].GetComponent<IInteractable>();
			if(target != null)
			{
				target?.Interact(this);
				break; // ���� ����� �ϳ� ��ȣ�ۿ��ϸ� break�� ���߱�, ������ �� �ʿ� X
			}
		}
		// NPC, ������Ʈ�� ȹ�� ������ ����

	}

	private void OnControllerColliderHit(ControllerColliderHit hit)
	{
		if (hit.collider.gameObject.layer == 6) // ���� ����� �� groundChecker true, �ƴҶ� false
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
