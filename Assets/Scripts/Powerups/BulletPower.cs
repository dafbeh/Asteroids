using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Powerups/BulletPower")]
public class BulletPower : PowerUpEffect
{
    private GameObject weapon;
    private GameObject player;    
    private Color orange = new Color(255, 124, 0);

    public override void Apply(GameObject gameObject) {
        weapon = gameObject.transform.GetChild(0).gameObject;
        player = gameObject;
        
        player.GetComponent<SpriteRenderer>().color = orange;
        weapon.GetComponent<Weapon>().autoFire = true;

        PlayerHealth.OnPlayerDeath += deathReset;
        gameObject.GetComponent<MonoBehaviour>().StartCoroutine(resetPower());
    }

    private IEnumerator resetPower() 
	{
		yield return new WaitForSeconds(10f);

        player.GetComponent<SpriteRenderer>().color = Color.white;
        weapon.GetComponent<Weapon>().autoFire = false;
        player.GetComponent<SpriteRenderer>().color = Color.white;
	}

    private void deathReset() 
    {
        weapon.GetComponent<Weapon>().autoFire = false;
        PlayerHealth.OnPlayerDeath -= deathReset;
        player.GetComponent<SpriteRenderer>().color = Color.white;
    }
}
