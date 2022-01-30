using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVolume : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        AudioListener.volume = 0.01f;
    }
}
