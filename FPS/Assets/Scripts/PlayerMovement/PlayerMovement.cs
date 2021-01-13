using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerMovement: MonoBehaviour
{
    [SerializeField]
    private GameObject _movementJoystick;

    [SerializeField]
    private float _speed = 4f;
    [SerializeField]
    private float _gravity = -18.62f;
    [SerializeField]
    private float _jumpHeight = 1f;

    [SerializeField]
    private float _groundDistance = 0.01f;
    [SerializeField]
    private LayerMask _groundMask;

    private CharacterController _charController;
    private JoystickDetector _movementJoystickDetector;

    private Vector3 _move;
    private Vector3 _velocity;

    private RaycastHit _hit;
    private float _distanceToColPoint;
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

    void FixedUpdate()
    {
        UpdateIsGrounded();

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
        _distanceToColPoint = _charController.height * 0.5f - _charController.radius;

        Physics.CapsuleCast(transform.position + _charController.center + Vector3.up * _distanceToColPoint,
                            transform.position + _charController.center - Vector3.up * _distanceToColPoint,
                            _charController.radius, Vector3.down, out _hit, _groundDistance, _groundMask);

        _isGrounded = _hit.collider != null;
    }
}
