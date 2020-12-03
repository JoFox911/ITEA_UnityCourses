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

    private void OnScoreChangedHandler(int score)
    {
        _scoreText.SetText(score.ToString("D5"));
    }

    private void OnHighScoreChangedHandler(int score)
    {
        _highScoreText.SetText(score.ToString("D5"));
    }

    private void OnLivesChangedHandler(int lives)
    {
        _livesText.SetText(lives.ToString("D2"));
    }

    private void OnLevelChangedHandler(int level)
    {
        _levelsText.SetText(level.ToString());
    }
}
