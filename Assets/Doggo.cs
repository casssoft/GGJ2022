using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Doggo : MonoBehaviour
{
    GameObject doggo;
    GameObject waypoint;
    public Animator anim;
    private Vector2 prevPos; // for calculating animation
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        doggo = GameObject.Find("Doggo");
        waypoint = GameObject.Find("WayPointDoggo");
        prevPos = transform.position;
    }

    void Update()
    {
        TextMeshProUGUI ugui = this.GetComponentInChildren<TextMeshProUGUI>();

        if (GlobalVariables.gaveFrisbeeToDoggo) {
            ugui.text = "Yip Yip!";

            transform.position = Vector2.MoveTowards(doggo.transform.position, waypoint.transform.position, 2*Time.deltaTime);
        }
        Vector2 pos = transform.position;
        Vector2 velocity = pos - prevPos;
        if (velocity.x == 0 && velocity.y == 0)
        {
            anim.Play("idle");
        } else if (Mathf.Abs(velocity.x) > Mathf.Abs(velocity.y))
        {
            if (velocity.x > 0)
            {
                anim.Play("walkright");
            } else
            {
                anim.Play("walkleft");
            }
        } else
        {
            if (velocity.y > 0)
            {
                anim.Play("walkup");
            } else
            {
                anim.Play("walkdown");
            }
        }
        prevPos = transform.position;
    }
}
