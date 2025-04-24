using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PowerUp : MonoBehaviour
{
    public int powerUpID;   
    [SerializeField] public PowerUpEffect effect;
    [SerializeField] private ParticleSystem explosionParticle;
    [SerializeField] private bool pickUp = false;
    [SerializeField] public float timer;

    private Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Rigidbody2D bulletRb = collision.GetComponent<Rigidbody2D>();
        Vector2 bulletVelocity = bulletRb.linearVelocity;
        Vector2 forceDirection = bulletVelocity.normalized;

        if(collision.CompareTag("Player")) {
            Destroy(gameObject);
            if(pickUp) {
                PickUp();
            } else {
                effect.Apply(collision.gameObject);
            }
        }

        if(collision.CompareTag("Asteroid")) {
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position, Quaternion.identity);
        }

        if(collision.CompareTag("Bullet")) {
            rb2d.AddForce(2 * forceDirection, ForceMode2D.Impulse);

            if (rb2d.linearVelocity.magnitude > 4)
            {
                rb2d.linearVelocity = rb2d.linearVelocity.normalized * 4;
            }
        }
    }

    private void PickUp() {
        SlotManager slotManager = FindFirstObjectByType<SlotManager>();
        int powerUpID = this.powerUpID;

        slotManager.storeItem(this, powerUpID);
    }
}