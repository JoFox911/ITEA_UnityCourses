using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BallsManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _platformObject = null;

    [SerializeField]
    private float _initialBallSpeed = 7.0f;

    [SerializeField]
    private Ball _ballPrefab = null;

    public List<Ball> Balls { get; set; }
    private Ball _initialBall;    

    void Awake()
    {
        Ball.OnBallDestroy += OnBallDestroy;
        GameEvents.OnMultiBallСatch += OnMultiBallСatch;
        GameEvents.OnResetGameState += ResetState;
    }

    void Update()
    {
        if ( !GameManager.Instance.IsGameStarted && _initialBall != null) 
        {
            _initialBall.transform.SetPostionXY(_platformObject.transform.position.x, 
                                                _platformObject.transform.position.y + 0.50f);

            if (!GameManager.Instance.IsGameDisabled && Input.GetKeyDown(KeyCode.Space)) 
            {
                GameManager.Instance.IsGameStarted = true;
                _initialBall.StartMoving(_initialBallSpeed, Vector2.up);
            }
        }
    }

    void OnDestroy()
    {
        Ball.OnBallDestroy -= OnBallDestroy;
        GameEvents.OnMultiBallСatch -= OnMultiBallСatch;
        GameEvents.OnResetGameState -= ResetState;
    }

    private void OnMultiBallСatch(int generatedBallsNumber)
    {
        var newBalls = new List<Ball>();
        foreach (var ball in Balls)
        {
            for (var i = 0; i < generatedBallsNumber; i++)
            {
                var newBall = Instantiate(_ballPrefab, ball.transform.position, Quaternion.identity);
                var direction = new Vector2(UnityEngine.Random.Range(-0.5f, 0.5f), ball.GetVelocity().y);
                newBall.StartMoving(_initialBallSpeed, direction);
                newBalls.Add(newBall);
            }
        }
        Balls = Balls.Concat(newBalls).ToList();
    }

    private void OnBallDestroy(Ball ball) {
        Balls.Remove(ball);
        if (Balls.Count <= 0)
        {
            GameEvents.AllBallsWastedEvent(this);
        }
    }

    public void ResetState()
    {
        Debug.Log("ResetState for ball manager");
        if (Balls != null)
        {
            Debug.Log("Balls" + Balls.Count);
            foreach (var Ball in Balls.ToList())
            {
                Destroy(Ball.gameObject);
            }
        }

        InitBall();
    }

    private void InitBall()
    {
        if (_platformObject != null)
        {
            Vector3 startingPos = new Vector3(_platformObject.transform.position.x, 
                                              _platformObject.transform.position.y + 0.50f, 
                                              0);
            _initialBall = Instantiate(_ballPrefab, startingPos, Quaternion.identity);

            Balls = new List<Ball>
            {
                _initialBall
            };
        } else {
            Debug.LogWarning("Platform not assigned");
        }
    }
}
