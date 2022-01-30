using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bark : MonoBehaviour
{

    public float speed = 20f;
    public Rigidbody2D rb;
    public Collider2D collider;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = (GameObject.Find("Sibling").transform.position - transform.position).normalized * speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Sibling" || collision.gameObject.name == "Player") {
            GlobalVariables.followPlayer = false;
            Sibling.inFear = true;
            Destroy(gameObject);
        }
    }
}
