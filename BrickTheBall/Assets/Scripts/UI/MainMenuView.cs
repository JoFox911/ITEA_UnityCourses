using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//todo refactoring
public class MainMenuView : MonoBehaviour
{
    [SerializeField]
    private Button _newGameBtn;

    [SerializeField]
    private Button _continueGameBtn;

    [SerializeField]
    private Button _exitGameBtn;

    void Awake()
    {
        _newGameBtn.onClick.AddListener(NewGameClicked);
        _continueGameBtn.onClick.AddListener(ContinueGameClicked);
        _exitGameBtn.onClick.AddListener(ExitGameClicked);

        var isContinuePossible = PlayerPrefs.HasKey("CurrentLevel") &&
                                PlayerPrefs.HasKey("CurrentLives") &&
                                PlayerPrefs.HasKey("CurrentScore");
        if (isContinuePossible)
        {
            _continueGameBtn.gameObject.SetActive(true);
        }
        else
        {
            _continueGameBtn.gameObject.SetActive(false);
        }

    }

    private void NewGameClicked()
    {
        RemoveSavedGameProgress();
        SceneManager.LoadScene("Game");
    }

    private void ContinueGameClicked()
    {
        SceneManager.LoadScene("Game");
    }

    private void ExitGameClicked()
    {
        Application.Quit();
    }

    private void RemoveSavedGameProgress()
    {
        PlayerPrefs.DeleteKey("CurrentLevel");
        PlayerPrefs.DeleteKey("CurrentLives");
        PlayerPrefs.DeleteKey("CurrentScore");
    }
}
