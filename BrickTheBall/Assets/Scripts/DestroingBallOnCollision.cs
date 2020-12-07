using UnityEngine;

public class DestroingBallOnCollision : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ball")
        {
            Ball ball = col.gameObject.GetComponent<Ball>();
            ball.Die();
        }
    }
}
