using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PlatformManager : MonoBehaviour
{
    [SerializeField]
    private Bullet _bulletPrefab;
    [SerializeField]
    private GameObject _leftLaser;
    [SerializeField]
    private GameObject _rightLaser;

    private Vector3 _initialPossition;
    private float _initialWidth;
    private float _initialHeigth;

    private IEnumerator _changeSizeCoroutine;
    private IEnumerator _shootingCoroutine;
    private SpriteRenderer _spriteRenderer;

    private List<Bullet> _remainingBullets =  new List<Bullet>();

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _initialPossition = transform.position;
        _initialWidth = _spriteRenderer.size.x;
        _initialHeigth = _spriteRenderer.size.y;

        GameEvents.OnResetGameState += ResetState;
        GameEvents.OnAllBallsWasted += ResetState;
        GameEvents.OnChangePlatformWithCatchEvent += OnChangePlatformWithCatch;
        GameEvents.OnShootingPlatformСatch += OnShootingPlatformСatch;
        GameEvents.OnMultiBallСatch -= OnMultiBallСatch;
    }

    void OnDestroy()
    {
        GameEvents.OnResetGameState -= ResetState;
        GameEvents.OnAllBallsWasted -= ResetState;
        GameEvents.OnChangePlatformWithCatchEvent -= OnChangePlatformWithCatch;
        GameEvents.OnShootingPlatformСatch -= OnShootingPlatformСatch;
        GameEvents.OnMultiBallСatch -= OnMultiBallСatch;
    }

    public void ResetState()
    {
        StopPlatformCoroutines();

        if (_remainingBullets != null)
        {
            foreach (var bullet in _remainingBullets)
            {
                Destroy(bullet.gameObject);
            }
        }
        _remainingBullets = new List<Bullet>();

        SetDefaultPlatformView();
        transform.position = _initialPossition;
    }

    private void OnMultiBallСatch(int ballsNum)
    {
        StopPlatformCoroutines();
        SetDefaultPlatformView();
    }

    private void SetDefaultPlatformView()
    {
        _spriteRenderer.size = new Vector2(_initialWidth, _initialHeigth);
    }

    private void OnChangePlatformWithCatch(float coef)
    {
        // если мы взяли другой бонус для платформы - убираем предыдущий

        // если если взяли тот же бонус, 
        // и напрмер бонус изменения размера был применен еще не до конца, 
        // то предыдущая корутитна остановится и начтенся новая, 
        // но доростет только до того размера который необходим


        // если был активный другой бонус, например сейчас прменяется бонус стрельбы платформой, 
        // то мы останавливаем корутину отвечающую за стрельбу и начинаем корутину отвечающую за размер
        StopPlatformCoroutines();

        _changeSizeCoroutine = ChangeWidth(_initialWidth * coef);
        StartCoroutine(_changeSizeCoroutine);
    }

    private void OnShootingPlatformСatch(float duration, float fireCooldown)
    {
        StopPlatformCoroutines();
        SetDefaultPlatformView();

        _shootingCoroutine = StartShooting(duration, fireCooldown);
        StartCoroutine(_shootingCoroutine);
    }

    private void StopPlatformCoroutines()
    {
        if (_shootingCoroutine != null)
        {
            StopCoroutine(_shootingCoroutine);
        }
        if (_changeSizeCoroutine != null)
        {
            StopCoroutine(_changeSizeCoroutine);
        }
    }

    public IEnumerator ChangeWidth(float newWidth)
    {
        if (newWidth > _spriteRenderer.size.x)
        {
            var currentWidth = _spriteRenderer.size.x;
            while (currentWidth < newWidth)
            {
                currentWidth += Time.deltaTime * 2;
                _spriteRenderer.size = new Vector2(currentWidth, _initialHeigth);
                yield return null;
            }
        }
        else
        {
            var currentWidth = _spriteRenderer.size.x;
            while (currentWidth > newWidth)
            {
                currentWidth -= Time.deltaTime * 2;
                _spriteRenderer.size = new Vector2(currentWidth, _initialHeigth);
                yield return null;
            }
        }
    }

    public IEnumerator StartShooting(float duration, float fireCooldown)
    {
        float shootingDurationLeft = duration;
        float fireCooldownLeft = 0;
        while (shootingDurationLeft >= 0)
        {
            fireCooldownLeft -= Time.deltaTime;
            shootingDurationLeft -= Time.deltaTime;

            if (fireCooldownLeft <= 0)
            {
                Shoot();
                fireCooldownLeft = fireCooldown;
            }

            yield return null;
        }
    }

    private void Shoot()
    {
        SpawnBullet(_leftLaser);
        SpawnBullet(_rightLaser);
    }

    private void OnBulletDestroyHandler(Bullet bullet)
    {
        _remainingBullets.Remove(bullet);
    }

    private void SpawnBullet(GameObject laser)
    {
        Bullet bullet = Instantiate(_bulletPrefab, laser.transform.position, Quaternion.identity);
        bullet.StartMoving();
        bullet.InitCallback(OnBulletDestroyHandler);
        _remainingBullets.Add(bullet);
    }

}
