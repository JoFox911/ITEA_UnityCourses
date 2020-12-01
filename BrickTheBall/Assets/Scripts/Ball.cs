using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private float _speed = 10.0f;

    private Rigidbody2D _rigidbody;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void StartMoving(float initialSpeed)
    {
        _speed = initialSpeed;
        _rigidbody.velocity = Vector2.up * _speed;
    }

    
    public void SetDirection(Vector2 direction)
    {
        _rigidbody.velocity = direction * _speed;
    }

    void Update()
    {


        //// todo bottom!
        //if (_isActive && _platformObject != null && transform.position.y < -5)
        //{
        //    Debug.Log("Case1" + transform.rotation.x + transform.rotation.y + transform.localRotation.x + transform.localRotation.y);
        //    _isActive = false;
        //    //_rigidbody.isKinematic = true;

        //    transform.SetPostionXY(_platformObject.transform.position.x, _initialPosY);
        //}
        
    }
}
