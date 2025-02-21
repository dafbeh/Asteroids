using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/HealthPower")]
public class HealthPower : PowerUpEffect
{
    public override void Apply(GameObject gameObject) {
        gameObject.GetComponent<PlayerHealth>().addHealth();        
    }
}