using System.Collections;
using UnityEngine;

public class Grenade : Weapon
{

    [SerializeField]
    private float _explosionDelay = 4f;

    [SerializeField]
    protected float _impactRadius = 6f;

    [SerializeField]
    protected float _reloadTime = 2f;

    private float _nextTimeToFire = 0f;

    void Awake()
    {
        _currentAmmo = _ammoVolume;
    }

    public override void Shoot(GameObject _raycastSource, string shooterName)
    {
        StartCoroutine(Explode(shooterName));
        _nextTimeToFire = Time.time + _reloadTime;
    }

    private IEnumerator Explode(string shooterName)
    {
        yield return new WaitForSeconds(_explosionDelay);
        var colliders = Physics.OverlapSphere(transform.position, _impactRadius);

        foreach (var collider in colliders)
        {
            var rig = collider.GetComponent<Rigidbody>();
            if (rig != null)
            {
                rig.AddExplosionForce(_impactForce, transform.position, _impactRadius, 1f, ForceMode.Impulse);
            }

            var target = collider.transform.GetComponent<IShootable>();
            if (target != null)
            {
                target.TakeDamage(new AttackData(shooterName, _damage, _weaponName, _weaponIcon));
            }
        }

        var impact = Instantiate(_impactEffect, transform.position, transform.rotation);
        Destroy(impact, _destroyImpactTimeout);
        Destroy(gameObject);
    }

    public override bool IsWeaponReady()
    {
        return Time.time >= _nextTimeToFire;
    }
}
