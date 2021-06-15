using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftBeatVisual : MonoBehaviour
{
	private const float TimeBetweenBeatVisualMovement = 0.001f;
	float beatSpeed;

	// Sets the speed the beats should move at on the screen
	public void SetBeatSpeed(float beatTiming)
	{
		float timeBetweenBeatsInSeconds = beatTiming;
		beatSpeed = transform.position.x / timeBetweenBeatsInSeconds * -1;
	}

    // Start is called before the first frame update
    void Start()
    {
		StartCoroutine(MoveRight());
	}

	// Moves the beat indicator to the right
	private IEnumerator MoveRight()
	{
		Vector2 beatMovement = new Vector2(beatSpeed * Time.deltaTime, 0);
		transform.Translate(beatMovement);
		if(transform.position.x >= 0) {
			DestroyBeat();
		}
		yield return new WaitForSeconds(TimeBetweenBeatVisualMovement);
		StartCoroutine(MoveRight());
	}

	// Destroys the current beat instance
	private void DestroyBeat()
	{
		Destroy(gameObject);
	}
}
