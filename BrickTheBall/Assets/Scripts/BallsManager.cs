using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BallsManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _platformObject;

    [SerializeField]
    private float _initialBallSpeed = 7.0f;

    [SerializeField]
    private Ball _ballPrefab = null;

    private List<Ball> _displayedBalls;
    private Ball _initialBall;

    private Vector2 _initialDirection;

    void Awake()
    {
        _initialDirection = new Vector2(0.33f, 1).normalized;
        _displayedBalls = new List<Ball>();

        GameEvents.OnMultiBallСatched += OnMultiBallСatch;
        GameEvents.OnResetGameState += ResetState;
    }

    void Update()
    {
        if (!GameManager.IsGameStarted() && _initialBall != null) 
        {
            _initialBall.transform.position = GetUpperPlatformPosition();

            if (Input.GetKeyDown(KeyCode.Space)) 
            {
                GameManager.StartPlaying();
                _initialBall.StartMoving(_initialBallSpeed, _initialDirection);
            }
        }
    }

    void OnDestroy()
    {
        GameEvents.OnMultiBallСatched -= OnMultiBallСatch;
        GameEvents.OnResetGameState -= ResetState;
    }

    private Vector3 GetUpperPlatformPosition()
    {
        if (_platformObject != null)
        {
            return new Vector3(_platformObject.transform.position.x, _platformObject.transform.position.y + .5f, _platformObject.transform.position.z);
        }
        else
        {
            CommonWarnings.ObjectNotAssignedWarning("Platform");
            return new Vector3(0f, 0f, 0f);
        }
    }

    private void OnMultiBallСatch(int generatedBallsNumber)
    {
        // copy of balls
        var newBalls = new List<Ball>();
        foreach (var ball in _displayedBalls)
        {
            for (var i = 0; i < generatedBallsNumber; i++)
            {
                var newBall = Instantiate(_ballPrefab, ball.transform.position, Quaternion.identity);
                newBall.StartMoving(_initialBallSpeed, new Vector2(Random.Range(-.5f, .5f), ball.GetVelocity().y));
                newBall.SetDestroyCallback(OnBallDestroy);
                newBalls.Add(newBall);
            }
        }

        _displayedBalls = _displayedBalls.Concat(newBalls).ToList();
    }

    private void AddBallToDisplayedList(Ball ball)
    {
        ball.SetDestroyCallback(OnBallDestroy);
        _displayedBalls.Add(ball);
    }

    private void OnBallDestroy(Ball ball) {
        _displayedBalls.Remove(ball);
        if (_displayedBalls.Count <= 0)
        {
            AudioManager.PlaySFX(SFXType.BallWasted);
            GameEvents.AllBallsWastedEvent();
            ResetState();
        }
    }

    private void ResetState()
    {
        if (_displayedBalls != null)
        {
            foreach (var ball in _displayedBalls.ToList())
            {
                if (ball != null)
                { 
                    Destroy(ball.gameObject);
                }
            }
        }
        _displayedBalls = new List<Ball>();

        InitBall();
    }

    private void InitBall()
    {
        _initialBall = Instantiate(_ballPrefab, GetUpperPlatformPosition(), Quaternion.identity);
        AddBallToDisplayedList(_initialBall);
    }
}
