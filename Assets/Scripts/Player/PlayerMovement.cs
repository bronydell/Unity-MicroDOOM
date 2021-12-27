using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
        [SerializeField]
        private float movementSensitivity;
        [SerializeField]
        private float rotationSensitivity;

        private CharacterController characterController;
        
        private float horizontalMovementDelta;
        private float verticalMovementDelta;
        private Vector2 mouseDelta;

        private void Awake()
        {
                characterController = GetComponent<CharacterController>();
        }

        public void SetupDependencies(PlayerInput playerInput)
        {
                playerInput.OnHorizontalAxis += OnHorizontalMove;
                playerInput.OnVerticalAxis += OnVerticalMove;
                playerInput.OnMouseHorizontal += OnMouseMove;
        }

        private void Update()
        {
                Vector3 movementDirection = Vector3.zero;
                movementDirection += transform.forward * verticalMovementDelta;
                movementDirection += transform.right * horizontalMovementDelta;
                movementDirection *= movementSensitivity;
                movementDirection += Physics.gravity;
                movementDirection *= Time.deltaTime;
                characterController.Move(movementDirection);
                float horizontalRotationPower = mouseDelta.x * rotationSensitivity * Time.deltaTime;
                transform.Rotate(Vector3.up, horizontalRotationPower);
                horizontalMovementDelta = 0.0f;
                verticalMovementDelta = 0.0f;
                mouseDelta = Vector2.zero;
        }

        private void OnHorizontalMove(float horizontalDelta)
        {
                horizontalMovementDelta = horizontalDelta;
        }

        private void OnVerticalMove(float verticalDelta)
        {
                verticalMovementDelta = verticalDelta;
        }

        private void OnMouseMove(Vector2 mouseDelta)
        {
                this.mouseDelta = mouseDelta;
        }
}