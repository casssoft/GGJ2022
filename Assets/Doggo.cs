using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Doggo : MonoBehaviour
{
    GameObject doggo;
    GameObject waypoint;
    public Bark barkPrefab;
    GameObject sibling;
    public float delayBetweenBarks = 3;
    float lastBark;
    public TextMeshProUGUI ugui;
    public Animator anim;
    private Vector2 prevPos; // for calculating animation
    public float frisbeeRadius = 10;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        doggo = GameObject.Find("Doggo");
        sibling = GameObject.Find("Sibling");
        waypoint = GameObject.Find("WayPointDoggo");
        lastBark = Time.time;
        ugui = this.GetComponentInChildren<TextMeshProUGUI>();
        prevPos = transform.position;
    }

    void Update()
    {
        // Dog is watches and chases frisbee
        GameObject[] frisbees = GameObject.FindGameObjectsWithTag("Frisbee");
        if (frisbees.Length > 0) {
            GameObject frisbee = frisbees[0];

            float distanceBetweenDogAndFrisbee = (transform.position - frisbee.transform.position).magnitude;

            if (distanceBetweenDogAndFrisbee < frisbeeRadius) {
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

    void Bark(){
        if (Time.time - lastBark > delayBetweenBarks) {
            Bark bark = Instantiate<Bark>(barkPrefab, doggo.transform.position, this.gameObject.transform.rotation);
            lastBark = Time.time;
        }
    }
}
