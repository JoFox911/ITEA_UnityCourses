using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusManager : MonoBehaviour
{
    [SerializeField]
    private List<Bonus> _availableBuffs;

    [SerializeField]
    private List<Bonus> _availableDebuffs;

    [SerializeField]
    [Range(0, 100)]
    private float _buffChance;

    [SerializeField]
    [Range(0, 100)]
    private float _debuffChance;

    private List<Bonus> _displayingBonuses;

    void Awake()
    {
        _displayingBonuses = new List<Bonus>();

        GameEvents.OnBrickDestructed += OnBrickDistructed;
        GameEvents.OnResetGameState += ResetState;
        GameEvents.OnAllBallsWasted += ResetState;
        Bonus.OnBonusDestroy += OnBonusDestroy;
    }

    void OnDestroy()
    {
        GameEvents.OnBrickDestructed -= OnBrickDistructed;
        GameEvents.OnResetGameState -= ResetState;
        Bonus.OnBonusDestroy -= OnBonusDestroy;
    }

    private void OnBrickDistructed(Brick brick)
    {
        float buffSpawnChance = UnityEngine.Random.Range(0, 100f);
        float debuffSpawnChance = UnityEngine.Random.Range(0, 100f);
        Bonus bonusPrefab = null;

        if (buffSpawnChance <= _buffChance && _availableBuffs != null)
        {
            bonusPrefab = SelectBuffPrefab();
        }
        else if (debuffSpawnChance <= _debuffChance && _availableDebuffs != null)
        {
            bonusPrefab = SelectDebuffPrefab();
        }

        if (bonusPrefab != null)
        { 
            var newBonus = Instantiate(bonusPrefab, brick.transform.position, Quaternion.identity);
            _displayingBonuses.Add(newBonus);
        }

    }

    public void ResetState()
    {
        if (_displayingBonuses != null)
        {
            foreach (var Bonus in _displayingBonuses)
            {
                if (Bonus != null)
                {
                    Destroy(Bonus.gameObject);
                }
            }
        }
    }

    private void OnBonusDestroy(Bonus bonus)
    {
        _displayingBonuses.Remove(bonus);
    }

    private Bonus SelectBuffPrefab()
    {
        return selectBonusPrefab(_availableBuffs);
    }

    private Bonus SelectDebuffPrefab()
    {

        return selectBonusPrefab(_availableDebuffs);
    }

    private Bonus selectBonusPrefab(List<Bonus> bonusesList)
    { 
        int index = UnityEngine.Random.Range(0, bonusesList.Count);
        return bonusesList[index];
    }
}



