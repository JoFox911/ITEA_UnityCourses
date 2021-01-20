using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewGameView : MonoBehaviour
{

    [SerializeField]
    private Button _teamaBtn;

    [SerializeField]
    private Button _deathGameBtn;

    [SerializeField]
    private Button _cancelBtn;

    [SerializeField]
    private TMP_InputField _playerNameInput;


    void Awake()
    {
        if (_teamaBtn != null)
        {
            _teamaBtn.onClick.AddListener(TeamBtnClicked);
        }
        else
        {
            Common.ObjectNotAssignedWarning("TeamaBtn");
        }

        if (_deathGameBtn != null)
        {
            _deathGameBtn.onClick.AddListener(DeathBtnClicked);
        }
        else
        {
            Common.ObjectNotAssignedWarning("DeathGameBtn");
        }

        if (_cancelBtn != null)
        {
            _cancelBtn.onClick.AddListener(CancelBtnClicked);
        }
        else
        {
            Common.ObjectNotAssignedWarning("CancelBtn");
        }

        if (PlayerPrefs.HasKey("PlayerName") && _playerNameInput != null)
        {
            _playerNameInput.text = PlayerPrefs.GetString("PlayerName", "Nemo");
        }
    }

    private void TeamBtnClicked()
    {
        PrepareGame(GameTypes.TeamMatch);
    }

    private void DeathBtnClicked()
    {
        PrepareGame(GameTypes.DeathMatch);
    }

    public void PrepareGame(GameTypes type)
    {
        var playerName = _playerNameInput.text == string.Empty ? "Nemo" : _playerNameInput.text;
        PlayerPrefs.SetString("PlayerName", playerName);
        PlayerPrefs.SetString("GameType", type.ToString());

        SceneManager.LoadScene(1);
    }

    private void CancelBtnClicked()
    {
        gameObject.SetActive(false);
    }
}
