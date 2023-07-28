using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 10;
    [SerializeField] private float _rotationSpeed = 10f;
    [SerializeField] private float _playerRadius = 0.7f;
    [SerializeField] private float _playerHeight = 2f;

    private Vector3 MovementVector { get; set; }

    private void Update()
    {
        Vector2 inputVector = GameInput.Instance.GetMovementVectorNormalized();
        MovementVector = new Vector3(inputVector.x, 0, inputVector.y);

        HandleRotation();
        HandleMovement();
    }
    public void HandleMovement()
    {
        RestrictMovementVector();

        MovePlayer(MovementVector);
    }

    private void MovePlayer(Vector3 movingVector) => transform.position += movingVector * Time.deltaTime * _moveSpeed;

    private void RestrictMovementVector()
    {
        Vector3 directionToTry = MovementVector;
        if (!HasObstacleOnTheWay(directionToTry))
        {
            MovementVector = directionToTry;
            return;
        }

        bool ControllerStickCheck(Vector3 inputVector) => inputVector.magnitude > 0.5f;

        directionToTry = Vector3.right * MovementVector.x;
        if (!HasObstacleOnTheWay(directionToTry) && ControllerStickCheck(directionToTry))
        {
            MovementVector = directionToTry;
            return;
        }

        directionToTry = Vector3.forward * MovementVector.z;
        if (!HasObstacleOnTheWay(directionToTry) && ControllerStickCheck(directionToTry))
        {
            MovementVector = directionToTry;
            return;
        }

        MovementVector = Vector3.zero;
        return;
    }

    private bool HasObstacleOnTheWay(Vector3 direction) => Physics.CapsuleCast
            (transform.position, transform.position + Vector3.up * _playerHeight, _playerRadius, direction, Time.deltaTime * _moveSpeed);

    // !!! Not best way to rotate
    public void HandleRotation() =>
        transform.forward = Vector3.Slerp(transform.forward, MovementVector, Time.deltaTime * _rotationSpeed);
    //_player.transform.forward = Vector3.RotateTowards(_player.transform.forward, moveDirection, _rotationSpeed, 1);  // ??????

    public bool IsMoving() => MovementVector != Vector3.zero;
}
