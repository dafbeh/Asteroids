using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/TimePower")]
public class TimePower : PowerUpEffect
{
    [SerializeField] private float timeScale = 0.5f;
    [SerializeField] private Color powerColor = new Color(20, 99, 6);
    
    public TimePower()
    {
        duration = 5f;
    }
    
    protected override void ApplyPower()
    {
        SetTime(timeScale);
        SetColor(powerColor);
        PlaySound("Sounds/time");
    }
    
    protected override void RemovePower()
    {
        SetTime(1f);
        SetColor(Color.white);
    }
}