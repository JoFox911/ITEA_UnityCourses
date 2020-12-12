using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseView : MonoBehaviour
{
    [SerializeField]
    private Button _continueBtn;

    [SerializeField]
    private Button _exitBtn;

    [SerializeField]
    private Button _settingsBtn;

    [SerializeField]
    private GameObject _settingsScreen;

    void Awake()
    {
        if (_continueBtn != null)
        {
            _continueBtn.onClick.AddListener(ContinueGame);
        }
        else
        {
            CommonWarnings.ObjectNotAssignedWarning("ContinueButton");
        }

        if (_exitBtn != null)
        {
            _exitBtn.onClick.AddListener(ExitGame);
        }
        else
        {
            CommonWarnings.ObjectNotAssignedWarning("ExitButton");
        }

        if (_settingsBtn != null)
        {
            _settingsBtn.onClick.AddListener(OpenSettings);
        }
        else
        {
            CommonWarnings.ObjectNotAssignedWarning("SettingsButton");
        }
    }

    void OnEnable()
    {
        AudioManager.PlayMusic(MusicType.Pause);
    }

    private void OnDisable()
    {
        AudioManager.StopMusic();
    }

    private void ContinueGame()
    {
        GameEvents.GameContinueClickedEvent();
        gameObject.SetActive(false);
    }

    private void ExitGame()
    {
        SceneManager.LoadScene("MainMenu");
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

}
