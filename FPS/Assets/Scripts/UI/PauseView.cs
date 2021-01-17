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
            Common.ObjectNotAssignedWarning("ContinueButton");
        }

        if (_exitBtn != null)
        {
            _exitBtn.onClick.AddListener(ExitGame);
        }
        else
        {
            Common.ObjectNotAssignedWarning("ExitButton");
        }

        if (_settingsBtn != null)
        {
            _settingsBtn.onClick.AddListener(OpenSettings);
        }
        else
        {
            Common.ObjectNotAssignedWarning("SettingsButton");
        }
    }

    //void OnEnable()
    //{
    //    //AudioManager.PlayMusic(MusicType.Pause);
    //}

    //private void OnDisable()
    //{
    //    //AudioManager.StopMusic();
    //}

    private void ContinueGame()
    {
        EventAgregator.Post(this, new UnpauseClickedEvent());
        gameObject.SetActive(false);
    }

    private void ExitGame()
    {
        SceneManager.LoadScene(0);
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
