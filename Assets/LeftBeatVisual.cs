using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftBeatVisual : MonoBehaviour
{
	private const float TimeBetweenBeatVisualMovement = 0.001f;
	float beatSpeed;

    // Start is called before the first frame update
    void Start()
    {
		float timeBetweenBeatsInSeconds = FindObjectOfType<Level>().GetTimeBetweenBeatsInSeconds();
		beatSpeed = transform.position.x / timeBetweenBeatsInSeconds * -1;
		StartCoroutine(MoveRight());
	}

    // Update is called once per frame
    void Update()
    {
		
    }

	private IEnumerator MoveRight()
	{
		transform.Translate(new Vector2(beatSpeed * Time.deltaTime, 0));
		if(transform.position.x >= 0) {
			DestroyBeat();
		}
		yield return new WaitForSeconds(TimeBetweenBeatVisualMovement);
		StartCoroutine(MoveRight());
	}

	private void DestroyBeat()
	{
		Destroy(gameObject);
	}
}
