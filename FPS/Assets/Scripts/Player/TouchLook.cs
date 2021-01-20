using UnityEngine;

public class TouchLook : MonoBehaviour
{
    [SerializeField]
    private Transform cameraTransform;

    [SerializeField]
    private Transform _playerBody;

    [SerializeField]
    private float _minVerticalLookAngle = -75f;

    [SerializeField]
    private float _maxVerticalLookAngle = 40f;

    // Player settings
    [SerializeField]
    private float _cameraSensitivity = 5f;

    // Touch detection
    private int _fingerId = -1;
    private float _halfScreenWidth;
    private Touch _touch;

    // Camera control
    private Vector2 _lookInput;
    private float _cameraPitch;


    void Start()
    {
        // id = -1 means the finger is not being tracked
        _fingerId = -1;

        // only calculate once
        _halfScreenWidth = Screen.width / 2;
    }

    void Update()
    {
        // Handles input
        GetTouchInput();

        if (_fingerId != -1)
        {
            // Ony look around if the right finger is being tracked
            LookAround();
        }
    }

    private void GetTouchInput()
    {
        // Iterate through all the detected touches
        for (int i = 0; i < Input.touchCount; i++)
        {

            _touch = Input.GetTouch(i);

            // Check each touch's phase
            switch (_touch.phase)
            {
                case TouchPhase.Began:
                     if (_touch.position.x > _halfScreenWidth && _fingerId == -1)
                    {
                        // Start tracking the rightfinger if it was not previously being tracked
                        _fingerId = _touch.fingerId;
                    }

                    break;
                case TouchPhase.Ended:
                case TouchPhase.Canceled:

                    if (_touch.fingerId == _fingerId)
                    {
                        // Stop tracking the right finger
                        _fingerId = -1;
                    }

                    break;
                case TouchPhase.Moved:

                    // Get input for looking around
                    if (_touch.fingerId == _fingerId)
                    {
                        _lookInput = _touch.deltaPosition * _cameraSensitivity * Time.deltaTime;
                    }

                    break;
                case TouchPhase.Stationary:
                    // Set the look input to zero if the finger is still
                    if (_touch.fingerId == _fingerId)
                    {
                        _lookInput = Vector2.zero;
                    }
                    break;
            }
        }
    }

    void LookAround()
    {
        // vertical (pitch) rotation
        _cameraPitch = Mathf.Clamp(_cameraPitch - _lookInput.y, _minVerticalLookAngle, _maxVerticalLookAngle);
        cameraTransform.localRotation = Quaternion.Euler(_cameraPitch, 0, 0);

        // horizontal (yaw) rotation
        _playerBody.Rotate(_playerBody.up * _lookInput.x);
    }
}