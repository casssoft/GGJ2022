using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sibling : MonoBehaviour
{
    void Start()
    {
        
    }
    Rigidbody2D body;
    GameObject player;
	
	void Awake() {
		body = GetComponent<Rigidbody2D>();
	}

    void Update()
    {
        if(GlobalVariables.followPlayer) {

            player = GameObject.Find("Player");
            body.transform.position = player.transform.position;
        }
    }

}
