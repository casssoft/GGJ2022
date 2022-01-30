using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Sibling : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject player;
    public GameObject doggo;
    public GameObject waypointleft;
    public GameObject waypointright;
    public TextMeshProUGUI siblingGUI;
    public static bool inFear = false;
    public int Anxiety;
    private Vector2 prevPos;
    private Animator anim;
    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Start() {
        prevPos = rb.transform.position;
    }

    private int AnxietyLevel(float distanceToDanger) {
        if (distanceToDanger <= 4) {
            return 2;
        } else if (distanceToDanger <= 8) {
            return 1;
        }
        return 0;
    }

    void Update() {
        // following player 
        if(GlobalVariables.followPlayer) {
            Vector2 toPlayer = player.transform.position - rb.transform.position;
            if (toPlayer.magnitude > 1)
            {
                toPlayer.Normalize();
                rb.transform.position += new Vector3(toPlayer.x, toPlayer.y, 0) * Time.deltaTime * 3.5f;
            }
        }

        // anxiety based on distance to enemy
        Anxiety = AnxietyLevel(Vector2.Distance(doggo.transform.position, rb.transform.position));

        // update character text to represent anxiety (for now)
        if (inFear) {
            GameObject waypoint = waypointleft;
            if (rb.transform.position.x > waypointleft.transform.position.x)
            {
                waypoint = waypointright;
            }
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

        // Animate this tiny kid
        Vector2 pos = rb.transform.position;
        Vector2 velocity = pos - prevPos;
        if (velocity.x == 0 && velocity.y == 0) {
            anim.Play("cry");
        } else if (Mathf.Abs(velocity.x) > Mathf.Abs(velocity.y)) {
            if (velocity.x > 0) {
                anim.Play("walkright");
            } else {
                anim.Play("walkleft");
            }
        } else {
            if (velocity.y > 0) {
                anim.Play("walkup");
            } else {
                anim.Play("walkdown");
            }
        }
        prevPos = rb.transform.position;
    }

}
