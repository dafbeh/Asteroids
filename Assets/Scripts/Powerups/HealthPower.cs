using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/HealthPower")]
public class HealthPower : PowerUpEffect
{
    public HealthPower()
    {
        duration = 0;
    }
    
    protected override void ApplyPower()
    {
        AddHealth();
        PlaySound("Sounds/heal");
    }
}