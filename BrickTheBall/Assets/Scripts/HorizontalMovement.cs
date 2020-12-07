using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class HorizontalMovement : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5.0f;

    private Rigidbody2D _rigidbody;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _rigidbody.velocity = new Vector2(Input.GetAxis("Horizontal") * _speed, 0.0f);
    }
}
