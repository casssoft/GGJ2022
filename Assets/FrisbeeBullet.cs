using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrisbeeBullet : MonoBehaviour
{

    public float speed = 20f;
    public Rigidbody2D rb;
    public static float cooldown;
    public Collider2D collider;
    
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    void Update() {
        cooldown -= Time.deltaTime;

        if (cooldown < 0) {
            collider.isTrigger = true;
        }
    }
}
