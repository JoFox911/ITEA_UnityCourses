using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Platform : MonoBehaviour
{
    [SerializeField]
    private float _speed = 12.0f;
    
    private Rigidbody2D _rigidbody;
    private Collider2D _collider;

    // Start is called before the first frame update
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // delta time not used there
        _rigidbody.velocity = new Vector2(Input.GetAxis("Horizontal") * _speed, 0.0f);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("collision platform and " + col.gameObject.name);
        // Hit the platform?
        if (col.gameObject.tag == "Ball")
        {
            Ball ball = col.gameObject.GetComponent<Ball>();

            float x = HitFactor(col.transform.position, 
                                transform.position,
                                _collider.bounds.size.x);

            // Calculate direction, set length to 1
            // The value of Y will always be 1, because we want it
            // to fly towards the top and not towards the bottom.
            ball.SetDirection(new Vector2(x, 1).normalized);
        }
    }

    private float HitFactor(Vector2 ballPos, Vector2 platformPos,
                float platformWidth)
    {
        //
        // 1  -0.5  0  0.5   1  <- x value
        // ===================  <- platform
        //
        return (ballPos.x - platformPos.x) / platformWidth;
    }
}
