using UnityEngine;

public class GunLikeWeapon : MonoBehaviour
{
    [SerializeField]
    private float _damage = 10f;
    [SerializeField]
    private float _range = 100f;
    [SerializeField]
    private float _impactForce = 30f;
    [SerializeField]
    private float _fireRate = 15f;

    [SerializeField]
    private ParticleSystem _muzzleFlash;
    [SerializeField]
    private GameObject _impactEffect;

    [SerializeField]
    private WeaponType _type;

    private float _nextTimeToFire = 0f;

    public void Shoot(GameObject _raycastSource)
    {
        if (Time.time <= _nextTimeToFire)
        {
            return;
        }

        _nextTimeToFire = Time.time + 1f / _fireRate;

        _muzzleFlash.Play();
        
        RaycastHit hit;
        if (Physics.Raycast(_raycastSource.transform.position, _raycastSource.transform.forward, out hit, _range))
        {
            Debug.Log("SHOOT" + hit.transform.tag);

            var target = hit.transform.GetComponent<IShootable>();
            if (target != null)
            {
                target.TakeDamage(_damage);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * _impactForce);
            }

            GameObject impactGO  = Instantiate(_impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);
        }
    }

    public WeaponType GetWeaponType()
    {
        return _type;
    }
}
