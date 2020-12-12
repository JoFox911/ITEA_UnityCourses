using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverView : MonoBehaviour
{
    [SerializeField]
    private Button _retryBtn;

    [SerializeField]
    private Button _backToMenuBtn;

    void Awake()
    {
        if (_retryBtn != null)
        {
            _retryBtn.onClick.AddListener(RastartGame);
        }
        else
        {
            CommonWarnings.ObjectNotAssignedWarning("RetryBtn");
        }

        if (_backToMenuBtn != null)
        {
            _backToMenuBtn.onClick.AddListener(GoToMenu);
        }
        else
        {
            CommonWarnings.ObjectNotAssignedWarning("BackToMenuBtn");
        }
    }

    void OnEnable()
    {
        AudioManager.PlaySFX(SFXType.GameOver);
    }

    private void GoToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void RastartGame()
    {
        GameEvents.RestartGameClickedEvent();
        gameObject.SetActive(false);
    }
}
