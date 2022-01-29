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

        // Dog has frisbee
        if (GlobalVariables.gaveFrisbeeToDoggo) {
            ugui.text = "Yip Yip!";

            transform.position = Vector2.MoveTowards(doggo.transform.position, waypoint.transform.position, 2*Time.deltaTime);
        }

        // Dog is watching for frisbee
        if (!GlobalVariables.gaveFrisbeeToDoggo) {
            GameObject[] frisbees = GameObject.FindGameObjectsWithTag("Frisbee");

            if (frisbees.Length > 0) {
                GameObject frisbee = frisbees[0];

                if ((transform.position - frisbee.transform.position).magnitude < 5)
                transform.position = Vector2.MoveTowards(doggo.transform.position, frisbee.transform.position, 3*Time.deltaTime);
            }
        }
 
    }
}
