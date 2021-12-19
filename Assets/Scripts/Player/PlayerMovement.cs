using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
        [SerializeField]
        private float movementSensitivity;

        private float horizontalMovementDelta;

        public void SetupDependencies(PlayerInput playerInput)
        {
                playerInput.OnHorizontalAxis += OnHorizontalMove;
        }

        private void Update()
        {
                transform.Rotate(Vector3.up, horizontalMovementDelta * movementSensitivity);
                horizontalMovementDelta = 0.0f;
        }

        private void OnHorizontalMove(float horizontalDelta)
        {
                horizontalMovementDelta = horizontalDelta;
        }
}