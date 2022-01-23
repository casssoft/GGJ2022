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
	

    // Update is called once per frame
    void Update()
    {
        Vector2 playerInput;
		playerInput.x = Input.GetAxis("Horizontal");
		playerInput.y = Input.GetAxis("Vertical");
		playerInput = Vector2.ClampMagnitude(playerInput, 1f);	
		        Debug.Log("Hello: " + playerInput);
		body.MovePosition(body.position + playerInput * 100* Time.deltaTime);

					
    }
}
