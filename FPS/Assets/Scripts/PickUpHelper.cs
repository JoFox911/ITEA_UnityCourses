using UnityEngine;

public class PickUpHelper : MonoBehaviour
{
    [SerializeField]
    private float _castDistance = 0.7f;
    [SerializeField]
    private LayerMask _canGrabMask;

    private CharacterController _charController;
    private GameObject _grabableObject;

    private RaycastHit _hit;
    private float _distanceToColPoint;

    public bool IsItemDetected => _grabableObject != null;

    void Awake()
    {
        _charController = gameObject.GetComponent<CharacterController>();
    }

    void Update()
    {
        CheckGrab();
    }

    public bool CheckGrab()
    {
        _distanceToColPoint = _charController.height * 0.5f - _charController.radius;

        if (Physics.CapsuleCast(transform.position + _charController.center + Vector3.up * _distanceToColPoint,
                                transform.position + _charController.center - Vector3.up * _distanceToColPoint,
                                _charController.radius, transform.forward, out _hit, _castDistance, _canGrabMask))
        {
            _grabableObject = _hit.transform.gameObject;
            //Debug.Log("CheckGrab true");
            return true;
        }

        _grabableObject = null;
        return false;
    }

    public GameObject GetPickUpObject()
    {
        return _grabableObject;
    }
}
