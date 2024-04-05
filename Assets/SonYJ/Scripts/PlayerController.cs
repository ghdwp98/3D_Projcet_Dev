using JJH;
using System.Collections;
using System.Collections.Generic;
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

	private void Start()
	{
		playerhpmp.HP = 50;
		Debug.Log(playerhpmp.HP);
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
			transform.rotation = Quaternion.LookRotation(moveDir, Vector3.up); // ȸ��
		controller.Move(moveDir * moveSpeed * Time.deltaTime); // �̵�
	}

	private void OnJump(InputValue value)
	{
		if(groundChecker) // ���� ��ư ������(OnJump), ��Ʈ�ѷ� isGrounded�� true�� ��
			Jump();
	}

	private void Jump()
	{
		// ���� ��ư ������ controller.isGrounded�� true�� ��
		// �߷°����� ��� - �Ǵ� ySpeed ���� ���ϴ� jumpSpeed�� ����
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

	/*private void OnDown(InputValue value)
	{
		Down();
	}

	private void Down()
	{
		controller.radius = 0.3f;
		controller.height = 0.5f;
	}

	private void UnDown()
	{
		controller.radius = 0.6f;
		controller.height = 1f;
	}*/

	private void OnControllerColliderHit(ControllerColliderHit hit)
	{
		if (hit.collider.includeLayers == 6) // ���� ����� �� groundChecker true, �ƴҶ� false
			groundChecker = true;
		else
			groundChecker = false;
		/*if (hit.collider.includeLayers == 31) // Damage
		{
			playerhpmp.TakeDamage(5);
			Debug.Log(playerhpmp.HP);
			gameObject.layer = 7; // DamageMusi
			Invoke("OnDamageLayer", 1f);
			Destroy(hit.gameObject);

			if (playerhpmp.HP <= 0)
			{
				transform.position = CheckPoint.GetActiveCheckPointPosition();
			}
		}

		if (hit.collider.includeLayers == 8) // Recovery
		{
			playerhpmp.HP += playerhpmp.HPRecovery;
			Debug.Log(playerhpmp.HP);
			Destroy(hit.gameObject);
		}*/
	}
/*
	void OnDamageLayer()
	{
		gameObject.layer = 0; // Default
	}*/
}
