using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Level : MonoBehaviour
{
	[SerializeField] float bpm = 1f;
	float timeBetweenBeatsInSeconds;
	[SerializeField] TextMeshProUGUI timeText; // Used for debugging remove later
	float currentBeatTime;
	[SerializeField] GameObject leftBeatVisual;
	
    // Start is called before the first frame update
    void Start()
    {
		timeBetweenBeatsInSeconds = 60 / bpm;
		currentBeatTime = timeBetweenBeatsInSeconds;
		CreateBeatVisual();
    }

    // Update is called once per frame
    void Update()
	{
		SetCurrentBeatTime();
	}

	private void SetCurrentBeatTime()
	{
		currentBeatTime -= Time.deltaTime;
		timeText.text = currentBeatTime + "";

		if (currentBeatTime <= 0) {
			currentBeatTime = timeBetweenBeatsInSeconds;
			CreateBeatVisual();
		}
	}

	public float GetCurrentBeatTime()
	{
		return currentBeatTime;
	}

	public float GetTimeBetweenBeatsInSeconds()
	{
		return timeBetweenBeatsInSeconds;
	}

	private void CreateBeatVisual()
	{
		GameObject sparkles = Instantiate(leftBeatVisual);
	}
}
