using UnityEngine;
using UnityEngine.UI;

public class SettingsView : MonoBehaviour
{
    [SerializeField]
    private Button _okBtn;

    [SerializeField]
    private Slider _masterVolumeSlider;

    [SerializeField]
    private Slider _musicVolumeSlider;

    [SerializeField]
    private Slider _SFXVolumeSlider;

    private const string MasterVolumeParameter = "MasterVolume";
    private const string MusicVolumeParameter = "MusicVolume";
    private const string SFXVolumeParameter = "SFXVolume";

    void Awake()
    {
        
        if (_okBtn != null)
        {
            _okBtn.onClick.AddListener(CloseSettings);
        }
        else
        {
            Common.ObjectNotAssignedWarning("OkButton");
        }

        if (_masterVolumeSlider != null)
        {
            _masterVolumeSlider.onValueChanged.AddListener(OnMasterVolumeSliderChanged);
        }
        else
        {
            Common.ObjectNotAssignedWarning("MasterVolumeSlider");
        }

        if (_musicVolumeSlider != null)
        {
            _musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeSliderChanged);
        }
        else
        {
            Common.ObjectNotAssignedWarning("MusicVolumeSlider");
        }

        if (_SFXVolumeSlider != null)
        {
            _SFXVolumeSlider.onValueChanged.AddListener(OnSFXVolumeSliderChanged);
        }
        else
        {
            Common.ObjectNotAssignedWarning("SFXVolumeSlider");
        }
    }

    private void OnMasterVolumeSliderChanged(float newValue)
    {
        PlayerPrefs.SetFloat(MasterVolumeParameter, newValue);
        EventAgregator.Post(this, new MasterVolumeChangedEvent());
    }

    private void OnMusicVolumeSliderChanged(float newValue)
    {
        PlayerPrefs.SetFloat(MusicVolumeParameter, newValue);
        EventAgregator.Post(this, new MusicVolumeChangedEvent());
    }

    private void OnSFXVolumeSliderChanged(float newValue)
    {
        PlayerPrefs.SetFloat(SFXVolumeParameter, newValue);
        EventAgregator.Post(this, new SFXVolumeChangedEvent());
    }

    private void CloseSettings()
    {
        gameObject.SetActive(false);
    }

}
