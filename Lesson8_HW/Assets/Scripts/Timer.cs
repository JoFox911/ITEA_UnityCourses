using System;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float _timeRemaining = 10;
    private bool _timerIsRunning = false;

    private Action _timerOutCallback;
    private Action<int> _eachSecondCallback;

    private int _lastSecondsResidue = 0;

    void Update()
    {
        if (_timerIsRunning)
        {
            if (_timeRemaining > 0)
            {
                var intSeconds = Mathf.FloorToInt(_timeRemaining);
                if (_lastSecondsResidue != intSeconds)
                {
                    _lastSecondsResidue = intSeconds;
                    _eachSecondCallback(intSeconds);
                }

                _timeRemaining -= Time.deltaTime;
            }
            else
            {
                _eachSecondCallback(0);
                _timerOutCallback();
                _timeRemaining = 0;
                _lastSecondsResidue = 0;
                _timerIsRunning = false;
            }
        }
    }

    public void StartTimer(float time)
    {
        _timeRemaining = time;
        _timerIsRunning = true;
    }

    public void InitTimeOutCallback(Action timeOutCallback)
    {
        _timerOutCallback = timeOutCallback;
    }

    public void InitEachSecondCallback(Action<int> eachSecondCallback)
    {
        _eachSecondCallback = eachSecondCallback;
    }

    public bool IsTimerRunning()
    {
        return _timerIsRunning;
    }
}