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
    public TextMeshProUGUI ugui;

    void Start()
    {
        doggo = GameObject.Find("Doggo");
        sibling = GameObject.Find("Sibling");
        waypoint = GameObject.Find("WayPointDoggo");
        lastBark = Time.time;
        ugui = this.GetComponentInChildren<TextMeshProUGUI>();
    }

    void Update()
    {
        // Dog is watches and chases frisbee
        GameObject[] frisbees = GameObject.FindGameObjectsWithTag("Frisbee");
        if (frisbees.Length > 0) {
            GameObject frisbee = frisbees[0];

            float distanceBetweenDogAndFrisbee = (transform.position - frisbee.transform.position).magnitude;

            if (distanceBetweenDogAndFrisbee < 5) {
                transform.position = Vector2.MoveTowards(doggo.transform.position, frisbee.transform.position, 3*Time.deltaTime);
            }

            if (distanceBetweenDogAndFrisbee == 0){
                ugui.text = "Yip Yip!";
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
