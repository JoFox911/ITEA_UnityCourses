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
    private GameObject _settinsMenu;

    void Awake()
    {
        _continueBtn.onClick.AddListener(ContinueGame);
        _exitBtn.onClick.AddListener(ExitGame);
        _settingsBtn.onClick.AddListener(OpenSettings);
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
        _settinsMenu.SetActive(true);
    }

}
