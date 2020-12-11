using UnityEngine;
using UnityEngine.UI;

public class SettingsView : MonoBehaviour
{
    [SerializeField]
    private Button _okBtn;

    void Awake()
    {
        _okBtn.onClick.AddListener(CloseSettings);
    }

    private void CloseSettings()
    {
        gameObject.SetActive(false);
    }

}
