using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/GravityPower")]
public class GravityPower : PowerUpEffect
{
    public PlayerRipple playerRipple;
    [SerializeField] private float blastRadius = 10f;

    public GravityPower()
    {
        duration = 1f;
    }
    
    protected override void ApplyPower()
    {
        sentGravity();
        PlaySound("sounds/gravity");
        FindFirstObjectByType<PlayerRipple>().ripplePlayer();
    }

    protected override void RemovePower() 
    {

    }

    private void sentGravity() {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.transform.position, blastRadius);

        foreach(Collider2D collider in colliders) {
            Rigidbody2D rb2d = collider.GetComponent<Rigidbody2D>();

            if (rb2d != null) {
                if (collider.CompareTag("Player")) continue;

                Vector2 direction = (collider.transform.position - player.transform.position).normalized;

                float distance = Vector2.Distance(player.transform.position, collider.transform.position);
                float forceMagnitude = 15000f * (1 - (distance / blastRadius));

                if (collider.CompareTag("PowerUp")) {
                    forceMagnitude = 100f * (1 - (distance / blastRadius));
                }
                
                rb2d.AddForce(direction * forceMagnitude);
            }
        }
    }
}