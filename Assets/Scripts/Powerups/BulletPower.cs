using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Powerups/BulletPower")]
public class BulletPower : PowerUpEffect
{
    [SerializeField] private Color powerColor = new Color(163, 79, 0);

    protected override void ApplyPower()
    {
        SetColor(powerColor);
        SetAutoFire(true);
        PlaySound("Sounds/reload");
    }

    protected override void RemovePower()
    {
        SetColor(Color.white);
        SetAutoFire(false);
    }
}
