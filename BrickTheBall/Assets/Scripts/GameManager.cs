using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isGameStarted { get; set; }

    #region Singleton
    private static GameManager _instanceInner;

    public static GameManager Instance
    {
        get
        {
            if (_instanceInner == null)
            {
                var go = new GameObject("GameManager");
                _instanceInner = go.AddComponent<GameManager>();
                DontDestroyOnLoad(_instanceInner.gameObject);
            }
            return _instanceInner;
        }
    }
    #endregion
}
