using System.Collections;
using System.Numerics;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private int Health;
    [SerializeField] private int MaxHealth;

    public GameManager gameManager;

    private Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager gameManager = FindFirstObjectByType<GameManager>();
        if (collision.CompareTag("Asteroid"))
        {
            if(Health > 1)
            {
                Health--;
                gameObject.SetActive(false);
                gameManager.RespawnPlayer(gameObject);
            } else {
                Instantiate(explosionPrefab, gameObject.transform.position, gameObject.transform.rotation);
                Destroy(gameObject);
                gameManager.GameOver();
            }
        }
    }
}
