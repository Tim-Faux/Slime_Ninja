using TMPro;
using UnityEngine;

public class Player1 : MonoBehaviour
{
	// Constants to define how fast the player moves
	private const float XSpeed = 60f;
	private const float YSpeed = 60f;
	// Constant to adjust how far the ray checks past the player bounds
	private const float wallCheckAdjustment = 0.25f;
	// Variables to keep track of player's state
	bool hasHitObj = true;
	Rigidbody2D rb;
	// Variable to keep track of the current level
	Level level;
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


	/*
	 *	Start is called before the first frame update
	 */
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		level = FindObjectOfType<Level>();
	}

	/*
	 *	Update is called once per frame
	 */
	void Update()
	{
		HandleButtonPress();
	}

	/*
	 *	Checks if a button is pressed, and if it is appropriate to move the block
	 */
	private void HandleButtonPress()
	{
		Vector3 pos = rb.transform.position;
		// Right
		if (Input.GetKeyDown(rightKey) && hasHitObj && !IsTouchingWallInDirection(pos, Vector2.right)) {
			MovePlayer1(XSpeed, 0, Vector2.right);
		}
		// Left
		else if (Input.GetKeyDown(leftKey) && hasHitObj && !IsTouchingWallInDirection(pos, Vector2.left)) {
			MovePlayer1(-XSpeed, 0, Vector2.left);
		}
		// Up
		else if (Input.GetKeyDown(upKey) && hasHitObj && !IsTouchingWallInDirection(pos, Vector2.up)) {
			MovePlayer1(0, YSpeed, Vector2.up);
		}
		// Down
		else if (Input.GetKeyDown(downKey) && hasHitObj && !IsTouchingWallInDirection(pos, Vector2.down)) {
			MovePlayer1(0, -YSpeed, Vector2.down);
		}
	}

	/*
	 *	Checks if there is a collision hitbox in the direction given
	 */
	private bool IsTouchingWallInDirection(Vector3 pos, Vector2 direction)
	{
		int layerMask = ~(1 << 6);
		float raySize = GetRaySize(direction);
		Physics2D.queriesHitTriggers = false;
		bool results = Physics2D.Raycast(pos, direction, raySize, layerMask);
		return results;
	}

	/*
	 *	Finds the ray size based on the size of the sprite used
	 */
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

	/*
	 *	Moves the player in a direction based on the given speeds
	 */
	private void MovePlayer1(float HorSpeed, float VertSpeed, Vector2 direction)
	{
		bool hitBeat = CheckButtonPressTiming();
		if (hitBeat) {
			// Get everything but the player
			var layerMask =~ LayerMask.GetMask(LayerMask.LayerToName(6));
			RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, float.MaxValue, layerMask);
			// Check if there is an object in the direction and move the player to that object
			if (hit.collider != null) {
				if (direction == Vector2.right) {
					var newPos = new Vector2(hit.point.x - hit.collider.bounds.extents.x, rb.transform.position.y);
					rb.MovePosition(newPos);
				}
				else if (direction == Vector2.left) {
					var newPos = new Vector2(hit.point.x + hit.collider.bounds.extents.x, rb.transform.position.y);
					rb.MovePosition(newPos);
				}
				else if (direction == Vector2.up) {
					var newPos = new Vector2(rb.transform.position.x, hit.point.y - hit.collider.bounds.extents.y);
					rb.MovePosition(newPos);
				}
				else if (direction == Vector2.down) {
					var newPos = new Vector2(rb.transform.position.x, hit.point.y + hit.collider.bounds.extents.y);
					rb.MovePosition(newPos);
				}
				// Check if what was hit was a block obstical and handle the hit
				Block hitBlock;
				hit.collider.TryGetComponent(out hitBlock);
				if (hitBlock != null) {
					var blockBroke = hitBlock.HandleHit();
					// If the block breaks continue player's movement
					if (blockBroke) {
						MovePlayer1(HorSpeed, VertSpeed, direction);
					}
				}
			}
		}
	}

	/*
	 *	Checks how close the timing for the next or previous beat is to 0
	 */
	private bool CheckButtonPressTiming()
	{
		float pressedButtonTime = level.GetCurrentBeatTime();
		float timeBetweenBeatsInSeconds = level.GetTimeBetweenBeatsInSeconds();

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

	/*
	 *	Tells the program the the player has collided with a wall and should be allowed to move
	 */
	private void OnCollisionEnter2D(Collision2D collision)
	{
		hasHitObj = true;
		Debug.Log("The player has stoped");
	}
}
