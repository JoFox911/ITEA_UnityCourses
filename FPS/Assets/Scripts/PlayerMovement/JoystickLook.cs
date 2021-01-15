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
    private float _minVerticalLookAngle = 45f;

    [SerializeField]
    // 360 - 75 т.к используем localEulerAngles которые измеряются в 0 - 360
    private float _maxVerticalLookAngle = 285f;


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
            xRotation = transform.localEulerAngles.x - (_direction.y * _joystickSensitivity);
            if (xRotation <= _maxVerticalLookAngle || xRotation >= _minVerticalLookAngle)
            {
                transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            }

            _playerBody.Rotate(Vector3.up * _direction.x * _joystickSensitivity);
        }
    }


    //private float RestrictVerticalRotation(float rotationY)
    //{
    //    var currentAngle = NormalizeAngle(rotationY);
    //    var minY = _minVerticalLookAngle + currentAngle;
    //    var maxY = _maxVerticalLookAngle + currentAngle;
    //    return Mathf.Clamp(rotationY, minY + 0.01f, maxY - 0.01f);
    //}

    ///// Normalize an angle between -180 and 180 degrees.
    ///// <param name="angleDegrees">angle to normalize</param>
    ///// <returns>normalized angle</returns>
    //private float NormalizeAngle(float angleDegrees)
    //{
    //    while (angleDegrees > 180f)
    //    {
    //        angleDegrees -= 360f;
    //    }

    //    while (angleDegrees <= -180f)
    //    {
    //        angleDegrees += 360f;
    //    }

    //    return angleDegrees;
    //}
}
