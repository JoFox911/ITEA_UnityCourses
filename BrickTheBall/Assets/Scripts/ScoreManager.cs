using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private ScoreController _scoreController;
    void Awake()
    {
        _scoreController = new ScoreController();

        GameEvents.OnBrickDestructed += AddScoreOnBrickDestructed;
        GameEvents.OnEnemyDestroyed += AddScoreOnEnemyDestroyed;
        GameEvents.OnResetGameState += ResetState;
    }

    void OnDestroy()
    {
        GameEvents.OnBrickDestructed -= AddScoreOnBrickDestructed;
        GameEvents.OnEnemyDestroyed -= AddScoreOnEnemyDestroyed;
        GameEvents.OnResetGameState -= ResetState;
    }

    private void AddScoreOnBrickDestructed(Brick brick)
    {
        _scoreController.AddScore(brick.GetPoints());
    }

    private void AddScoreOnEnemyDestroyed(int points)
    {
        _scoreController.AddScore(points);
    }
    private void ResetState()
    {
        _scoreController.ResetScore();
    }
}

public class ScoreController
{
    public int Score;
    public int HighScore;

    public ScoreController()
    {
        Score = PlayerPrefs.GetInt("CurrentScore", 0);
        HighScore = PlayerPrefs.GetInt("HighScore", 0);
        GameEvents.RaiseScoreEvent(Score);
        GameEvents.RaiseHighScoreEvent(HighScore);
    }
    public void AddScore(int score)
    {
        SetScore(Score + score);
    }

    public void ResetScore()
    {
        SetScore(0);
    }

    private void SetScore(int score)
    {
        Score = score;
        PlayerPrefs.SetInt("CurrentScore", Score);
        if (Score >= HighScore)
        {
            HighScore = Score;
            PlayerPrefs.SetInt("HighScore", HighScore);
            GameEvents.RaiseHighScoreEvent(HighScore);
        }
        GameEvents.RaiseScoreEvent(Score);
    }
}
