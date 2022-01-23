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
	
	void Awake() {
		body = GetComponent<Rigidbody2D>();
	}

    void Update()
    {
        Vector2 playerInput;
		playerInput.x = Input.GetAxis("Horizontal");
		playerInput.y = Input.GetAxis("Vertical");
		playerInput = Vector2.ClampMagnitude(playerInput, 1f);
		body.MovePosition(body.position + playerInput * 20 * Time.deltaTime);
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
