using System.Collections.Generic;
using UnityEngine;

public class ComandsSpawnsManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _soldiersContainer;

    [SerializeField]
    private List<Transform> _spawnPossitions;

    void Awake()
    {
        ServiceLocator.Register<ComandsSpawnsManager>(this);
    }

    public List<Transform> GetAllSpawns()
    {
        return _spawnPossitions;
    }

}
