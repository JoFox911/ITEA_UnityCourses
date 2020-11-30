using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamManager : MonoBehaviour
{
    public bool isGameStarted { get; set; }

    #region Singleton
    private static GamManager _instanceInner;

    public static GamManager Instance
    {
        get
        {
            if (_instanceInner == null)
            {
                var go = new GameObject("GamManager");
                _instanceInner = go.AddComponent<GamManager>();
                DontDestroyOnLoad(_instanceInner.gameObject);
            }
            return _instanceInner;
        }
    }
    #endregion
}
