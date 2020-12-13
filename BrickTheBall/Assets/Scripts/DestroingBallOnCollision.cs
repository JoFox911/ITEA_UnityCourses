using UnityEngine;

public class DestroingBallOnCollision : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D col)
    {
        var obj = col.gameObject.GetComponent<IDestroyableOnCollisionWithDeadZone>();
        if (obj != null)
        {
            obj.DestroyOnCollisionWithDeadZone();
        }
    }
}
