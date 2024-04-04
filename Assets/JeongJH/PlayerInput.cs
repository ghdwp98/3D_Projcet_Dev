using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace JJH
{
    public class PlayerInput : MonoBehaviour
    {        
        [SerializeField] CharacterController controller;
        [SerializeField] float moveSpeed;
        [SerializeField] float jumpSpeed;

        private Vector3 moveInput;
        private float ySpeed;

        private void Update()
        {
            Move();
            Fall();


            if(Input.GetKeyDown(KeyCode.RightShift))
            {
                transform.position = CheckPoint.GetActiveCheckPointPosition();
            }


        }

        private void Move()
        {
            Vector3 forwardDir = Vector3.ProjectOnPlane(Camera.main.transform.forward, Vector3.up).normalized;
            Vector3 rightDir = Vector3.ProjectOnPlane(Camera.main.transform.right, Vector3.up).normalized;

            Debug.DrawRay(transform.position, forwardDir, Color.blue);
            Debug.DrawRay(transform.position, rightDir, Color.green);

            controller.Move(forwardDir * moveInput.z * moveSpeed * Time.deltaTime);
            controller.Move(rightDir * moveInput.x * moveSpeed * Time.deltaTime);

            if (moveInput.sqrMagnitude > 0)
            {
                Quaternion lookRotation = Quaternion.LookRotation(forwardDir * moveInput.z + rightDir * moveInput.x);
                transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, 3f * Time.deltaTime);
            }
        }

        private void Fall()
        {
            ySpeed += Physics.gravity.y * Time.deltaTime;
            if (controller.isGrounded && ySpeed < 0)
            {
                ySpeed = 0;
            }

            controller.Move(Vector3.up * ySpeed * Time.deltaTime);
        }

        private void OnMove(InputValue value)
        {
            Vector2 input = value.Get<Vector2>();
            moveInput.x = input.x;
            moveInput.z = input.y;

            
        }
        private void Jump()
        {
            ySpeed = jumpSpeed;
        }

        private void OnJump(InputValue value)
        {
            if (value.isPressed && controller.isGrounded)
            {
                Jump();
            }
        }





    }







}

