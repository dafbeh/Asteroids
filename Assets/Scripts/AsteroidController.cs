using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class AsteroidController : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private float speed;
    [SerializeField] public float size = 3;
    [SerializeField] private ParticleSystem explosionParticle;

    private SpriteRenderer spriteRenderer;
    private WrapAroundController wrapAroundController;
    private Rigidbody2D rb2d;
    public GameManager gameManager;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        wrapAroundController = GetComponent<WrapAroundController>();
        rb2d = GetComponent<Rigidbody2D>();
        gameManager = FindFirstObjectByType<GameManager>();

        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        transform.localScale = new Vector3(0.3f * size, 0.3f * size, 1f);
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        rb2d.AddForce(transform.up * speed);

        gameManager.asteroidCount++;
    }

    void FixedUpdate()
    {
        if(wrapAroundController.isOutOfBounds)
        {
            Vector2 newDir = Random.insideUnitCircle.normalized;

            //rb2d.linearVelocity = newDir;        
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            gameManager.asteroidCount--;
            if(size > 1) 
            {
                size--;
                for(int i = 0; i < 2; i++) {
                    Instantiate(this, gameObject.transform.position, gameObject.transform.rotation);
                }
                Instantiate(explosionParticle, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }
}