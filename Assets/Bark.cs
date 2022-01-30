using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bark : MonoBehaviour
{

    public float speed = 2f;
    public Rigidbody2D rb;
    public Collider2D collider;
    public bool aim = false;

    // Start is called before the first frame update
    void Start()
    {
        float direction = Random.Range(0f,1f);
        // only the dog has aim bullets 
        if (aim) {
            rb.velocity = (GameObject.Find("Sibling").transform.position - transform.position).normalized * speed;
        } else {
            if (direction < 0.5f) {
                rb.velocity = Vector2.left;
            } else {
                rb.velocity = Vector2.right;
            }
        }
        
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
