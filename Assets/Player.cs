using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Fungus;

public class Player : MonoBehaviour
{
	public Rigidbody2D body;
    public GameObject frisbeePrefab;
    public Animator anim;
    public Flowchart flowchart;
    public static bool hasFrisbee;
    public static bool playerIsNearSibling;


    void Update()
    {   
        // Frisbee throwing behavior 
        if (Input.GetButtonDown("Fire1") && hasFrisbee) {
            Shoot();
        }


        Vector2 playerInput;
		playerInput.x = Input.GetAxis("Horizontal");
		playerInput.y = Input.GetAxis("Vertical");
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
            GlobalVariables.followPlayer = !GlobalVariables.followPlayer;
            flowchart.ExecuteBlock("First dark mode");

            // Turn off the text if you just grabbed ur sibling
            // Turn on text if you just let them go
            // TextMeshProUGUI ugui = GlobalVariables.sibling.GetComponentInChildren<TextMeshProUGUI>();
            // ugui.enabled = !GlobalVariables.followPlayer;
        }
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Sibling")
        {
            playerIsNearSibling = true;
            // TextMeshProUGUI ugui = collision.GetComponentInChildren<TextMeshProUGUI>();
            // ugui.enabled = true;

            flowchart.ExecuteBlock("Sibling First Chat");
        }

        if (collision.gameObject.tag == "Frisbee") {
            hasFrisbee = true;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.name == "Doggo" && hasFrisbee == true) {
            GlobalVariables.gaveFrisbeeToDoggo = true;
        } else if (collision.gameObject.name == "Doggo") {
            flowchart.ExecuteBlock("Doggo");
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Sibling")
        {
            playerIsNearSibling = false;
            // TextMeshProUGUI ugui = collision.GetComponentInChildren<TextMeshProUGUI>();
            // ugui.enabled = false;
        }
    }

    void Shoot() {
        GameObject frisbee = Instantiate(frisbeePrefab, body.position, this.gameObject.transform.rotation);
        frisbee.tag = "Frisbee";
        hasFrisbee = false;
    }
}
