using UnityEngine;

public class Soldier : MonoBehaviour, IShootable
{
    [SerializeField]
    private float _health = 50f;



    public void TakeDamage(float amount)
    {
        _health -= amount;
        if (_health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

}
