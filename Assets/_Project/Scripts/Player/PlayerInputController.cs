using UnityEngine;
using UnityEngine.InputSystem;

namespace Web3_Elden_Ring
{
	[RequireComponent(typeof(PlayerInput))]
	public class PlayerInputController : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		[HideInInspector] public bool jump;
		[HideInInspector] public bool sprint;

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;
		
		[HideInInspector] public PlayerInput playerInput;


		#region UNITY_LIFECYCLE

		private void Awake()
		{
			playerInput = GetComponent<PlayerInput>();
			EnableInput(false);
		}

		#endregion
		

		#region INPUT_SYSTEM_EVENT_HANDLERS

		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}

		#endregion


		#region PUBLIC_METHODS
		
		public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} 

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}
		
		public void EnableInput(bool status)
		{
			playerInput.enabled = status;
			
			cursorLocked = status;
			cursorInputForLook = status;
			
			SetCursorState(cursorLocked);
		}

		#endregion


		#region PRIVATE_METHODS

		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}

		#endregion
	}
}