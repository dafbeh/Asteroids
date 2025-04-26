using System.Collections;
using UnityEngine;
using Helpers;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private AsteroidController asteroidPrefab;
    [SerializeField] public int asteroidCount = 0;
    [SerializeField] public int level = 0;
    [SerializeField] public GameObject[] powerUps;

    private Camera mainCamera;
    public bool spawningAsteroids;
    private bool insideAsteroid = false;

    void Start()
    {
        instance = this;
        mainCamera = Camera.main;
    }

    void Update()
    {
        if(asteroidCount == 0 && !spawningAsteroids)
        {
            level++;
            StartCoroutine(SpawnAsteroidsWithDelay());
        }
    }

    private IEnumerator SpawnAsteroidsWithDelay()
    {
        spawningAsteroids = true;
        while(mainCamera.orthographicSize <= 4.5f) {
            yield return null;
        }

        int asteroids = (2 * level) + 2;
        for (int i = 0; i < asteroids; i++) 
        {
            Instantiate(asteroidPrefab);
            yield return new WaitForSeconds(0.1f);
        }

        spawnPowerup();
        spawningAsteroids = false;
    }

    private void spawnPowerup() {
        int id = Random.Range(0, powerUps.Length);
        Vector3 location = SpawnHelper.randomScreenLocation(0f);

        GameObject powerUpObj = Instantiate(powerUps[id], location, Quaternion.identity);

        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        powerUpObj.GetComponent<Rigidbody2D>().AddForce(randomDirection * 100f);

        PowerUp powerUp = powerUpObj.GetComponent<PowerUp>();
        if (powerUp != null) {
            powerUp.powerUpID = id;
        }
    }

    public GameObject getPowerUpPrefab(int id)
    {
        return powerUps[id];
    }

    public void RespawnPlayer(GameObject player)
    {
        StartCoroutine(respawn(player));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Asteroid"))
        {
            insideAsteroid = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Asteroid"))
        {
            insideAsteroid = false;
        }
    }

    private IEnumerator respawn(GameObject player)
    {
        while(insideAsteroid) {
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        player.transform.localPosition = new Vector3(0,0,0);
        player.SetActive(true);
    }

    public void GameOver()
    {
        int score = ScoreManager.instance.getScore();
        Leaderboard.instance.updateLeaderboard(score);

        PlayerPrefs.SetInt("ActiveScore", score);
        PlayerPrefs.Save();
        
        FindFirstObjectByType<GameOver>().toggleGameOver();
    }
}
