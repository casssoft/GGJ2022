using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Doggo : MonoBehaviour
{
    GameObject doggo;
    GameObject waypoint;
    public GameObject barkPrefab;
    GameObject sibling;
    public float delayBetweenBarks = 3;
    float lastBark;

    void Start()
    {
        doggo = GameObject.Find("Doggo");
        sibling = GameObject.Find("Sibling");
        waypoint = GameObject.Find("WayPointDoggo");
        lastBark = Time.time;
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

                if ((transform.position - frisbee.transform.position).magnitude < 5) {
                    transform.position = Vector2.MoveTowards(doggo.transform.position, frisbee.transform.position, 3*Time.deltaTime);
                }
            }
        }

        // Doggo barks at sibling
        if (GlobalVariables.followPlayer) {

            if ((transform.position - sibling.transform.position).magnitude < 8) {
                Bark();
            }
        }

        void Bark(){
            if (Time.time - lastBark > delayBetweenBarks) {
                GameObject bark = Instantiate(barkPrefab, doggo.transform.position, this.gameObject.transform.rotation);
                bark.tag = "Bark";

                lastBark = Time.time;
            }
        }
    }
}
