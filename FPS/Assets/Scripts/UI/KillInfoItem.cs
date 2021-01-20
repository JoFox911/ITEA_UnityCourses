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
        if (_killerName != null)
        {
            _killerName.text = data.Who;
        }
        else
        {
            Common.ObjectNotAssignedWarning("KillerName");
        }

        if (_victimName != null)
        {
            _victimName.text = data.Whom;
        }
        else
        {
            Common.ObjectNotAssignedWarning("VictimName");
        }

        if (_weaponIcon != null)
        {
            _weaponIcon.sprite = data.WeaponIcon;
        }
        else
        {
            Common.ObjectNotAssignedWarning("WeaponIcon");
        }
    }
}
