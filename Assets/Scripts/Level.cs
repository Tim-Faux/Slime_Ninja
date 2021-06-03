using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Level : MonoBehaviour
{
	[SerializeField] float bpm = 1f;
	float timeBetweenBeatsInSeconds;
	[SerializeField] TextMeshProUGUI timeText;
	float currentBeatTime;
	
    // Start is called before the first frame update
    void Start()
    {
		timeBetweenBeatsInSeconds = 60 / bpm;
		currentBeatTime = timeBetweenBeatsInSeconds;
    }

    // Update is called once per frame
    void Update()
    {
		currentBeatTime -= Time.deltaTime;
		timeText.text = currentBeatTime + "";

		if(currentBeatTime <= 0) {
			currentBeatTime = timeBetweenBeatsInSeconds;
		}
	}
}
