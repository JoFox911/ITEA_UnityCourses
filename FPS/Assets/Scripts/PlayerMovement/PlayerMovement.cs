using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement: MonoBehaviour
{
    [SerializeField]
    private GameObject _movementJoystick;

    [SerializeField]
    private float _speed = 8f;
    [SerializeField]
    private float _gravity = -18.62f;
    [SerializeField]
    private float _jumpHeight = 3f;

    [SerializeField]
    private Transform _groundCheck;
    [SerializeField]
    private float _groundDistance = 0.01f;
    [SerializeField]
    private LayerMask _groundMask;

    private CharacterController _charController;
    private JoystickDetector _movementJoystickDetector;
    private Vector3 _move;
    private Vector3 _velocity;
    private bool _isGrounded;

    private bool _isMovementStarted;
    private Vector2 _movementDirection;

    void Awake()
    {
        _charController = gameObject.GetComponent<CharacterController>();
        _movementJoystickDetector = _movementJoystick.GetComponent<JoystickDetector>();

        _movementJoystickDetector.IsJoystickUse += SetIsMovementStarted;
        _movementJoystickDetector.Direction += OnDirectionChange;

        EventAgregator.Subscribe<JumpBtnClickedEvent>(Jump);
    }

    void OnDestroy()
    {
        EventAgregator.Unsubscribe<JumpBtnClickedEvent>(Jump);
    }

    private void SetIsMovementStarted(bool isMovementStarted)
    {
        _isMovementStarted = isMovementStarted;
    }

    private void OnDirectionChange(Vector2 movementDirection)
    {
        _movementDirection = movementDirection;
    }

    void FixedUpdate()
    {
        UpdateIsGrounded();

        Debug.Log("_velocity.y " + _velocity.y);

        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = _gravity;
        }

        if (_isMovementStarted)
        {
            Move();
        }

        _velocity.y += _gravity * Time.deltaTime;
        _charController.Move(_velocity * Time.deltaTime);
    }

    private void Move()
    {
        _move = transform.right * _movementDirection.x + transform.forward * _movementDirection.y;
        _charController.Move(_move * _speed * Time.deltaTime);
    }

    private void Jump(object sender, JumpBtnClickedEvent eventData)
    {
        if (_isGrounded)
        {
            // amount velocity to jump on certain height: v = sqrt(h * -2 * g)
            // where h- jump height and g - gravity
            _velocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
        }
    }

    private void UpdateIsGrounded()
    {

        RaycastHit hit;
        Vector3 p1 = transform.position + _charController.center + Vector3.up * -_charController.height * 0.5F;
        Vector3 p2 = p1 + Vector3.up * _charController.height;
        float distanceToObstacle = 0;

        // Cast character controller shape 10 meters forward to see if it is about to hit anything.
        if (Physics.CapsuleCast(p1, p2, _charController.radius, Vector3.down, out hit, _charController.bounds.extents.y + _groundDistance, _groundMask))
            distanceToObstacle = hit.distance;


        //RaycastHit hit;
        //Physics.CapsuleCast(_charController.bounds.center, Vector3.down, out hit, _charController.bounds.extents.y + _groundDistance, _groundMask);

        //Color rayColor;
        //if (hit.collider != null)
        //{
        //    rayColor = Color.green;
        //}
        //else
        //{
        //    rayColor = Color.red;
        //}
        //Debug.DrawRay(_charController.bounds.center, Vector3.down * (_charController.bounds.extents.y + _groundDistance), rayColor);

        _isGrounded = hit.collider != null;
    }
}
