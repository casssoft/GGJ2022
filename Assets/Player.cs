using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Fungus;

public class Player : MonoBehaviour
{
	public Rigidbody2D body;
    public FrisbeeBullet frisbeePrefab;
    public Animator anim;
    public Flowchart flowchart;
    public static bool hasFrisbee;
    public static bool playerIsNearSibling;
    private Vector2 lastInput = new Vector2(1, 0);
    public AudioSource audio;
    public AudioClip[] audioClips;

    void Start() {
        audio = GetComponent<AudioSource>();
    }


    void Update()
    {
        Vector2 playerInput;
        playerInput.x = Input.GetAxis("Horizontal");
        playerInput.y = Input.GetAxis("Vertical");

        if (playerInput.magnitude > 0.1)
        {
            lastInput = playerInput;
        }

        // Frisbee throwing behavior 
        if (Input.GetButtonDown("Fire1") && hasFrisbee) {
            Shoot();
        }

		playerInput = Vector2.ClampMagnitude(playerInput, 1f);
        body.velocity = playerInput * 5;
        //body.MovePosition(body.position + playerInput * 50 * Time.deltaTime);

        // Control animations
        this.gameObject.transform.localScale = new Vector3(1, 1, 1);
        if (playerInput.magnitude < 0.1)
        {
            anim.SetBool("WalkHorizontal", false);
            anim.SetBool("WalkVertical", false);
        } else if (Mathf.Abs(playerInput.x) > Mathf.Abs(playerInput.y))
        {
            if (playerInput.x < -0.01f)
            {
                // Flip the sprite if we are walking to the left
                this.gameObject.transform.localScale = new Vector3(-1, 1, 1);
            }

            anim.SetBool("WalkHorizontal", true);
            anim.SetBool("WalkVertical", false);
        } else
        {
            anim.SetBool("WalkHorizontal", false);
            anim.SetBool("WalkVertical", true);

        }
        anim.SetFloat("Vertical", playerInput.y);

        // Handle interactions
        if (Input.GetKeyDown(KeyCode.E) && playerIsNearSibling)
        {
            if (GlobalVariables.followPlayer) {
                audio.PlayOneShot(audioClips[0]);
            } else {
                audio.PlayOneShot(audioClips[1]);
            }
            GlobalVariables.followPlayer = !GlobalVariables.followPlayer;
            flowchart.ExecuteBlock("First dark mode");
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Sibling")
        {
            playerIsNearSibling = true;

            flowchart.ExecuteBlock("Sibling First Chat");
        }

        if (collision.gameObject.tag == "Frisbee") {
            audio.PlayOneShot(audioClips[5]);
            hasFrisbee = true;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.name == "Doggo") {
            flowchart.ExecuteBlock("Doggo");
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Sibling")
        {
            playerIsNearSibling = false;
            // TextMeshProUGUI ugui = collision.GetComponentInChildren<TextMeshProUGUI>();
            // ugui.enabled = false;
        }
    }

    void Shoot() {

        // var name is frisebee
        audio.PlayOneShot(RandomThrowClip());
        FrisbeeBullet frisebee = Instantiate<FrisbeeBullet>(frisbeePrefab, body.position + lastInput.normalized, this.gameObject.transform.rotation);
        frisebee.tag = "Frisbee";
        frisebee.direction = lastInput.normalized;
        hasFrisbee = false;
    }

    AudioClip RandomThrowClip(){
        return audioClips[Random.Range(2, 4)];
    }
}
