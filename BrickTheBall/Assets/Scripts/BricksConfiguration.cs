using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "BricksConfiguration", menuName = "Configs/Bricks list", order = 1)]
public class BricksConfiguration : ScriptableObject
{
    private const string ConfigName = "BricksDataList";

    [SerializeField]
    private List<BrickData> _bricksDataList = new List<BrickData>();

    public static List<BrickData> BricksDataList => _instanceInner._bricksDataList;

    private static BricksConfiguration _instance;

    private static BricksConfiguration _instanceInner
    {
        get
        {
            if (_instance == null)
            {
                _instance = Resources.Load<BricksConfiguration>(ConfigName);
            }

            return _instance;
        }
    }

    private BrickData BrickDataByTypeInner(BrickType type)
    {
        BrickData brickData = null;
        if (BricksDataList != null)
        {
            brickData = BricksDataList.FirstOrDefault(data => data.Type == type);
            if (brickData == null)
            {
                Debug.LogError("THERE IS NO BRICK DATA FOR BRICK TYPE: " + type);
            }
        }

        return brickData;
    }

    public static BrickData BrickDataByType(BrickType type)
    {
        return _instanceInner.BrickDataByTypeInner(type);
    }
}

[Serializable]
public class BrickData
{
    public BrickType Type;
    public int Hitponts;
    public List<Sprite> Sprites;
    public int Points;

    public float SizeX = 0.8f;
    public float SizeY = 0.4f;
    public bool IsEnemyGenerator = false;
}