using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVolume : MonoBehaviour
{

    // Update is called once per frame
    void Awake()
    {
        AudioListener.volume = 0.05f;
    }
}