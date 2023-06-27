using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
	[Header("Player Components")]
	[SerializeField] private Transform body;
	[SerializeField] private Animator anim_Player;
	private FloatingJoystick joystick;

	[Header("Player Control Data")]
	public float flt_MoveSpeed;
	private Vector3 leftSideRotationValues = new Vector3(0, 0, 0);
	private Vector3 rightSideRotationValues = new Vector3(0, 180, 0);


	private float currentSpeed;
	private float targetSpeed;
	private float SpeedChangeRate = 10f; // acceleration and deceleration
	private int _animIDSpeed;

	// Input
	private float horizontalInput;
	private float verticalInput;
	private Vector3 moveDirection;

	private void Start()
	{
		SetPlayer();
		
	}


	private void Update()
	{
		GetUserInput();
		HandleBodyRotation();
		MovePlayer();
	}

	private void GetUserInput()
	{
		//horizontalInput = Input.GetAxis("Horizontal");
		//verticalInput = Input.GetAxis("Vertical");

		horizontalInput = joystick.Horizontal;
		verticalInput = joystick.Vertical;

		moveDirection = new Vector3(horizontalInput, verticalInput, 0);
	}

	private void MovePlayer()
	{
		//if (moveDirection == Vector3.zero)
		//{
		//	//anim_Player.SetState(CharacterState.Idle);
		//}
		//else
		//{
		//	//anim_Player.SetState(CharacterState.Walk);
		//}

		targetSpeed = 0f;

		if(moveDirection != Vector3.zero)
		{
			targetSpeed = flt_MoveSpeed;
		}

		currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, Time.deltaTime * SpeedChangeRate);
		transform.Translate(moveDirection * currentSpeed * Time.deltaTime, Space.World);
		anim_Player.SetFloat(_animIDSpeed, currentSpeed);
	}

	private void HandleBodyRotation()
	{
		if(horizontalInput > 0)
		{
			body.localEulerAngles = leftSideRotationValues;
		}
		else if(horizontalInput < 0)
		{
			body.localEulerAngles = rightSideRotationValues;
		}
	}

	private void SetPlayer()
	{
		_animIDSpeed = Animator.StringToHash("Speed");
		joystick = FindObjectOfType<FloatingJoystick>();
	}
}
