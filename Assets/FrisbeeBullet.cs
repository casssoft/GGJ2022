using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrisbeeBullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    public Collider2D collider;
    public Vector2 frisbeePickupVector = new Vector2(0.0f, 0.0f);
    public Vector2 direction;
    public AudioSource audio;
    public AudioClip[] audioClips;
    
    void Start()
    {
        rb.velocity = direction * speed;
        audio = GetComponent<AudioSource>();
    }

    void Update() {
        // Allow Player to pickup the object once it has slowed down
        if (!collider.isTrigger && (rb.velocity - frisbeePickupVector).magnitude < 1) {
            collider.isTrigger = true;

            if (audioClips.Length > 0) {
                audio.PlayOneShot(RandomLandClip());
            }
            
        }
    }

    AudioClip RandomLandClip(){
        return audioClips[Random.Range(0, 2)];
    }
}
