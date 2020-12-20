using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RopeBall : MonoBehaviour
{
    private Rigidbody _rigidbody;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    //Triggered when you click on the sphere
    void OnMouseDown()
    {
        _rigidbody.isKinematic = false;
    }
}
