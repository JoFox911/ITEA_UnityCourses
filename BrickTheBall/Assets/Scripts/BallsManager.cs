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
    private float _initialBallSpeed = 10.0f;

    [SerializeField]
    private Ball _ballPrefab = null;

    public static event Action<BallsManager> OnAllBallsWasted;
    public List<Ball> Balls { get; set; }

    private Ball _initialBall = null;


    #region Singleton
    private static BallsManager _instanceInner;
    private static BallsManager Instance
    {
        get
        {
            if (_instanceInner == null)
            {
                var go = new GameObject("BallsManager");
                _instanceInner = go.AddComponent<BallsManager>();
                DontDestroyOnLoad(_instanceInner.gameObject);
            }
            return _instanceInner;
        }
    }
    #endregion


    

    void Start()
    {
        Ball.OnBallDestroy += OnBallDestroy;
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
                _initialBall.StartMoving(_initialBallSpeed);
            }
        }
    }

    private void OnBallDestroy(Ball ball) {
        Balls.Remove(ball);
        // todo надо ли как-то реагировать на то что єто был главный мячик или нет? скорее всего надо, 
        // если будет рассстроение мяча, то будет троиться главный!
        if (Balls.Count <= 0)
        {
            OnAllBallsWasted?.Invoke(this);
        }
    }

    public void ResetState()
    {
        if (Balls != null)
        {
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
