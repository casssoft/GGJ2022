using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class Mom : MonoBehaviour
{
    public Flowchart flowchart;
    private bool firstChat = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Sibling") {
            GlobalVariables.siblingSaved = true;
            GlobalVariables.followPlayer = true;

            flowchart.ExecuteBlock("Sibling Saved");
        }

        if (collision.gameObject.name == "Player" && !firstChat) {
            firstChat = true;

            flowchart.ExecuteBlock("Sibling Missing");
        }
    }
}
