using System.Collections;
using UnityEngine;

public class Player1 : MonoBehaviour
{
	private const float XSpeed = 14f;
	private const float YSpeed = 14f;
	private const float wallCheckAdjustment = 0.25f;
	bool hasHitObj = true;
	Rigidbody2D rb;
	[SerializeField] KeyCode rightKey = KeyCode.D;
	[SerializeField] KeyCode leftKey = KeyCode.A;
	[SerializeField] KeyCode upKey = KeyCode.W;
	[SerializeField] KeyCode downKey = KeyCode.S;
	Vector2 prevPos;

	// Start is called before the first frame update
	void Start()
    {
		rb = GetComponent<Rigidbody2D>();
		prevPos = rb.transform.position;
    }

	// Update is called once per frame
	void Update()
	{
		//if (!isMoving()) {
			HandleButtonPress();
		//}
	}

	private void HandleButtonPress()
	{
		Vector3 pos = rb.transform.position;
		if (Input.GetKeyDown(rightKey) && hasHitObj && !IsTouchingWallInDirection(pos, Vector2.right)) {
			hasHitObj = false;
			MovePlayer1(XSpeed, 0);
		}
		else if (Input.GetKeyDown(leftKey) && hasHitObj && !IsTouchingWallInDirection(pos, Vector2.left)) {
			hasHitObj = false;
			MovePlayer1(-XSpeed, 0);
		}
		else if (Input.GetKeyDown(upKey) && hasHitObj && !IsTouchingWallInDirection(pos, Vector2.up)) {
			hasHitObj = false;
			MovePlayer1(0, YSpeed);
		}
		else if (Input.GetKeyDown(downKey) && hasHitObj && !IsTouchingWallInDirection(pos, Vector2.down)) {
			hasHitObj = false;
			MovePlayer1(0, -YSpeed);
		}
	}

	private bool IsTouchingWallInDirection(Vector3 pos, Vector2 direction)
	{
		int layerMask = 1 << 6;
		layerMask = ~layerMask;
		float raySize = 0;
		if (direction.x != 0) {
			Debug.Log("Checking for wall in the X direction");
			raySize = (GetComponent<SpriteRenderer>().bounds.size.x / 2) + wallCheckAdjustment;
			Debug.Log(raySize);
		}
		else if (direction.y != 0) {
			Debug.Log("Checking for wall in the Y direction");
			raySize = (GetComponent<SpriteRenderer>().bounds.size.y / 2) + wallCheckAdjustment;
			Debug.Log(raySize);
		}
		
		Physics2D.queriesHitTriggers = false;
		bool results = Physics2D.Raycast(pos, direction, raySize, layerMask);
		Debug.DrawRay(pos, direction, Color.black, 0.5f);
		Debug.Log(results);
		return results;
	}

	private void MovePlayer1(float HorSpeed, float VertSpeed)
	{
		Debug.Log("Moving the player " + HorSpeed + " right and " + VertSpeed + " up");
		rb.velocity = new Vector2(HorSpeed, VertSpeed);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		hasHitObj = true;
		//Wait(0.5f);
		//prevPos = rb.transform.position;
		Debug.Log("The player has stoped");
	}

	private IEnumerator Wait(float time)
	{
		yield return new WaitForSeconds(time);
	}

	private bool isMoving()
	{
		Vector2 curPos = rb.transform.position;
		Debug.Log("prevPos: " + prevPos.x + ", " + prevPos.y);
		Debug.Log("curPos: " + curPos.x + ", " + curPos.y);
		if (prevPos.x == curPos.x && prevPos.y == curPos.y) {
			return false;
		}
		prevPos = curPos;
		return true;
	}

	private Vector2 Round(Vector2 value, int digits)
	{
		float mult = Mathf.Pow(10.0f, digits);
		float x = Mathf.Round(value.x * mult) / mult;
		float y = Mathf.Round(value.y * mult) / mult;
		Vector2 roundedVector = new Vector2(x, y);
		return roundedVector;
	}
}
