using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftBeatVisual : MonoBehaviour
{
	const float beatSpeed = 0.005f;

    // Start is called before the first frame update
    void Start()
    {
		
	}

    // Update is called once per frame
    void Update()
    {
		MoveRight();
    }

	private void MoveRight()
	{
		transform.Translate(new Vector2(beatSpeed, 0));
		if(transform.position.x >= 0) {
			DestroyBeat();
		}
	}

	private void DestroyBeat()
	{
		Destroy(gameObject);
	}
}
