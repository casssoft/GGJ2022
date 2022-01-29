using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Sibling : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject player;
    public GameObject doggo;
    public GameObject waypoint;
    public TextMeshProUGUI siblingGUI;
    public bool inFear = false;
    public int Anxiety;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
    }

    private int AnxietyLevel(float distanceToDanger) {
        if (distanceToDanger <= 4) {
            return 2;
        } else if (distanceToDanger <= 8) {
            return 1;
        } 
        return 0;
    }

    void Update()
    {
        // following player 
        if(GlobalVariables.followPlayer) {
            rb.transform.position = player.transform.position;
        }

        // anxiety based on distance to enemy
        Anxiety = AnxietyLevel(Vector2.Distance(doggo.transform.position, rb.transform.position));

        // update character text to represent anxiety (for now)
        if (inFear){
            transform.position = Vector2.MoveTowards(rb.transform.position, waypoint.transform.position, 3*Time.deltaTime);
            if (rb.transform.position == waypoint.transform.position) {
                inFear = false;
            }
        }

        if (Anxiety == 2) {
            siblingGUI.text = "NOOOO";
            GlobalVariables.followPlayer = false;
            inFear = true;
        }
        if (Anxiety == 1 && !inFear) {
            siblingGUI.text = "Uh oh....";
        }
        if (Anxiety == 0 && !inFear) {
            siblingGUI.text = "";
        }

    }

}
