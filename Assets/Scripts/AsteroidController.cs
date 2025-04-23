using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Helpers;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class AsteroidController : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private float speed;
    [SerializeField] public float size = 3;
    [SerializeField] private ParticleSystem explosionParticle;

    private Camera mainCamera;

    private SpriteRenderer spriteRenderer;
    private WrapAroundController wrapAroundController;
    private Rigidbody2D rb2d;
    public GameManager gameManager;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        wrapAroundController = GetComponent<WrapAroundController>();
        rb2d = GetComponent<Rigidbody2D>();
        gameManager = FindFirstObjectByType<GameManager>();
        mainCamera = FindFirstObjectByType<Camera>();
    }

    void Start()
    {
        SpawnAsteroid();

        gameManager.asteroidCount++;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet") || collision.CompareTag("PowerUp"))
        {
            gameManager.asteroidCount--;
            if(size > 1) 
            {
                size--;
                for(int i = 0; i < 2; i++) {
                    Instantiate(this, gameObject.transform.position, gameObject.transform.rotation);
                }
            }
            Instantiate(explosionParticle, transform.position, Quaternion.identity);
            Destroy(gameObject);
            ScoreManager.instance.AddScore();
        }
    }

    public void SpawnAsteroid()
    {
        Vector3 spawnPosition = SpawnHelper.randomScreenLocation(1.5f);
        Vector3 viewportToWorldPoint = mainCamera.ViewportToWorldPoint(spawnPosition);
        Vector2 worldSpawnPosition = new Vector2(viewportToWorldPoint.x, viewportToWorldPoint.y);

        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        transform.localScale = new Vector3(0.3f * size, 0.3f * size, 1f);
        if(size == 3) {
            transform.position = worldSpawnPosition;
        }
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        rb2d.AddForce(transform.up * speed);
    }
}