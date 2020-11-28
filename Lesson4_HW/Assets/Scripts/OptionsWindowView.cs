using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class OptionsWindowView : MonoBehaviour
{

    [SerializeField]
    private TMP_Dropdown _resolutionDropdown;

    [SerializeField]
    private TMP_Dropdown _quolityDropdown;

    [SerializeField]
    private Toggle _fullscreenToggle;

    [SerializeField]
    private Slider _masterVolumeSlider;

    [SerializeField]
    private Slider _musicVolumeSlider;

    [SerializeField]
    private Toggle _soundInBackgroundToggle;

    [SerializeField]
    private Toggle _enableChallengingToggle;

    [SerializeField]
    private Toggle _allowFriendsSpectateToggle;

    [SerializeField]
    private Button _creditsButton;

    [SerializeField]
    private Button _cinematicButton;

    void Awake()
    {
        if (_resolutionDropdown != null) {
            _resolutionDropdown.onValueChanged.AddListener(OnResolutionDropdownChanged);
        } else {
            ObjectNotAssignedWarning("ResolutionDropdown");
        }

        if (_quolityDropdown != null) {
            _quolityDropdown.onValueChanged.AddListener(OnQuolityDropdownChanged);
        } else {
            ObjectNotAssignedWarning("QuolityDropdown");
        }

        if (_fullscreenToggle != null) {
            _fullscreenToggle.onValueChanged.AddListener(OnFullscreenToggleChanged);
        } else {
            ObjectNotAssignedWarning("FullscreenToggle");
        }

        if (_masterVolumeSlider != null) {
            _masterVolumeSlider.onValueChanged.AddListener(OnMasterVolumeSliderChanged);
        } else {
            ObjectNotAssignedWarning("MasterVolumeSlider");
        }

        if (_musicVolumeSlider != null) {
            _musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeSliderChanged);
        } else {
            ObjectNotAssignedWarning("MusicVolumeSlider");
        }

        if (_soundInBackgroundToggle != null) {
            _soundInBackgroundToggle.onValueChanged.AddListener(OnSoundInBackgroundToggleChanged);
        } else {
            ObjectNotAssignedWarning("SoundInBackground");
        }

        if (_enableChallengingToggle != null) {
            _enableChallengingToggle.onValueChanged.AddListener(OnEnableChallengingToggleChanged);
        } else {
            ObjectNotAssignedWarning("EnableChallenging");
        }

        if (_allowFriendsSpectateToggle != null) {
            _allowFriendsSpectateToggle.onValueChanged.AddListener(OnAllowFriendsSpectateToggleChanged);
        } else {
            ObjectNotAssignedWarning("AllowFriendsSpectate");
        }

        if (_creditsButton != null) {
            _creditsButton.onClick.AddListener(OnCreditsButtonClicked);
        } else {
            ObjectNotAssignedWarning("CreditsButton");
        }

        if (_cinematicButton != null) {
            _cinematicButton.onClick.AddListener(OnCinematicButtonClicked);
        } else {
            ObjectNotAssignedWarning("CinematicButton");
        }
    }

    private void ObjectNotAssignedWarning(String objName) {
        Debug.Log($"{objName} not assigned");
    }

    private void OnResolutionDropdownChanged(int change)
    {
        Debug.Log($"OnResolutionDropdownChanged - ok, new value: {_resolutionDropdown.options[change].text}");
    }

    private void OnQuolityDropdownChanged(int change)
    {
        Debug.Log($"OnQuolityDropdownChanged - ok, new value: {_quolityDropdown.options[change].text}");
    }

    private void OnFullscreenToggleChanged(bool isOn)
    {
        Debug.Log($"OnFullscreenToggleChanged - ok, isOn: {isOn}");
    }

    private void OnMasterVolumeSliderChanged(float vol)
    {
        Debug.Log($"OnMasterVolumeSliderChanged - ok, new volume: {vol}");
    }

    private void OnMusicVolumeSliderChanged(float vol)
    {
        Debug.Log($"OnMusicVolumeSliderChanged - ok, new volume: {vol}");
    }

    private void OnSoundInBackgroundToggleChanged(bool isOn)
    {
        Debug.Log($"OnSoundInBackgroundToggleChanged - ok, isOn: {isOn}");
    }

    private void OnEnableChallengingToggleChanged(bool isOn)
    {
        Debug.Log($"OnEnableChallengingToggleChanged - ok, isOn: {isOn}");
    }

    private void OnAllowFriendsSpectateToggleChanged(bool isOn)
    {
        Debug.Log($"OnAllowFriendsSpectateToggleChanged - ok, isOn: {isOn}");
    }    

    private void OnCreditsButtonClicked()
    {
        Debug.Log("OnCreditsButtonClicked - ok");
    }

    private void OnCinematicButtonClicked()
    {
        Debug.Log("OnCinematicButtonClicked - ok");
    }
}
