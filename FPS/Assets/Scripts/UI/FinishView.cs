using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishView : MonoBehaviour
{
    [SerializeField]
    private Button _okBtn;

    [SerializeField]
    private string _congratsText;

    [SerializeField]
    private string _motivationText;

    [SerializeField]
    private TextMeshProUGUI _resultText;

    [SerializeField]
    private TextMeshProUGUI _labelText;

    void Awake()
    {
        var currentPlace = ServiceLocator.Resolved<GameController>()?.AliveEnemiesNumber + 1;

        _labelText.text = $"You are got {currentPlace} place!";

        if (currentPlace == 1)
        {
            _resultText.text = _congratsText;
        }
        else
        {
            _resultText.text = _motivationText;
        }

        if (_okBtn != null)
        {
            _okBtn.onClick.AddListener(GoToMenu);
        }
        else
        {
            Common.ObjectNotAssignedWarning("OkButton");
        }
       
    }

    //void OnEnable()
    //{
    //    AudioManager.PlayMusic(MusicType.Victory);
    //}

    //private void OnDisable()
    //{
    //    AudioManager.StopMusic();
    //}

    private void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
