using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour
{
	private const float XSpeed = 7f;
	private const float YSpeed = 7f;
	bool hasHitObj = true;
	Rigidbody2D rb;
	[SerializeField] KeyCode rightKey = KeyCode.D;
	[SerializeField] KeyCode leftKey = KeyCode.A;
	[SerializeField] KeyCode upKey = KeyCode.W;
	[SerializeField] KeyCode downKey = KeyCode.S;

	// Start is called before the first frame update
	void Start()
    {
		rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(rightKey)) {
			hasHitObj = false;
			Debug.Log("Moving the player " + XSpeed + " to the right");
			rb.velocity = new Vector2(XSpeed, rb.velocity.y);
		}
		else if (Input.GetKeyDown(leftKey)) {
			hasHitObj = false;
			Debug.Log("Moving the player " + XSpeed + " to the left");
			rb.velocity = new Vector2(-XSpeed, rb.velocity.y);
		}
		else if (Input.GetKeyDown(upKey)) {
			hasHitObj = false;
			Debug.Log("Moving the player " + YSpeed + " to the up");
			rb.velocity = new Vector2(rb.velocity.x, YSpeed);
		}
		else if (Input.GetKeyDown(downKey)) {
			hasHitObj = false;
			Debug.Log("Moving the player " + YSpeed + " to the down");
			rb.velocity = new Vector2(rb.velocity.x, -YSpeed);
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{	
		hasHitObj = true;
	}
}
