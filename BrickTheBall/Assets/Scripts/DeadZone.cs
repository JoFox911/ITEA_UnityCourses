using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("collision deadZone and " + col.gameObject.tag);
        if (col.gameObject.tag == "Ball")
        {
            Ball ball = col.gameObject.GetComponent<Ball>();
            ball.Die();
        }
    }
}
