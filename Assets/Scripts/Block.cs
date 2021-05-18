using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
	int timesHit = 0;
	[SerializeField] Color hitColor = new Color(0, 0, 0, 1f);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (tag == "Breakable") {
			HandleHit();
		}
	}

	private void HandleHit()
	{
		timesHit++;
		int maxHits = 2;
		if (timesHit >= maxHits) {
			DestroyBlock();
		}
		else {
			ChangeColor();
		}
	}

	private void DestroyBlock()
	{
		//PlayBlockDestroySFX();
		Destroy(gameObject);
		//level.OnBlockBreak();
		//TriggerSparklesVFX();
	}

	private void ChangeColor()
	{
		Debug.Log("Changing block color");
		SpriteRenderer renderer = GetComponent<SpriteRenderer>();
		renderer.color = hitColor;
	}

}
