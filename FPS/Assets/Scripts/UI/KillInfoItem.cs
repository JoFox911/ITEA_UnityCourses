using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KillInfoItem : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _killerName;

    [SerializeField]
    private TextMeshProUGUI _victimName;

    [SerializeField]
    private Image _weaponIcon;

    public void SetValues(KillInfoData data)
    {
        _killerName.text = data.Who;
        _victimName.text = data.Whom;
        _weaponIcon.sprite = data.WeaponIcon;
    }
}
