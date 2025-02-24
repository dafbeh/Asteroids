using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Powerups/BulletPower")]
public class BulletPower : PowerUpEffect
{
    private GameObject weapon;
    public override void Apply(GameObject gameObject) {
        weapon = gameObject.transform.GetChild(0).gameObject;
        
        weapon.GetComponent<Weapon>().autoFire = true;

        PlayerHealth.OnPlayerDeath += deathReset;
        gameObject.GetComponent<MonoBehaviour>().StartCoroutine(resetPower());
    }

    private IEnumerator resetPower() 
	{
		yield return new WaitForSeconds(10f);

        weapon.GetComponent<Weapon>().autoFire = false;
	}

    private void deathReset() 
    {
        weapon.GetComponent<Weapon>().autoFire = false;
        PlayerHealth.OnPlayerDeath -= deathReset;
    }
}
