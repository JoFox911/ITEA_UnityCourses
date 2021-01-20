using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsSpawnsManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _spawnsContainer;

    [SerializeField]
    private List<Vector3> _spawnPossitions;

    [SerializeField]
    private ItemSpawn _spawnsPrefab;

    [SerializeField]
    [Range(0, 100)]
    // регулируем отношение оружия к лечилкам
    // чем больше число тем больше будет лечения относительно оружия
    private float _weaponToHealRatio;

    [SerializeField]
    private List<GameObject> _availableHealPrefabs;

    [SerializeField]
    private List<GameObject> _availableWeaponPrefabs;

    [SerializeField]
    private float _nextItemDelay = 10;

    private List<ItemSpawn> _spawnsList;

    void Awake()
    {
        ServiceLocator.Register<ItemsSpawnsManager>(this);

        _spawnsList = new List<ItemSpawn>();

        foreach (var possition in _spawnPossitions)
        {
            var spawn = Instantiate(_spawnsPrefab, possition, Quaternion.identity);
            spawn.transform.SetParent(_spawnsContainer.transform);
            spawn.SetItemToContainer(GetItemPrefab());
            _spawnsList.Add(spawn);
        }
    }

    void Update()
    {
        foreach (var spawn in _spawnsList)
        {
            if (spawn.IsContainerEmpty() && !spawn.IsWainingForNextItem())
            {
                spawn.SetIsWaitingForNextItem();
                StartCoroutine(SetItemToSpawn(spawn));
            }
        }
    }

    void OnDestroy()
    {
        ServiceLocator.Unregister<ItemsSpawnsManager>();
    }

    public List<Transform> GetAllSpawns()
    {
        var transformsList = new List<Transform>();
        foreach (var spawn in _spawnsList)
        {
            transformsList.Add(spawn.transform);
        }
        return transformsList;
    }

    public IEnumerator SetItemToSpawn(ItemSpawn spawn)
    {
        yield return new WaitForSeconds(_nextItemDelay);
        spawn.SetItemToContainer(GetItemPrefab());
    }

    private GameObject GetItemPrefab()
    {
        float itemTypeValue = Random.Range(0, 100f);
        GameObject itemPrefab = null;

        // _weaponToHealRatio - если рандомное меньше чем єто значение, то будет оруие, если больше - хилка

        if (itemTypeValue < _weaponToHealRatio)
        {
            if (_availableWeaponPrefabs != null)
            {
                itemPrefab = SelectWeaponPrefab();
            }
        }
        else
        {
            if (_availableHealPrefabs != null)
            {
                itemPrefab = SelectHealPrefab();
            }
        }

        return itemPrefab;
    }

    private GameObject SelectWeaponPrefab()
    {
        return SelectItemPrefab(_availableWeaponPrefabs);
    }

    private GameObject SelectHealPrefab()
    {

        return SelectItemPrefab(_availableHealPrefabs);
    }

    private GameObject SelectItemPrefab(List<GameObject> itemsList)
    {
        return itemsList[Random.Range(0, itemsList.Count)];
    }
}
