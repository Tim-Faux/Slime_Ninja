using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Level : MonoBehaviour
{
	// Speed of the beats, set in unity
	[SerializeField] float bpm = 1f;
	// The object used as a visual representation of the beat and when to hit it
	[SerializeField] GameObject leftBeatVisual;
	// The amount of time until the beat is expected to be hit
	float currentBeatTime;
	// The total time between each beat of the song, based on the bpm
	float timeBetweenBeatsInSeconds;

	[SerializeField] TextMeshProUGUI timeText; // Used for debugging remove later


	// Start is called before the first frame update
	void Start()
    {
		timeBetweenBeatsInSeconds = 60 / bpm;
		ResetBeatTime();
    }

    // Update is called once per frame
    void Update()
	{
		SetCurrentBeatTime();
	}

	// Sets the amount of time left until the beat
	private void SetCurrentBeatTime()
	{
		currentBeatTime -= Time.deltaTime;
		timeText.text = currentBeatTime + "";

		if (currentBeatTime <= 0) {
			ResetBeatTime();
		}
	}

	// Sets the currentBeatTime to it's max value and creates a visual to indicate its timing
	private void ResetBeatTime()
	{
		currentBeatTime = timeBetweenBeatsInSeconds;
		CreateBeatVisual();
	}

	public float GetCurrentBeatTime()
	{
		return currentBeatTime;
	}

	public float GetTimeBetweenBeatsInSeconds()
	{
		return timeBetweenBeatsInSeconds;
	}

	// Creates a visual indication of the beat to show when you should hit it
	private void CreateBeatVisual()
	{
		GameObject leftBeat = Instantiate(leftBeatVisual);
		leftBeat.SendMessage("SetBeatSpeed", timeBetweenBeatsInSeconds);
	}
}
