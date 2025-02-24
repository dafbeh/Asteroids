using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/ShrinkPower")]
public class ShrinkPower : PowerUpEffect
{
	private Vector3 originalSize;
	private Vector3 newSize = new Vector3(0.15f, 0.15f, 0.15f);
	private GameObject player;

    public override void Apply(GameObject gameObject) {
		originalSize = gameObject.transform.localScale;
		player = gameObject;

		player.transform.localScale = newSize;

        PlayerHealth.OnPlayerDeath += deathReset;
		gameObject.GetComponent<MonoBehaviour>().StartCoroutine(resetPower());
    }

	private IEnumerator resetPower() 
	{
		yield return new WaitForSeconds(10f);

		player.transform.localScale = originalSize;
	}

	private void deathReset() 
    {
		player.transform.localScale = originalSize;
        PlayerHealth.OnPlayerDeath -= deathReset;
    }
}