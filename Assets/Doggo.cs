using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Doggo : MonoBehaviour
{
    GameObject doggo;
    GameObject waypoint;

    void Start()
    {
        doggo = GameObject.Find("Doggo");
        waypoint = GameObject.Find("WayPointDoggo");
    }

    void Update()
    {
        TextMeshProUGUI ugui = this.GetComponentInChildren<TextMeshProUGUI>();

        if (GlobalVariables.gaveFrisbeeToDoggo) {
            ugui.text = "Yip Yip!";

            transform.position = Vector2.MoveTowards(doggo.transform.position, waypoint.transform.position, 2*Time.deltaTime);
        }
    }
}
