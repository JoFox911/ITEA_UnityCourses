using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Catapult : MonoBehaviour
{
    [SerializeField]
    private Vector3 _throwAngularVelocity = new Vector3(-10, 0, 0);
    [SerializeField]
    private float _throwDuration = .2f;

    private Rigidbody _rigidbody;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();

        GameEvents.OnTimeOut += ThrowTheBomb;
    }

    void OnDestroy()
    {
        GameEvents.OnTimeOut -= ThrowTheBomb;
    }

    private void ThrowTheBomb()
    {
        _rigidbody.angularVelocity = _throwAngularVelocity;
        StartCoroutine(StopCatapultArm());
    }

    IEnumerator StopCatapultArm()
    {
        yield return new WaitForSeconds(_throwDuration);
        _rigidbody.angularVelocity = new Vector3(0, 0, 0);

    }
}
