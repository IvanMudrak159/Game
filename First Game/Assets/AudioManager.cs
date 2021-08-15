using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    void Start()
    {
        AudioListener.volume = PlayerPrefs.GetInt("MusicIsOn", 1);
    }
}
