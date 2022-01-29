using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrisbeeBullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    public Collider2D collider;
    public Vector2 frisbeePickupVector = new Vector2(0.0f, 0.0f);
    
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    void Update() {
        // Allow Player to pickup the object once it has slowed down
        if ((rb.velocity - frisbeePickupVector).magnitude < 1 ) {
            collider.isTrigger = true;
        }
    }
}
