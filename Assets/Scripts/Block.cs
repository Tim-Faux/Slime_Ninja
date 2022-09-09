using UnityEngine;

public class Block : MonoBehaviour
{
	int timesHit = 0;
	[SerializeField] Color hitColor = new Color(0, 0, 0, 1f);
	[SerializeField] AudioClip breakSound;
	[SerializeField] AudioClip damageSound;

	/*
	 *	Handles when the player hits the block
	 */
	public bool HandleHit()
	{
		// checks if the block should break and destroys it
		if (tag == "Breakable") {
			timesHit++;
			int maxHits = 2;
			if (timesHit >= maxHits) {
				DestroyBlock();
				return true;
			}
			else {
				PlayDamageSFX();
				ChangeColor();
				return false;
			}
		}
		return false;
	}

	/*
	 *	Removes the breakable block and plays sound effect
	 */
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

	/*
	 *	changes block's color once hit
	 */
	private void ChangeColor()
	{
		Debug.Log("Changing block color");
		SpriteRenderer renderer = GetComponent<SpriteRenderer>();
		renderer.color = hitColor;
	}
}
