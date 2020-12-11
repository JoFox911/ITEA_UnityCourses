using System.Collections.Generic;
using UnityEngine;

public class BonusManager : MonoBehaviour
{
    [SerializeField]
    private List<Bonus> _availableBuffs;

    [SerializeField]
    private List<Bonus> _availableDebuffs;

    [SerializeField]
    // минимальная пауза между двумя бонусами
    private float _minBonusPause = 3f;

    [SerializeField]
    [Range(0, 100)]
    // отдельно регулируем частоту выпадения каких-либо бонусов
    private float _bonusChance;

    [SerializeField]
    [Range(0, 100)]
    // регулируем отношение положительных и негативных бонусов
    // чем больше число тем больше будет бафов относительно дебафов
    private float _buffsToDebuffsRatio;

    private float _lastBonusSpawnTime= 0;

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
        GameEvents.OnAllBallsWasted -= ResetState;
        Bonus.OnBonusDestroy -= OnBonusDestroy;
    }

    private void OnBrickDistructed(Brick brick)
    {
        if (Time.time - _lastBonusSpawnTime < _minBonusPause)
        {
            return;
        }

        float bonusSpawnChance = UnityEngine.Random.Range(0, 100f);
        if (bonusSpawnChance <= _bonusChance)
        {
            float bonusTypeValue = UnityEngine.Random.Range(0, 100f);
            Bonus bonusPrefab = null;

            // _buffsToDebuffsRatio - если рандомное меньше чем єто значение, то будет баф, если больше - дебаф
            // так, зная что сейчас будет сгенерирован бонус, мы можем регулировать тип бонуса с вероятностью выставленной параметром 

            if (bonusTypeValue < _buffsToDebuffsRatio)
            {
                if (_availableBuffs != null)
                { 
                    bonusPrefab = SelectBuffPrefab();
                }
            }
            else
            {
                if (_availableDebuffs != null)
                {
                    bonusPrefab = SelectDebuffPrefab();
                }
            }

            if (bonusPrefab != null)
            {
                _lastBonusSpawnTime = Time.time;
                var newBonus = Instantiate(bonusPrefab, brick.transform.position, Quaternion.identity);
                _displayingBonuses.Add(newBonus);
            }

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



