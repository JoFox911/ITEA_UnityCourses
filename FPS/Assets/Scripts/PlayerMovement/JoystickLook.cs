using UnityEngine;

public class JoystickLook : MonoBehaviour
{
    [SerializeField]
    private GameObject _rotationJoystick;

    [SerializeField]
    private float _joystickSensitivity = 1f;

    [SerializeField]
    private Transform _playerBody;

    [SerializeField]
    private float _maxBottomLookAngle = 45f;

    [SerializeField]
    // 360 - 75 т.к используем localEulerAngles которые измеряются в 0 - 360
    private float _maxTopLookAngle = 285f;


    private float xRotation = 0f;

    private JoystickDetector _rotationJoystickDetector;

    private bool _isRotationStarted;
    private Vector2 _direction;

    void Awake()
    {
        _rotationJoystickDetector = _rotationJoystick.GetComponent<JoystickDetector>();
        _rotationJoystickDetector.IsJoystickUse += SetIsRotationStarted;
        _rotationJoystickDetector.Direction += OnDirectionChange;
    }

    private void SetIsRotationStarted(bool isRotationStarted) 
    {
        _isRotationStarted = isRotationStarted;
        _direction = Vector2.zero;
    }

    private void OnDirectionChange(Vector2 Direction)
    {
        _direction = Direction;
    }

    void FixedUpdate()
    {

        if (_isRotationStarted)
        {
            
            xRotation = transform.localEulerAngles.x - _direction.y;
            if (xRotation <= _maxBottomLookAngle || xRotation >= _maxTopLookAngle)
            {
                transform.localRotation = Quaternion.Euler(xRotation * _joystickSensitivity, 0f, 0f);
            }
            _playerBody.Rotate(Vector3.up * _direction.x * _joystickSensitivity);
        }
    }
}
