using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Helpers;
using Unity.Mathematics;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class AsteroidController : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private float speed;
    [SerializeField] public int size = 3;
    [SerializeField] private ParticleSystem explosionParticle;

    private Camera mainCamera;
    private IAudioSystem audioSystem;

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
        audioSystem = ServiceLocator.Get<IAudioSystem>();
    }

    void Start()
    {
        SpawnAsteroid();

        gameManager.asteroidCount++;
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
            } else {
                powerUpChance();
            }
            Instantiate(explosionParticle, transform.position, Quaternion.identity);
            Destroy(gameObject);
            ScoreManager.instance.AddScore();
            audioSystem.PlaySound("Sounds/explode");
        }

        if (collision.CompareTag("PowerUp")) {
            gameManager.asteroidCount--;
            Destroy(gameObject);
            ScoreManager.instance.AddScore(size * 10);
            audioSystem.PlaySound("Sounds/bigexplode");
        }
    }

    public void SpawnAsteroid()
    {
        Vector3 spawnPosition = SpawnHelper.randomScreenLocation(1.5f);
        Vector3 viewportToWorldPoint = mainCamera.ViewportToWorldPoint(spawnPosition);
        Vector2 worldSpawnPosition = new Vector2(viewportToWorldPoint.x, viewportToWorldPoint.y);

        spriteRenderer.sprite = sprites[UnityEngine.Random.Range(0, sprites.Length)];
        transform.localScale = new Vector3(0.3f * size, 0.3f * size, 1f);
        if(size == 3) {
            transform.position = worldSpawnPosition;
        }
        transform.rotation = Quaternion.Euler(0, 0, UnityEngine.Random.Range(0, 360));

        float randomRange = UnityEngine.Random.Range(speed, speed * 1.5f);

        float levelSpeedMultiplier = 1 + (GameManager.instance.level * 0.1f);
        float randomSpeed = randomRange * levelSpeedMultiplier;

        float maxSpeed = 15000f;
        randomSpeed = Mathf.Min(randomSpeed, maxSpeed);

        rb2d.AddForce(transform.up * randomSpeed);
    }

    private void powerUpChance() {
        GameObject[] powerUps = GameManager.instance.powerUps;
        int chance = UnityEngine.Random.Range(0, 20);

        if(chance == 5) {
            int id = UnityEngine.Random.Range(1, powerUps.Length);
            Vector3 location = SpawnHelper.randomScreenLocation(3f);
    
            GameObject powerUpObj = Instantiate(powerUps[id], location, Quaternion.identity);
    
            Vector2 randomDirection = UnityEngine.Random.insideUnitCircle.normalized;
            powerUpObj.GetComponent<Rigidbody2D>().AddForce(randomDirection * 100f);
    
            PowerUp powerUp = powerUpObj.GetComponent<PowerUp>();
            if (powerUp != null) {
                powerUp.powerUpID = id;
            }
        }
    }
}
