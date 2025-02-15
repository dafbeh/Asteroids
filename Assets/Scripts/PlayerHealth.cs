using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private ParticleSystem explosionParticle;

    [SerializeField] private int Health;
    [SerializeField] private int MaxHealth;

    [SerializeField] private GameObject[] heart;
    [SerializeField] private Sprite heartFull;
    [SerializeField] private Sprite heartEmpty;


    public GameManager gameManager;

    private Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Asteroid"))
        {
            removeHealth();
        }
    }

    private void removeHealth()
    {
        GameManager gameManager = FindFirstObjectByType<GameManager>();
        switch(Health)
        {
            case 3:
                heart[2].GetComponent<SpriteRenderer>().sprite = heartEmpty;
                Instantiate(explosionParticle, transform.position, Quaternion.identity);
                gameObject.SetActive(false);
                gameManager.RespawnPlayer(gameObject);
                break;
            case 2: 
                heart[1].GetComponent<SpriteRenderer>().sprite = heartEmpty;
                Instantiate(explosionParticle, transform.position, Quaternion.identity);
                gameObject.SetActive(false);
                gameManager.RespawnPlayer(gameObject);
                break;
            case 1:
                heart[0].GetComponent<SpriteRenderer>().sprite = heartEmpty;
                Instantiate(explosionPrefab, gameObject.transform.position, gameObject.transform.rotation);
                Destroy(gameObject);
                gameManager.GameOver();
                break;
        }
        Health--;
    }

    private void addHealth()
    {
        Health++;
    }
}
