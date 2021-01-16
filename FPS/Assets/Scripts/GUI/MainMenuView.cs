using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour
{
    [SerializeField]
    private Button _newGameBtn;

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
            Common.ObjectNotAssignedWarning("ТewGameBtn");
        }

        if (_exitGameBtn != null)
        {
            _exitGameBtn.onClick.AddListener(ExitGameClicked);
        }
        else
        {
            Common.ObjectNotAssignedWarning("ExitGameBtn");
        }

        if (_settingsBtn != null)
        {
            _settingsBtn.onClick.AddListener(OpenSettings);
        }
        else
        {
            Common.ObjectNotAssignedWarning("SettingsBtn");
        }

    }

    void Start()
    {
        AudioManager.PlayMusic(MusicType.InMenu);
    }

    private void NewGameClicked()
    {
        SceneManager.LoadScene(1);
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
            Common.ObjectNotAssignedWarning("SettingsScreen");
        }
    }
}
