using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/ShrinkPower")]
public class ShrinkPower : PowerUpEffect
{
	[SerializeField] private Vector3 scale = new Vector3(0.15f, 0.15f, 0.15f);
	[SerializeField] private Color powerColor = new Color(97, 195, 0);

    protected override void ApplyPower()
    {
        Resize(scale);
		SetColor(powerColor);
		PlaySound("Sounds/wind");
    }

    protected override void RemovePower()
    {
        Resize(originalScale);
        SetColor(Color.white);
    }
}