using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Powerups/TimePower")]
public class TimePower : PowerUpEffect
{
    public override void Apply(GameObject gameObject) {
        Time.timeScale = 0.5f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        PlayerHealth.OnPlayerDeath += deathReset;
        gameObject.GetComponent<MonoBehaviour>().StartCoroutine(resetPower());
    }

    private IEnumerator resetPower() 
	{
		yield return new WaitForSeconds(5f);

		Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
	}

    private void deathReset() 
    {
		Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
        PlayerHealth.OnPlayerDeath -= deathReset;
    }
}
