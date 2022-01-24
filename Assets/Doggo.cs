using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Doggo : MonoBehaviour
{
    GameObject doggo;

    void Start()
    {
        doggo = GameObject.Find("Doggo");
    }

    void Update()
    {
        TextMeshProUGUI ugui = this.GetComponentInChildren<TextMeshProUGUI>();

        if (GlobalVariables.gaveFrisbeeToDoggo) {
            ugui.text = "Yip Yip!";

            // TODO: Move the doggo to some location out of the way
        }
    }
}
