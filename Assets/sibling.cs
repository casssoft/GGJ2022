using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class sibling : MonoBehaviour
{
    void Start()
    {
        
    }
    Rigidbody2D body;
    GameObject player;
    GameObject doggo;
    public int Anxiety;

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
        if (Anxiety == 2) {
            ugui.text = "NOOOO";
            GlobalVariables.followPlayer = false;

            // TODO: move the sibling back to a starting location

        }
        if (Anxiety == 1) {
            ugui.text = "Uh oh....";
        }
        if (Anxiety == 0) {
            ugui.text = "";
        }

    }

}
