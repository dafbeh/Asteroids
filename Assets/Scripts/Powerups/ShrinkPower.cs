using System;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/ShrinkPower")]
public class ShrinkPower : PowerUpEffect
{
	private Vector3 originalSize;
	private Vector3 newSize = new Vector3(0.15f, 0.15f, 0.15f);
	private GameObject player;
	private Color green = new Color(97, 195, 0);
	private IAudioSystem audioSystem;

    public override void Apply(GameObject gameObject) {
		originalSize = gameObject.transform.localScale;
		player = gameObject;

		player.transform.localScale = newSize;
		player.GetComponentInChildren<SpriteRenderer>().color = green;

        audioSystem = ServiceLocator.Get<IAudioSystem>();
		audioSystem.PlaySound("Sounds/wind");

        PlayerHealth.OnPlayerDeath += deathReset;
		gameObject.GetComponent<MonoBehaviour>().StartCoroutine(resetPower());
    }

	private IEnumerator resetPower() 
	{
		yield return new WaitForSeconds(10f);

		player.transform.localScale = originalSize;
        player.GetComponent<SpriteRenderer>().color = Color.white;
	}

	private void deathReset() 
    {
		player.transform.localScale = originalSize;
        PlayerHealth.OnPlayerDeath -= deathReset;
        player.GetComponent<SpriteRenderer>().color = Color.white;
    }
}