using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    }

    private void OnTriggerStay2D(Collider2D other) {
        if (other)
        {
            if (Input.GetKeyDown(KeyCode.E)) {
                GlobalVariables.followPlayer = !GlobalVariables.followPlayer;
            }
        }
    }
}
