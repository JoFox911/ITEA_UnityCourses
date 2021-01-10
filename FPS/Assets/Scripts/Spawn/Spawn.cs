using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField]
    private GameObject itemContainer;

    private bool _isWainingForNextItem;


    public bool IsContainerEmpty()
    {
        return itemContainer.transform.childCount <= 0;
    }

    public bool IsWainingForNextItem()
    {
        return _isWainingForNextItem;
    }

    public void SetItemToContainer(GameObject itemPrefab)
    {
        _isWainingForNextItem = false;
        if (itemPrefab != null)
        {
            var item  = Instantiate(itemPrefab, itemContainer.transform.position, Quaternion.identity);
            item.transform.SetParent(itemContainer.transform);
        }
    }

    public void SetIsWaitingForNextItem()
    {
        _isWainingForNextItem = true;
    }

}
