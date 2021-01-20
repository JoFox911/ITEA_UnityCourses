using UnityEngine;

public class KillInfoView : MonoBehaviour
{
    [SerializeField] 
    private KillInfoItem _itemPrefab;

    [SerializeField] 
    private Transform _parentForKillInfoItems;

    [SerializeField]
    private float _killInfoPreviewTime = 3f;

    public void AddKillInfoItem(KillInfoData data)
    {
        if (_itemPrefab == null)
        {
            Common.ObjectNotAssignedWarning("KillInfoItem");
            return;
        }
        if (_parentForKillInfoItems == null)
        {
            Common.ObjectNotAssignedWarning("ParentForKillInfoItems");
            return;
        }

        var killInfoItem = Instantiate(_itemPrefab, _parentForKillInfoItems);
        killInfoItem.SetValues(data);
        Destroy(killInfoItem.gameObject, _killInfoPreviewTime);
    }
}

public class KillInfoData
{
    public string Who;
    public string Whom;
    public string WeaponLabel;
    public Sprite WeaponIcon;

    public KillInfoData(string who, string whom, Sprite weaponIcon, string weaponLabel)
    {
        Who = who;
        Whom = whom;
        WeaponIcon = weaponIcon;
        WeaponLabel = weaponLabel;
    }
}
