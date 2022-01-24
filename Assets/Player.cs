using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
	Rigidbody2D body;
    public Animator anim;

    void Awake() {
		body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
	}

    void Update()
    {
        Vector2 playerInput;
		playerInput.x = Input.GetAxis("Horizontal");
		playerInput.y = Input.GetAxis("Vertical");
		playerInput = Vector2.ClampMagnitude(playerInput, 1f);
        body.velocity = playerInput * 5;
        //body.MovePosition(body.position + playerInput * 50 * Time.deltaTime);
        anim.SetFloat("Horizontal", playerInput.x);
        anim.SetFloat("Vertical", playerInput.y);
        anim.SetFloat("SpeedX", Mathf.Abs(playerInput.x));
        anim.SetFloat("SpeedY", Mathf.Abs(playerInput.y));
        if (playerInput.x < -0.01f)
        {
            this.gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            this.gameObject.transform.localScale = new Vector3(1, 1, 1);
        }

        if (Input.GetKeyDown(KeyCode.E) && GlobalVariables.playerIsNearSibling)
        {
            GlobalVariables.followPlayer = !GlobalVariables.followPlayer;
            // Turn off the text if you just grabbed ur sibling
            // Turn on text if you just let them go
            TextMeshProUGUI ugui = GlobalVariables.sibling.GetComponentInChildren<TextMeshProUGUI>();
            ugui.enabled = !GlobalVariables.followPlayer;
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Sibling")
        {
            GlobalVariables.playerIsNearSibling = true;
            TextMeshProUGUI ugui = collision.GetComponentInChildren<TextMeshProUGUI>();
            ugui.enabled = true;
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Sibling")
        {
            GlobalVariables.playerIsNearSibling = false;
            TextMeshProUGUI ugui = collision.GetComponentInChildren<TextMeshProUGUI>();
            ugui.enabled = false;
        }
    }
}
