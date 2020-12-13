using UnityEngine;
using UnityEngine.UI;

public class InfoView : MonoBehaviour
{
    [SerializeField]
    private Button _okBtn;

    void Awake()
    {
        if (_okBtn != null)
        {
            _okBtn.onClick.AddListener(CloseInfo);
        }
        else
        {
            CommonWarnings.ObjectNotAssignedWarning("OkButton");
        }

    }

    void OnEnable()
    {
        AudioManager.PlayMusic(MusicType.Victory);
    }

    private void OnDisable()
    {
        AudioManager.StopMusic();
    }

    private void CloseInfo()
    {
        GameEvents.GameContinueClickedEvent();
        gameObject.SetActive(false);
    }
}
