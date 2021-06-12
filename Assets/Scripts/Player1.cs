using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Player1 : MonoBehaviour
{
	// Constants to define how fast the player moves
	private const float XSpeed = 14f;
	private const float YSpeed = 14f;
	// Constant to adjust how far the ray checks past the player bounds
	private const float wallCheckAdjustment = 0.25f;
	// Variables to keep track of player's state
	bool hasHitObj = true;
	Rigidbody2D rb;
	// Keyboard variables
	[SerializeField] KeyCode rightKey = KeyCode.D;
	[SerializeField] KeyCode leftKey = KeyCode.A;
	[SerializeField] KeyCode upKey = KeyCode.W;
	[SerializeField] KeyCode downKey = KeyCode.S;
	// Timing variables
	[SerializeField] float perfectTiming;
	[SerializeField] float goodTiming;
	[SerializeField] float badTiming;
	// refereance to the textbox for the input feedback
	[SerializeField] TextMeshProUGUI timeText;


	// Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update()
	{
		HandleButtonPress();
	}

	// Checks if a button is pressed, and if it is appropriate to move the block
	private void HandleButtonPress()
	{
		Vector3 pos = rb.transform.position;
		if (Input.GetKeyDown(rightKey) && hasHitObj && !IsTouchingWallInDirection(pos, Vector2.right)) {
			MovePlayer1(XSpeed, 0);
		}
		else if (Input.GetKeyDown(leftKey) && hasHitObj && !IsTouchingWallInDirection(pos, Vector2.left)) {
			MovePlayer1(-XSpeed, 0);
		}
		else if (Input.GetKeyDown(upKey) && hasHitObj && !IsTouchingWallInDirection(pos, Vector2.up)) {
			MovePlayer1(0, YSpeed);
		}
		else if (Input.GetKeyDown(downKey) && hasHitObj && !IsTouchingWallInDirection(pos, Vector2.down)) {
			MovePlayer1(0, -YSpeed);
		}
	}

	// Checks if there is a collision hitbox in the direction given
	private bool IsTouchingWallInDirection(Vector3 pos, Vector2 direction)
	{
		int layerMask = ~(1 << 6);
		float raySize = GetRaySize(direction);
		Physics2D.queriesHitTriggers = false;
		bool results = Physics2D.Raycast(pos, direction, raySize, layerMask);
		return results;
	}

	// Finds the ray size based on the size of the sprite used
	private float GetRaySize(Vector2 direction)
	{
		float raySize = 0;
		if (direction.x != 0) {
			Debug.Log("Checking for wall in the X direction");
			raySize = (GetComponent<SpriteRenderer>().bounds.size.x / 2) + wallCheckAdjustment;
		}
		else if (direction.y != 0) {
			Debug.Log("Checking for wall in the Y direction");
			raySize = (GetComponent<SpriteRenderer>().bounds.size.y / 2) + wallCheckAdjustment;
		}

		return raySize;
	}

	// Moves the player in a direction based on the given speeds
	private void MovePlayer1(float HorSpeed, float VertSpeed)
	{
		bool hitBeat = CheckButtonPressTiming();
		if (hitBeat) {
			hasHitObj = false;
			Debug.Log("Moving the player " + HorSpeed + " right and " + VertSpeed + " up");
			rb.velocity = new Vector2(HorSpeed, VertSpeed);
		}
	}


	//TODO This doesnt make much sense right now with how the indicator is working
	private bool CheckButtonPressTiming()
	{
		float pressedButtonTime = FindObjectOfType<Level>().GetCurrentBeatTime();
		float timeBetweenBeatsInSeconds = FindObjectOfType<Level>().GetTimeBetweenBeatsInSeconds();

		if (pressedButtonTime <= perfectTiming || timeBetweenBeatsInSeconds - pressedButtonTime <= perfectTiming) {
			timeText.text = "Perfect";
		}
		else if(pressedButtonTime <= goodTiming || timeBetweenBeatsInSeconds - pressedButtonTime <= goodTiming) {
			timeText.text = "Good";
		}
		else if (pressedButtonTime <= badTiming || timeBetweenBeatsInSeconds - pressedButtonTime <= badTiming) {
			timeText.text = "Bad";
		}
		else {
			timeText.text = "Miss";
			return false;
		}
		return true;
	}

	// Tells the program the the player has collided with a wall and should be allowed to move
	private void OnCollisionEnter2D(Collision2D collision)
	{
		hasHitObj = true;
		Debug.Log("The player has stoped");
	}

	
}
