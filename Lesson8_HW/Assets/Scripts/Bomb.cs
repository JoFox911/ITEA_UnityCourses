using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private float _explosionForce;
    [SerializeField] private float _radius;

    void OnCollisionEnter(Collision collision)
    {
        // react on collisions with any object, except a catapult
        if (!collision.gameObject.CompareTag("Catapult"))
        {
            Exposion();
        }            
    }

    private void Exposion()
    {
        // get all object that will in the explosion wave
        var hitsInfoArray = Physics.SphereCastAll(gameObject.transform.position, _radius, gameObject.transform.up);
        foreach (var hit in hitsInfoArray)
        {
            var rigidBody = hit.collider.gameObject.GetComponent<Rigidbody>();
            if (rigidBody != null)
            {
                rigidBody.AddExplosionForce(_explosionForce, gameObject.transform.position, _radius);
            }
        }
        Destroy(gameObject);
    }
}
