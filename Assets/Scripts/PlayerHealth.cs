using Unity.VisualScripting;
using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private ParticleSystem explosionParticle;

    [SerializeField] public int Health;
    [SerializeField] private int MaxHealth;

    [SerializeField] private GameObject[] heart;
    [SerializeField] private Sprite heartFull;
    [SerializeField] private Sprite heartEmpty;

    public GameManager gameManager;
    private IAudioSystem audioSystem;

    public static event Action OnPlayerDeath;

    void Awake()
    {
        audioSystem = ServiceLocator.Get<IAudioSystem>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Asteroid"))
        {
            removeHealth();
            OnPlayerDeath?.Invoke();
        }
    }

    private void removeHealth()
    {
        GameManager gameManager = FindFirstObjectByType<GameManager>();
        switch(Health)
        {
            case 3:
                handleEffects(2);
                break;
            case 2: 
                handleEffects(1);
                break;
            case 1:
                handleEffects(0);
                gameManager.GameOver();
                Destroy(gameObject);
                break;
        }
        audioSystem.PlaySound("Sounds/death");
        Camera.main.GetComponent<CameraShake>().Shake(0.2f, 0.2f);
        Health--;
    }

    private void handleEffects(int i) {
        heart[i].GetComponent<SpriteRenderer>().sprite = heartEmpty;
        Instantiate(explosionParticle, transform.position, Quaternion.identity);

        if(i != 0) {
            gameObject.SetActive(false);
            gameManager.RespawnPlayer(gameObject);
        }
    }

    public void addHealth()
    {
        if(Health == MaxHealth) {
            return;
        }

        Health++;
        switch(Health)
        {
            case 1:
                fillHeartSprite(0);
                break;
            case 2: 
                fillHeartSprite(1);
                break;
            case 3:
                fillHeartSprite(2);
                break;
        }
    }

    private void fillHeartSprite(int i) {
        heart[i].GetComponent<SpriteRenderer>().sprite = heartFull;
    }
}
