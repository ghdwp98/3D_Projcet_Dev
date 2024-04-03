using JJH;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] CharacterController controller;
	[SerializeField] PlayerHp playerhpmp;

	[SerializeField] float moveSpeed;
	[SerializeField] float jumpSpeed;

	private Vector3 moveDir;
	private float ySpeed;

	[SerializeField] LayerMask groundChecker;
	[SerializeField] bool isJump;

	Vector3 moveVec;

	public SphereCollider childCollider;

	private void Start()
	{
		//childCollider = GetComponentInChildren<SphereCollider>();
		Debug.Log(childCollider.gameObject.name);
	}
	private void Update()
	{
		Move();
		Jump();
	}
	private void Move()
	{
		controller.Move(moveDir * moveSpeed * Time.deltaTime);
	}
	private void OnMove(InputValue value)
	{
		Vector2 inputDir = value.Get<Vector2>();
		moveDir.x = inputDir.x;
		moveDir.z = inputDir.y;
	}

	void Turn()
	{
		transform.LookAt(transform.position + moveVec);
	}

	private void Jump()
	{
		ySpeed += Physics.gravity.y * Time.deltaTime;
		controller.Move(ySpeed * Vector3.up * Time.deltaTime);
	}
	private void OnJump(InputValue value)
	{
		if(isJump)
			ySpeed = jumpSpeed;
	}

	private void OnCollisionEnter(Collision collision)
	{
		Debug.Log("CollisionEnter 되었는지");
		if(childCollider.gameObject.tag == "Floor")
			isJump = false;
		
		if (collision.gameObject.layer == 31)
		{
			Debug.Log("충돌진입");
			gameObject.layer = 6;
			PlayerHp.Player_Action?.Invoke(10);
			Invoke("OnDamegeLayer", 1f);
			Debug.Log(playerhpmp.HP);
			Destroy(collision.gameObject);
		}
	}
}
