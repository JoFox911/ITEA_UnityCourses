using UnityEngine;

public class DestroingBallOnCollision : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("OnCollisionEnter2D" + col.gameObject.tag);
        var obj = col.gameObject.GetComponent<IDestroyableOnCollisionWithDeadZone>();
        if (obj != null)
        {
            obj.DestroyOnCollisionWithDeadZone();
        }
    }
}
