using System.Collections;
using UnityEngine;

public class Grenade : Weapon
{

    [SerializeField]
    private float _delay = 4f;

    [SerializeField]
    protected float _impactRadius = 6f;

    private float _nextTimeToFire = 0f;

    public override void Shoot(GameObject _raycastSource, string shooterName)
    {
        Debug.Log("granage Shoot");
        StartCoroutine(Explode(shooterName));
        _nextTimeToFire = Time.time + _reloadTime;
    }

    private IEnumerator Explode(string shooterName)
    {
        yield return new WaitForSeconds(_delay);
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
                var shootData = new AttackData(_damage, shooterName, _weaponName, _weaponIcon);
                target.TakeDamage(shootData);
            }
        }
        GameObject go = Instantiate(_impactEffect, transform.position, transform.rotation);
        Destroy(go, 2f);
        Destroy(gameObject);
    }

    public override void Reload(int enabledAmmoNumber, out int remaining)
    {
        remaining = enabledAmmoNumber;
    }


    public override int GetCurrentAmmo()
    {
        return 1;
    }

    public override bool IsWeaponReady()
    {
        return (Time.time >= _nextTimeToFire) && !_isOutOfAmmo && !_isReloading;
    }
}
