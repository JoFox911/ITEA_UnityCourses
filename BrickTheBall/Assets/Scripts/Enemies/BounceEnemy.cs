using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BounceEnemy : Enemy
{
    protected override void ApplyEnemyEffect(Collision2D col)
    {
        Ball ball = col.gameObject.GetComponent<Ball>();
        if (GameManager.IsGameStarted() && ball != null)
        {
            //todo explosion sound
            //AudioManager.PlaySFX(SFXType.BallAndPlatformCollision);


            // определяем противоположное направление тому когда отбились об врага и добавляем рандомное изменение
            var newDirection = col.contacts[0].normal + new Vector2(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f));

            // Calculate direction, set length to 1
            ball.SetDirection(newDirection.normalized);
        }
    }
}
