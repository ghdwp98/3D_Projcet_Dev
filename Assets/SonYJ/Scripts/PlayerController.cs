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
	[SerializeField] float jumpSpeed; // 지정 Y 속도
	[SerializeField] float ySpeed; // 실제 이동 Y 속도
	[SerializeField] bool groundChecker; // 땅에 붙어있는지 확인
	Vector3 moveDir;

	[SerializeField] PlayerHp playerhpmp;
	[SerializeField] GameObject nearObject;

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
			transform.rotation = Quaternion.LookRotation(moveDir, Vector3.up); // 회전
		controller.Move(moveDir * moveSpeed * Time.deltaTime); // 이동
	}

	private void OnJump(InputValue value)
	{
		if(groundChecker) // 점프 버튼 눌리고(OnJump), 컨트롤러 isGrounded가 true일 때
			Jump();
	}

	private void Jump()
	{
		// 점프 버튼 눌리고 controller.isGrounded가 true일 때
		// 중력값으로 계속 - 되던 ySpeed 값을 원하는 jumpSpeed로 변경
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

	private void Down()
	{
		controller.radius = 0.3f;
		controller.height = 0.5f;
	}

	private void UnDown()
	{
		controller.radius = 0.6f;
		controller.height = 1f;
	}

	private void OnInteraction(InputValue value)
	{
		if(nearObject != null)
		{
			
		}
	}

	private void Interaction()
	{

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
			Debug.Log(playerhpmp.HP);
			gameObject.layer = 7; // DamageMusi
			Invoke("OnDamageLayer", 1f);
			Destroy(hit.gameObject);

			if (playerhpmp.HP <= 0)
			{
				transform.position = CheckPoint.GetActiveCheckPointPosition();
			}
		}

		if (hit.collider.gameObject.layer == 8) // Recovery
		{
			playerhpmp.HP += playerhpmp.HPRecovery;
			Debug.Log(playerhpmp.HP);
			Destroy(hit.gameObject);
		}
	}

	void OnDamageLayer()
	{
		gameObject.layer = 0; // Default
	}
}
