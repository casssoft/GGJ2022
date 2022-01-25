using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Sibling : MonoBehaviour
{
    Rigidbody2D body;
    GameObject sibling;
    GameObject player;
    GameObject doggo;
    GameObject waypoint;
    public bool inFear;
    public int Anxiety;

    void Start()
    {
        waypoint = GameObject.Find("WayPointSibling");
        sibling = GameObject.Find("Sibling");
    }

    private int AnxietyLevel(float distanceToDanger) {
        if (distanceToDanger <= 4) {
            return 2;
        } else if (distanceToDanger <= 8) {
            return 1;
        } 
        return 0;
    }
	
	void Awake() {
		body = GetComponent<Rigidbody2D>();
	}

    void Update()
    {
        // following player 
        if(GlobalVariables.followPlayer) {

            player = GameObject.Find("Player");
            body.transform.position = player.transform.position;
        }

        // anxiety based on distance to enemy
        doggo = GameObject.Find("Doggo");
        Anxiety = AnxietyLevel(Vector2.Distance(doggo.transform.position, body.transform.position));

        TextMeshProUGUI ugui = this.GetComponentInChildren<TextMeshProUGUI>();

        // update character text to represent anxiety (for now)
        if (inFear){
            transform.position = Vector2.MoveTowards(sibling.transform.position, waypoint.transform.position, 3*Time.deltaTime);
            if (sibling.transform.position == waypoint.transform.position) {
                inFear = false;
            }
        }

        if (Anxiety == 2) {
            ugui.text = "NOOOO";
            GlobalVariables.followPlayer = false;
            inFear = true;
        }
        if (Anxiety == 1 && !inFear) {
            ugui.text = "Uh oh....";
        }
        if (Anxiety == 0 && !inFear) {
            ugui.text = "";
        }

    }

}
