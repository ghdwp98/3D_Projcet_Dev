using JJH;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
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

	[SerializeField] PlayerHp playerhpmp;
	GameObject nearObject;

	[Header("Interact")]
	Collider[] colliders = new Collider[20];
	[SerializeField] float range;

	[SerializeField] TextMeshProUGUI hpText;

	private void Start()
	{
		playerhpmp.HP = 50;
	}

	private void Update()
	{
		Move();
		Fall();
		hpText.text = "HP : " + (playerhpmp.HP).ToString();
	}

	private void OnMove(InputValue value)
	{
		Vector3 inputDir = value.Get<Vector2>();
		moveDir.x = -inputDir.x;
		moveDir.z = -inputDir.y;
	}

	private void Move()
	{
		if (moveDir != Vector3.zero)
			transform.rotation = Quaternion.LookRotation(moveDir, Vector3.up); // ȸ��
		controller.Move(moveDir * moveSpeed * Time.deltaTime); // �̵�
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
		int size = Physics.OverlapSphereNonAlloc(transform.position, range, colliders); // �÷��̾� ��ġ���� ������ŭ, �浹ü���� ��ȯ�ؼ� ��ȣ�ۿ�
		for (int i = 0; i < size; i++)
		{
			IInteractable interactable = colliders[i].GetComponent<IInteractable>();
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
			Debug.Log("??");
			nearObject = hit.gameObject;
			Debug.Log(nearObject.name);
		}
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
