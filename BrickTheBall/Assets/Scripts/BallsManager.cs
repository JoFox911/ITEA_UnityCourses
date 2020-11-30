using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallsManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _platformObject = null;

    [SerializeField]
    private float _initialBallSpeed = 10.0f;

    [SerializeField]
    private Ball _ballPrefab = null;

    public List<Ball> Balls { get; set; }

    private Ball _initialBall = null;

    #region Singleton
    private static BallsManager _instanceInner;
    private static BallsManager _instance
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
        InitBall();
    }

    void Update()
    {
        if (!GamManager.Instance.isGameStarted) 
        {
            _initialBall.transform.SetPostionXY(_platformObject.transform.position.x, 
                                                _platformObject.transform.position.y + 0.50f);

            if (Input.GetKeyDown(KeyCode.Space)) 
            {
                GamManager.Instance.isGameStarted = true;
                _initialBall.StartMoving(_initialBallSpeed);
            }
        }
    }

    private void InitBall()
    {
        if (_platformObject != null)
        {
            Vector3 startingPos = new Vector3(_platformObject.transform.position.x, 
                                              _platformObject.transform.position.y + 0.50f, 
                                              0);
            Debug.Log("BALLS MANAGER InitBall" + _ballPrefab + startingPos);
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
