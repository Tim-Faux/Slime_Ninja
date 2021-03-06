using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
	int timesHit = 0;
	[SerializeField] Color hitColor = new Color(0, 0, 0, 1f);
	[SerializeField] AudioClip breakSound;
	[SerializeField] AudioClip damageSound;

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnTriggerEnter2D(Collider2D collision)
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
			PlayDamageSFX();
			ChangeColor();
		}
	}

	private void DestroyBlock()
	{
		PlayBlockDestroySFX();
		Destroy(gameObject);
	}

	private void PlayBlockDestroySFX()
	{
		AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
	}

	private void PlayDamageSFX()
	{
		AudioSource.PlayClipAtPoint(damageSound, Camera.main.transform.position);
	}

	private void ChangeColor()
	{
		Debug.Log("Changing block color");
		SpriteRenderer renderer = GetComponent<SpriteRenderer>();
		renderer.color = hitColor;
	}
}
