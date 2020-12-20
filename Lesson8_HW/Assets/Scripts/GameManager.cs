using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Timer _timer;

    private const float minTimeBeforeExplosion = 3;
    private const float maxTimeBeforeExplosion = 5;

    void Awake()
    {
        GameEvents.OnButtonPressed += StartExplosionTimer;
    }

    void Update()
    {
        //If you press R - reload scene
        if (Input.GetKeyDown(KeyCode.R))
        { 
            SceneManager.LoadScene(SceneManager.GetActiveScene().ToString());
        }
    }

    void OnDestroy()
    {
        GameEvents.OnButtonPressed -= StartExplosionTimer;
    }

    private void StartExplosionTimer()
    {
        if (!_timer.IsTimerRunning())
        {
            var timeBeforeExplosion = Random.Range(minTimeBeforeExplosion, maxTimeBeforeExplosion);

            _timer.StartTimer(Mathf.Round(timeBeforeExplosion));
            _timer.InitTimeOutCallback(OnTimerOut);
            _timer.InitEachSecondCallback(OnSecondPassed);
        }
    }

    private void OnTimerOut()
    {
        GameEvents.TimeOutEvent();
    }

    private void OnSecondPassed(int secondsResidue)
    {
        GameEvents.ChangeSecondsResidueEvent(secondsResidue);
    }
}
