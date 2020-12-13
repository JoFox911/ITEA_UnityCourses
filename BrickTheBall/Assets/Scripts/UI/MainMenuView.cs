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

    [SerializeField]
    private Button _settingsBtn;

    [SerializeField]
    private GameObject _settingsScreen;

    void Awake()
    {
        _newGameBtn.onClick.AddListener(NewGameClicked);
        _continueGameBtn.onClick.AddListener(ContinueGameClicked);
        _exitGameBtn.onClick.AddListener(ExitGameClicked);
        _settingsBtn.onClick.AddListener(OpenSettings);

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

    private void OpenSettings()
    {
        _settingsScreen.SetActive(true);
    }

    private void RemoveSavedGameProgress()
    {
        PlayerPrefs.DeleteKey("CurrentLevel");
        PlayerPrefs.DeleteKey("CurrentLives");
        PlayerPrefs.DeleteKey("CurrentScore");
    }
}
