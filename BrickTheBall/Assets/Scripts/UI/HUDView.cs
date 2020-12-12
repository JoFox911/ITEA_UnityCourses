using TMPro;
using UnityEngine;

public class HUDView : MonoBehaviour
{
    [SerializeField] 
    private TextMeshProUGUI _scoreText;
    [SerializeField] 
    private TextMeshProUGUI _highScoreText;
    [SerializeField]
    private TextMeshProUGUI _livesText;
    [SerializeField]
    private TextMeshProUGUI _levelsText;

    void Awake()
    {
		GameEvents.OnRaiseScore += OnScoreChangedHandler;
        GameEvents.OnRaiseHighScore += OnHighScoreChangedHandler;
        GameEvents.OnChangeLives += OnLivesChangedHandler;
        GameEvents.OnChangeLevel += OnLevelChangedHandler;
	}

	void OnDestroy() 
    {
        GameEvents.OnRaiseScore -= OnScoreChangedHandler;
        GameEvents.OnRaiseHighScore -= OnHighScoreChangedHandler;
        GameEvents.OnChangeLives -= OnLivesChangedHandler;
        GameEvents.OnChangeLevel -= OnLevelChangedHandler;
    }

    private void OnScoreChangedHandler(int score)
    {
        if (_scoreText != null)
        {
            _scoreText.SetText(score.ToString("D5"));
        }
        else
        {
            CommonWarnings.ObjectNotAssignedWarning("ScoreText");
        }
    }

    private void OnHighScoreChangedHandler(int score)
    {
        if (_highScoreText != null)
        {
            _highScoreText.SetText(score.ToString("D5"));
        }
        else
        {
            CommonWarnings.ObjectNotAssignedWarning("HighScoreText");
        }
    }

    private void OnLivesChangedHandler(int lives)
    {
        if (_livesText != null)
        {
            _livesText.SetText(lives.ToString("D2"));
        }
        else
        {
            CommonWarnings.ObjectNotAssignedWarning("LivesText");
        }
    }

    private void OnLevelChangedHandler(int level)
    {
        if (_levelsText != null)
        {
            // +1 потому что level - индекс, который начинается с 0. 
            // Для отображения не подходит
            _levelsText.SetText((level + 1).ToString());
        }
        else
        {
            CommonWarnings.ObjectNotAssignedWarning("LevelsText");
        }
    }
}
