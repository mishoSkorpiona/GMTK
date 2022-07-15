using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector2 _moveDirection;
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _jumpStrengt = 5;
    [SerializeField] private Vector2 _mDelta;
    [Range(0.05f, 1.0f)][SerializeField] private float _mouseSensitivity = 0.1f;

    private Rigidbody _rigidbody;
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.None;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        RotatePlayer();
    }

    void HandleMovement()
    {
        var transformovement = new Vector3(_moveDirection.x, 0, _moveDirection.y);
        _rigidbody.AddRelativeForce(transformovement*_speed, ForceMode.Acceleration);
    }

    void HandleJumping()
    {
        _rigidbody.AddRelativeForce(new Vector3(0,_jumpStrengt,0));
    }

    void OnMove(InputValue inputValue)
    {
        _moveDirection = inputValue.Get<Vector2>();
    }

    void OnJump(InputValue inputValue)
    {
        //ground check
    }

    void OnCameraMove(InputValue inputValue)
    {
        _mDelta = inputValue.Get<Vector2>();
    }

    void RotatePlayer()
    {
        _rigidbody.AddTorque(new Vector3(0,_mDelta.x * _mouseSensitivity * 0.1f,0));
    }
}
