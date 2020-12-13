using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BounceEnemy : Enemy
{
    protected override void ApplyEnemyEffect(Collision2D col)
    {
        // если игра началась имеется в виду, что мячик запущен
        // если мяч не запущен, на надо к нему применять єффект
        Ball ball = col.gameObject.GetComponent<Ball>();
        if (GameManager.IsGameStarted() && ball != null)
        {
            // определяем противоположное направление тому когда отбились об врага и добавляем рандомное изменение
            var newDirection = col.contacts[0].normal + new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));

            ball.SetDirection(newDirection.normalized);
        }
    }
}
