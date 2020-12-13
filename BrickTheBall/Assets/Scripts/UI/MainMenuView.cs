using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
        if (_newGameBtn != null)
        {
            _newGameBtn.onClick.AddListener(NewGameClicked);
        }
        else
        {
            CommonWarnings.ObjectNotAssignedWarning("ТewGameBtn");
        }

        if (_continueGameBtn != null)
        {
            _continueGameBtn.onClick.AddListener(ContinueGameClicked);
        }
        else
        {
            CommonWarnings.ObjectNotAssignedWarning("СontinueGameBtn");
        }

        if (_exitGameBtn != null)
        {
            _exitGameBtn.onClick.AddListener(ExitGameClicked);
        }
        else
        {
            CommonWarnings.ObjectNotAssignedWarning("ExitGameBtn");
        }

        if (_settingsBtn != null)
        {
            _settingsBtn.onClick.AddListener(OpenSettings);
        }
        else
        {
            CommonWarnings.ObjectNotAssignedWarning("SettingsBtn");
        }

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

    void Start()
    {
        AudioManager.PlayMusic(MusicType.InMenu);
    }

    private void NewGameClicked()
    {
        RemoveSavedGameProgress();
        SceneManager.LoadScene("Game");
    }

    private void ContinueGameClicked()
    {
        // менеджер игры увидит что есть сохранение и запустит игру с теми данными
        SceneManager.LoadScene("Game");
    }

    private void ExitGameClicked()
    {
        Application.Quit();
    }

    private void OpenSettings()
    {
        if (_settingsScreen != null)
        {
            _settingsScreen.SetActive(true);
        }
        else
        {
            CommonWarnings.ObjectNotAssignedWarning("SettingsScreen");
        }
    }

    private void RemoveSavedGameProgress()
    {
        // удаляем весь сохраненный прогресс уровня, 
        // количества жизней и текущего результата
        PlayerPrefs.DeleteKey("CurrentLevel");
        PlayerPrefs.DeleteKey("CurrentLives");
        PlayerPrefs.DeleteKey("CurrentScore");
    }
}
