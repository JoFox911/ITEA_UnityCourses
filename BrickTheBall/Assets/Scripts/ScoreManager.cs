using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private ScoreController _scoreController;
    void Awake()
    {
        _scoreController = new ScoreController();
        GameEvents.OnBrickDestructed += AddScoreOnBrickDestructed;
    }

    private void AddScoreOnBrickDestructed(Brick brick)
    {
        _scoreController.AddScore(brick.Points);
    }

    public void ResetState()
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
        //Load();
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
        if (Score >= HighScore)
        {
            HighScore = Score;
            GameEvents.RaiseHighScoreEvent(HighScore);
            //Save();
        }
        GameEvents.RaiseScoreEvent(Score);
    }
}
