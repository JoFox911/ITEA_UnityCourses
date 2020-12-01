using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BricksManager : MonoBehaviour
{
    #region Singleton
    private static BricksManager _instanceInner;

    public static BricksManager Instance
    {
        get
        {
            if (_instanceInner == null)
            {
                var go = new GameObject("BricksManager");
                _instanceInner = go.AddComponent<BricksManager>();
                DontDestroyOnLoad(_instanceInner.gameObject);
            }
            return _instanceInner;
        }
    }
    #endregion

    //public static List<BrickTypeData> BrickTypesData;

    //[SerializeField]
    //public static BrickTypeData GetBrickDataByType(BrickType brickType)
    //{
    //    return BrickTypesData.First(brickTypeData => brickTypeData.type == brickType);
    //}
}



//[Serializable]
//public class BrickTypeData
//{
//    public BrickType type;
//    public int Hitpoints;
//    public Sprite Sprite;
//}
