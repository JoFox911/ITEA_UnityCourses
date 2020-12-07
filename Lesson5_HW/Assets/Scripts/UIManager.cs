using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Button _bonfireButton;

    [SerializeField]
    private Button _fountainButton;

    [SerializeField]
    private Button _fireworkButton;

    [SerializeField]
    private GameObject _bonfire;

    [SerializeField]
    private GameObject _fountain;

    [SerializeField]
    private GameObject _firework;

    [SerializeField]
    private Camera _camera;


    void Awake()
    {
        _bonfireButton.onClick.AddListener(OnBonfireButtonClickHandler);

        _fountainButton.onClick.AddListener(OnFontainButtonClickHandler);

        _fireworkButton.onClick.AddListener(OnFireworkButtonClickHandler);
    }

    private void OnBonfireButtonClickHandler()
    {
        Debug.Log("OnBonfireButtonClickHandler");
        _fountain.SetActive(false);
        _firework.SetActive(false);
        _camera.transform.position = new Vector3(6.5f, 6.5f, 6.5f);
        _camera.clearFlags = CameraClearFlags.Skybox;
        _bonfire.SetActive(true);

    }

    private void OnFontainButtonClickHandler()
    {
        Debug.Log("OnFontainButtonClickHandler");
        _bonfire.SetActive(false);
        _firework.SetActive(false);
        _camera.transform.position = new Vector3(20f, 18f, 10f);
        _camera.clearFlags = CameraClearFlags.Skybox;
        _fountain.SetActive(true);
    }

    private void OnFireworkButtonClickHandler()
    {
        Debug.Log("OnFireworkButtonClickHandler");
        _bonfire.SetActive(false);
        _fountain.SetActive(false);
        _camera.transform.position = new Vector3(320f, 190f, 230f);
        _camera.clearFlags = CameraClearFlags.SolidColor;
        _firework.SetActive(true);
    }
}
