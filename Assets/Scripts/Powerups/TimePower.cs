using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Powerups/TimePower")]
public class TimePower : PowerUpEffect
{
    private GameObject player;
    private Color purple = new Color(49, 0, 123);
    private IAudioSystem audioSystem;

    public override void Apply(GameObject gameObject) {
        Time.timeScale = 0.5f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        player = gameObject;

        audioSystem = ServiceLocator.Get<IAudioSystem>();
        audioSystem.PlaySound("Sounds/time");

        player.GetComponent<SpriteRenderer>().color = purple;
        PlayerHealth.OnPlayerDeath += deathReset;
        gameObject.GetComponent<MonoBehaviour>().StartCoroutine(resetPower());
    }

    private IEnumerator resetPower() 
	{
		yield return new WaitForSeconds(5f);

		Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
        player.GetComponent<SpriteRenderer>().color = Color.white;
	}

    private void deathReset() 
    {
		Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
        PlayerHealth.OnPlayerDeath -= deathReset;
        player.GetComponent<SpriteRenderer>().color = Color.white;
    }
}
