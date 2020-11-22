using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float _speed = 10f;
    private CharacterController _charController;

    // Start is called before the first frame update
    void Start()
    {
        _charController = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            Motion(transform.forward);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            Motion(transform.forward * (-1));
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Motion(transform.right);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            Motion(transform.right * (-1));
        }    
    }

    private void Motion(Vector3 direction)
    {
        // Скорость игрока завиcит от скорости компьютера (кадровой частоты), 
        // так как код движения зависит от частоты кадров игры и
        // чем чаще вызывается функция Update тем быстрее двигается игрок.
        // DeltaTime - время между кадрами. Умножение скорости на Time.deltaTime 
        // делает скорость перемещений игрока независимой от скорости работы компьютера.
        direction = direction * _speed *  Time.deltaTime;
        _charController.Move(direction);
    }
}
