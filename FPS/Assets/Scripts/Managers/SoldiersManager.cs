using System.Collections.Generic;
using UnityEngine;

class SoldiersManager: MonoBehaviour
{
    [SerializeField]
    private GameObject _soldiersContainer;

    [SerializeField]
    private GameObject _playerPrefab;
    [SerializeField]
    private GameObject _botPrefab;

    private List<GameObject> _soldiersList;

    void Awake()
    {
        _soldiersList = new List<GameObject>();
        ServiceLocator.Register<SoldiersManager>(this);
    }

    public void InstantiateSoldier(SoldierData soldierData, Vector3 pos)
    {
        GameObject soldierPrefab = null;
        if (soldierData.type == SoldierType.Bot)
        {
            soldierPrefab = _botPrefab;
        }
        else if (soldierData.type == SoldierType.Player)
        {
            soldierPrefab = _playerPrefab;
        }

        if (soldierPrefab != null)
        {
            var soldier = Instantiate(soldierPrefab, pos, Quaternion.identity);
            soldier.transform.SetParent(_soldiersContainer.transform);
            _soldiersList.Add(soldier);
        }        
    }
}