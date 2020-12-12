using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    void Start()
    {
        AudioManager.PlayMusic(MusicType.InMenu);
    }
}
